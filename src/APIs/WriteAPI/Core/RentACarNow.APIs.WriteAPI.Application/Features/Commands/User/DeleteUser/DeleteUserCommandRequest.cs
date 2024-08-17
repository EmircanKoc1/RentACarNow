﻿using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser
{
    public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
    {
        public Guid Id { get; set; }
    }

}