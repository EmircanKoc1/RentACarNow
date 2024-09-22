using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandResponse : BaseCommandResponse
    {
        public Guid RentalId { get; set; }
       
    }

}
