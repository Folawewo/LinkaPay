using System;
namespace LinkaPay.Domain.Entities
{
    public class Users: BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
    }
}

