using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly IValidator<DeleteCarCommandRequest> _validator;
        private readonly ILogger<DeleteCarCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            IValidator<DeleteCarCommandRequest> validator,
            ILogger<DeleteCarCommandRequestHandler> logger,
            IMapper mapper)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteCarCommandRequestHandler)} Handle method has been executed");



            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Request not validated");


                return new DeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} car not found , id : {request.CarId}");
                return new DeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "car not found",
                            PropertyName = null
                        }
                    }
                };
            }


            using var efTransaction = _carWriteRepository.BeginTransaction();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();


            var carDeletedEvent = new CarDeletedEvent()
            {
                DeletedDate = DateHelper.GetDate(),
                Id = request.CarId,
                MessageId = Guid.NewGuid(),
            };

            try
            {
                mongoSession.StartTransaction();

                _carWriteRepository.DeleteById(request.CarId);
                _carWriteRepository.SaveChanges();


                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage()
                {
                    Id = request.CarId,
                    AddedDate = DateHelper.GetDate(),
                    CarEventType = CarEventType.CarDeletedEvent,
                    Payload = carDeletedEvent.Serialize()

                }, mongoSession);


                await mongoSession.CommitTransactionAsync();
                efTransaction.Commit();

                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                efTransaction.Rollback();

                _logger.LogError($"{nameof(DeleteCarCommandRequestHandler)} transaction rollbacked");

                return new DeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Transaction exception",
                            PropertyName = null
                        }
                    }
                };

            }

            return new DeleteCarCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
