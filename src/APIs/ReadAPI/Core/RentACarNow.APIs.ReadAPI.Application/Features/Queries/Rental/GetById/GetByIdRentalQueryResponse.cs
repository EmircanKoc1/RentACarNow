using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public CarDTO Car { get; set; }
        public CustomerDTO Customer { get; set; }
        public RentalStatus Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }

}
