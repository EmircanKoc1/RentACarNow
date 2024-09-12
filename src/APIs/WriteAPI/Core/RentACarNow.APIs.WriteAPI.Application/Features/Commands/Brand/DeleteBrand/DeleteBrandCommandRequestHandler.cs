using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestHandler : IRequestHandler<DeleteBrandCommandRequest, DeleteBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IValidator<DeleteBrandCommandRequest> _validator;
        private readonly ILogger<DeleteBrandCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBrandEventFactory _brandEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public DeleteBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IBrandOutboxRepository brandOutboxRepository,
            IValidator<DeleteBrandCommandRequest> validator,
            ILogger<DeleteBrandCommandRequestHandler> logger,
            IMapper mapper,
            IBrandEventFactory brandEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _brandEventFactory = brandEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteBrandCommandRequestHandler)} Handle method has been executed");

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteBrandCommandRequestHandler)} Request not validated");

                return new DeleteBrandCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }

            var isExists = await _brandReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteBrandCommandRequestHandler)} Entity not found , id : {request.Id}");
                return new DeleteBrandCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Entity not found",
                            PropertyName = null
                        }
}
                };
            }

            var generatedDeletionDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            _logger.LogInformation($"{nameof(DeleteBrandCommandRequestHandler)} Transaction started");

            mongoSession.StartTransaction();

            var brandDeletedEvent = _brandEventFactory.CreateBrandDeletedEvent(
                brandId: request.Id,
                deletedDate: generatedDeletionDate).SetMessageId<BrandDeletedEvent>(generatedMessageId);


            try
            {

                _brandWriteRepository.DeleteById(request.Id);
                _brandWriteRepository.SaveChanges();

                var outboxMessage = new BrandOutboxMessage
                {
                    Id = generatedMessageId,
                    EventType = BrandEventType.BrandDeletedEvent,
                    AddedDate = generatedMessageAddedDate,
                    Payload = brandDeletedEvent.Serialize()!
                };

                await _brandOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                efTransaction.Commit();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(DeleteBrandCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {


                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(DeleteBrandCommandRequestHandler)} transaction rollbacked");

                return new DeleteBrandCommandResponse
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


            return new DeleteBrandCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
