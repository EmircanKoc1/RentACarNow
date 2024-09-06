using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestHandler : IRequestHandler<DeleteClaimCommandRequest, DeleteClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IClaimOutboxRepository _claimOutboxRepository;
        private readonly IValidator<DeleteClaimCommandRequest> _validator;
        private readonly ILogger<DeleteClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<DeleteClaimCommandRequest> validator,
            ILogger<DeleteClaimCommandRequestHandler> logger,
            IMapper mapper,
            IClaimOutboxRepository claimOutboxRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _claimOutboxRepository = claimOutboxRepository;
        }

        public async Task<DeleteClaimCommandResponse> Handle(DeleteClaimCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteClaimCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} Request not validated");


                return new DeleteClaimCommandResponse
                {
                    
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _readRepository.IsExistsAsync(request.ClaimId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} claim not found , id : {request.ClaimId}");
                return new DeleteClaimCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "claim not found",
                            PropertyName = null
                        }
                    }
                };
            }
            var claimEntity = _mapper.Map<EfEntity.Claim>(request);

            var claimDeletedEvent = _mapper.Map<ClaimDeletedEvent>(claimEntity);
            claimDeletedEvent.DeletedDate = DateHelper.GetDate();

            using var efTran = _writeRepository.BeginTransaction();
            using var mongoSession = await _claimOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();


                _writeRepository.Delete(claimEntity);
                _writeRepository.SaveChanges();


                var outboxMessage = new ClaimOutboxMessage
                {
                    Id = claimDeletedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    ClaimEventType = ClaimEventType.ClaimDeletedEvent,
                    Payload = claimDeletedEvent.Serialize()!
                    
                };

                await _claimOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                efTran.Commit();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                efTran.Rollback();

                _logger.LogError($"{nameof(DeleteClaimCommandRequestHandler)} transaction rollbacked");

                return new DeleteClaimCommandResponse
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


            return new DeleteClaimCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
