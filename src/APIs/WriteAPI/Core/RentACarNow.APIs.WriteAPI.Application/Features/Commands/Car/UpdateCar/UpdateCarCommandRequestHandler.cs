using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequestHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly ICarOutboxRepository _carOutboxReadRepository;
        private readonly IValidator<UpdateCarCommandRequest> _validator;
        private readonly ILogger<UpdateCarCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxReadRepository,
            IValidator<UpdateCarCommandRequest> validator,
            ILogger<UpdateCarCommandRequestHandler> logger,
            IMapper mapper,
            IEfCoreBrandReadRepository brandReadRepository)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxReadRepository = carOutboxReadRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _brandReadRepository = brandReadRepository;
        }

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateCarCommandResponse { };
            }

            var carIsExists = await _carReadRepository.IsExistsAsync(request.Id);
            var brand = await _brandReadRepository.GetByIdAsync(request.BrandId);
            if (!carIsExists || brand is null)
                return new UpdateCarCommandResponse { };

            var carEntity = _mapper.Map<EfEntity.Car>(request);
            



            var carUpdatedEvent = _mapper.Map<CarUpdatedEvent>(carEntity);
            carUpdatedEvent.Brand = _mapper.Map<BrandMessage>(brand);

            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxReadRepository.StartSessionAsync();

            try
            {

                mongoSession.StartTransaction();

                await _carWriteRepository.UpdateAsync(carEntity);
                await _carWriteRepository.SaveChangesAsync();

                await _carOutboxReadRepository.AddMessageAsync(new CarOutboxMessage
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateTime.Now,
                    CarEventType = CarEventType.CarUpdatedEvent,
                    Payload = carUpdatedEvent.Serialize()!,
                    IsPublished = false,
                    PublishDate = null
                }, mongoSession);



                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                throw;
            }




            return new UpdateCarCommandResponse { };
        }
    }

}
