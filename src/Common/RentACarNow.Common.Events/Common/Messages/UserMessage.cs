using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Common.Messages
{
    public class UserMessage
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public int Age { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
    }
}
