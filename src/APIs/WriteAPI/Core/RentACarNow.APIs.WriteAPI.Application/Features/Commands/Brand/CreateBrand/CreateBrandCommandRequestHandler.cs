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
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IValidator<CreateBrandCommandRequest> _validator;
        private readonly ILogger<CreateBrandCommandRequestHandler> _logger;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IValidator<CreateBrandCommandRequest> validator,
            ILogger<CreateBrandCommandRequestHandler> logger,
            IBrandOutboxRepository brandOutboxRepository, IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _validator = validator;
            _logger = logger;
            _brandOutboxRepository = brandOutboxRepository;
            _mapper = mapper;
        }

        public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateBrandCommandResponse
                {
                    BrandId = Guid.Empty,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var efBrandEntity = _mapper.Map<EfEntity.Brand>(request);
            efBrandEntity.Id = Guid.NewGuid();
            efBrandEntity.CreatedDate = DateHelper.GetDate();

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();


            var brandCreatedEvent = _mapper.Map<BrandCreatedEvent>(efBrandEntity);
            brandCreatedEvent.MessageId = Guid.NewGuid();
            //brandCreatedEvent.CreatedDate = DateHelper.GetDate();

            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.AddAsync(efBrandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                var outboxMessage = new BrandOutboxMessage
                {
                    Id = brandCreatedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    Payload = brandCreatedEvent.Serialize()!
                };

                await _brandOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTransaction.RollbackAsync();

                return new CreateBrandCommandResponse
                {
                    BrandId = Guid.Empty,
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


            return new CreateBrandCommandResponse
            {
                BrandId = efBrandEntity.Id,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }

}
