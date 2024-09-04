using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EFEntities = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly IValidator<CreateCarCommandRequest> _validator;
        private readonly ILogger<CreateCarCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CreateCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository, 
            IEfCoreCarReadRepository carReadRepository, 
            IEfCoreBrandReadRepository brandReadRepository, 
            ICarOutboxRepository carOutboxRepository, 
            IValidator<CreateCarCommandRequest> validator, 
            ILogger<CreateCarCommandRequestHandler> logger, 
            IMapper mapper)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _brandReadRepository = brandReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(CreateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Request not validated");


                return new CreateCarCommandResponse
                {
                    CarId = Guid.Empty,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }
            var foundedBrand = await _brandReadRepository.GetByIdAsync(request.BrandId);

            if (foundedBrand is null)
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} brand not found , id : {request.BrandId}");
                return new CreateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "brand not found",
                            PropertyName = null
                        }
                    }                   
                };
            }

            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var efCarEntity = _mapper.Map<EFEntities.Car>(request);
            efCarEntity.Id = Guid.NewGuid();
            efCarEntity.CreatedDate = DateHelper.GetDate();


            var carCreatedEvent = _mapper.Map<CarCreatedEvent>(efCarEntity);

            carCreatedEvent.Brand = _mapper.Map<BrandMessage>(foundedBrand);

            try
            {
                mongoSession.StartTransaction();



                await _carWriteRepository.AddAsync(efCarEntity);
                await _carWriteRepository.SaveChangesAsync();

                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateHelper.GetDate(),
                    CarEventType = CarEventType.CarCreatedEvent,
                    Payload = carCreatedEvent.Serialize()!,
                  
                }, mongoSession);


                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Transaction commited");


            }
            catch
            {

                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(CreateCarCommandRequestHandler)} transaction rollbacked");

                return new CreateCarCommandResponse
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

            return new CreateCarCommandResponse
            {
                CarId = efCarEntity.Id,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
