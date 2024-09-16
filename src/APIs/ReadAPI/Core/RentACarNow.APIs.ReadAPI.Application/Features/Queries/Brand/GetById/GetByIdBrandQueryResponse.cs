using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{
    public class GetByIdBrandQueryResponse 
    {

        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }

    }

}
