using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base
{
    public class BaseGetAllQueryRequest
    {
        public bool IsAscending { get; set; } = true;
        public string? SortingField { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }


    }
}
