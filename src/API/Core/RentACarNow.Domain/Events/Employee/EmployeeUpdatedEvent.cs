using RentACarNow.Domain.Enums;
using RentACarNow.Domain.Events.Common;

namespace RentACarNow.Domain.Events.Employee
{
    public class EmployeeUpdatedEvent : BaseEvent
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public EmployeePosition Position { get; set; }
        public WorkEnvironment WorkEnvironment { get; set; }
    }
}
