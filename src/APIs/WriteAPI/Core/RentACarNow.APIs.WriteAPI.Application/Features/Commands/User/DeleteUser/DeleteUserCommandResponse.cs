﻿using Amazon.Runtime.Internal;
using RentACarNow.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser
{

    public class DeleteUserCommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }
    }

}
