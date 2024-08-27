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
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteCarCommandResponse();
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteCarCommandResponse();
            }


            using var efTransaction = _carWriteRepository.BeginTransaction();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();


            var carDeletedEvent = new CarDeletedEvent()
            {
                DeletedDate = DateTime.UtcNow,
                Id = request.Id,
            };

            try
            {
                mongoSession.StartTransaction();

                _carWriteRepository.DeleteById(request.Id);
                _carWriteRepository.SaveChanges();
                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage()
                {
                    AddedDate = DateTime.UtcNow,
                    CarEventType = CarEventType.CarDeletedEvent,
                    Id = request.Id,
                    IsPublished = false,
                    Payload = carDeletedEvent.Serialize()

                }, mongoSession);


                await mongoSession.CommitTransactionAsync();
                efTransaction.Commit();
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                efTransaction.Rollback();
                throw;
            }



            return new DeleteCarCommandResponse();
        }
    }
}
