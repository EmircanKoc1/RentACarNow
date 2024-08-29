using MediatR;
using RentACarNow.Common.Enums.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequest : IRequest<UpdateRentalCommandResponse>
    {
        public Guid Id { get; set; }
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }
        public RentalStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }


    }

}
