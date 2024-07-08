using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.DTOs
{
    public class ClaimDTO
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
