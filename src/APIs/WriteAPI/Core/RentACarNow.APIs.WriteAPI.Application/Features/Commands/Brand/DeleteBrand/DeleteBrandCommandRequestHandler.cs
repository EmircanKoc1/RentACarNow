using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
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

        public DeleteBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IBrandOutboxRepository brandOutboxRepository,
            IValidator<DeleteBrandCommandRequest> validator,
            ILogger<DeleteBrandCommandRequestHandler> logger,
            IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
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

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            _logger.LogInformation($"{nameof(DeleteBrandCommandRequestHandler)} Transaction started");

            mongoSession.StartTransaction();

            var brandDeletedEvent = new BrandDeletedEvent
            {
                DeletedDate = DateHelper.GetDate(),
                Id = request.Id,
            };

            brandDeletedEvent.MessageId = Guid.NewGuid();

            try
            {

                _brandWriteRepository.DeleteById(request.Id);
                _brandWriteRepository.SaveChanges();

                var outboxMessage = new BrandOutboxMessage
                {
                    Id = brandDeletedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
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
            }


            return new DeleteBrandCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
