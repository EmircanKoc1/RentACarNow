﻿using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{
    public class UserCreateCommandRequest : IRequest<UserCreateCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public decimal WalletBalance { get; set; }
    }

}