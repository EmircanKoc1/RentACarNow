using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Rental
{
    public class RentalDeletedEvent : BaseEvent
    {
        public RentalDeletedEvent(Guid rentalId, DateTime deletedDate)
        {
            RentalId = rentalId;
            DeletedDate = deletedDate;
        }

        public Guid RentalId { get; set; }
        public DateTime DeletedDate { get; set; }

    }
}
