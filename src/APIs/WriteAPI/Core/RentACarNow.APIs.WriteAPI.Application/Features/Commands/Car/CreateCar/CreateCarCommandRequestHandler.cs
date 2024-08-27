using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
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

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateCarCommandResponse();
            }

            var brand = await _brandReadRepository.GetByIdAsync(request.BrandId);

            if (brand is null) return null;

            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var efCarEntity = _mapper.Map<EFEntities.Car>(request);
            efCarEntity.Brand = brand;
            efCarEntity.Id = Guid.NewGuid();

            var carCreatedEvent = _mapper.Map<CarCreatedEvent>(efCarEntity);


            try
            {
                mongoSession.StartTransaction();



                await _carWriteRepository.AddAsync(efCarEntity);
                await _carWriteRepository.SaveChangesAsync();

                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateTime.UtcNow,
                    CarEventType = CarEventType.CarCreatedEvent,
                    Payload = carCreatedEvent.Serialize()!,
                    IsPublished = false,
                    PublishDate = null
                }, mongoSession);


                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();
            }
            catch
            {

                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();
            }

            return null;
        }
    }
}
