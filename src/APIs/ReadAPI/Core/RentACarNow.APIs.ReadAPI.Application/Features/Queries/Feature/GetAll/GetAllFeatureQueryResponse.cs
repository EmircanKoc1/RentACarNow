﻿namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll
{
    public class GetAllFeatureQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid CarId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }


    }

}
