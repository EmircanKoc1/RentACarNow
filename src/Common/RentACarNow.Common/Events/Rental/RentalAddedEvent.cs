using RentACarNow.Common.Enums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Rental
{
    public class RentalAddedEvent : BaseEvent
    {

        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public CarMessage Car { get; set; }
        public CustomerMessage Customer { get; set; }
        public RentalStatus Status { get; set; }

    }
}
