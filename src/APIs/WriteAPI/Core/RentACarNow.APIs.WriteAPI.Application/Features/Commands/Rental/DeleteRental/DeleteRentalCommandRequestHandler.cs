using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _rentalWriteRepository;
        private readonly IEfCoreRentalReadRepository _rentalReadRepositoyr;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<DeleteRentalCommandRequest> _validator;
        private readonly ILogger<DeleteRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteRentalCommandRequestHandler(IEfCoreRentalWriteRepository rentalWriteRepository, IEfCoreRentalReadRepository rentalReadRepositoyr, IRentalOutboxRepository rentalOutboxRepository, IValidator<DeleteRentalCommandRequest> validator, ILogger<DeleteRentalCommandRequestHandler> logger, IMapper mapper)
        {
            _rentalWriteRepository = rentalWriteRepository;
            _rentalReadRepositoyr = rentalReadRepositoyr;
            _rentalOutboxRepository = rentalOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DeleteRentalCommandResponse> Handle(DeleteRentalCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteCarCommandRequestHandler)} Handle method has been executed");



            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Request not validated");


                return new DeleteRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }



            var isExists = await _rentalReadRepositoyr.IsExistsAsync(request.RentalId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteRentalCommandRequestHandler)} rental not found , id : {request.RentalId}");
                return new DeleteRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "rental not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var rentalDeletedEvent = new RentalDeletedEvent
            {
                Id = request.RentalId,
                MessageId = Guid.NewGuid(),
                DeletedDate = DateHelper.GetDate()
            };

            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            using var efTran = await _rentalWriteRepository.BeginTransactionAsync();

            try
            {
                mongoSession.StartTransaction();


                _rentalWriteRepository.DeleteById(request.RentalId);
                await _rentalWriteRepository.SaveChangesAsync();

                var outboxMessage = new RentalOutboxMessage()
                {
                    Id = rentalDeletedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    EventType = RentalEventType.RentalDeletedEvent,
                    Payload = rentalDeletedEvent.Serialize()!
                };

                await _rentalOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);


                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();
            }
            catch
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(DeleteRentalCommandRequestHandler)} transaction rollbacked");

                return new DeleteRentalCommandResponse
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


            return new DeleteRentalCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
