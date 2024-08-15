using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Models
{
    public class OrderingParameter
    {
        public bool Sort { get; set; } 
        public bool IsAscending { get; set; } = true;
        public string SortingField { get; set; }
    }
}
