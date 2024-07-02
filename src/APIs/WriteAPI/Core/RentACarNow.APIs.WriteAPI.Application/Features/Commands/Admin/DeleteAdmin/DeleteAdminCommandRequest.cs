using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequest : IRequest<DeleteAdminCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
