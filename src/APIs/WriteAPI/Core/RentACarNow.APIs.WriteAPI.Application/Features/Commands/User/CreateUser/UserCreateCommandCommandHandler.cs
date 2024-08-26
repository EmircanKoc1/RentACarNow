using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using   RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{
    public class UserCreateCommandCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<UserCreateCommandCommandHandler> _logger;
        private readonly IValidator<UserCreateCommandRequest> _validator;
        private readonly IMapper _mapper;

        public UserCreateCommandCommandHandler(IEfCoreUserWriteRepository userWriteRepository, IEfCoreUserReadRepository userReadRepository, IUserOutboxRepository userOutboxRepository, ILogger<UserCreateCommandCommandHandler> logger, IValidator<UserCreateCommandRequest> validator, IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }


        public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UserCreateCommandResponse();
            }

            var efUser = _mapper.Map<EfEntity.User>(request);


            return null;
        }
    }

}
