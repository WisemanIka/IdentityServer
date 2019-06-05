using System.Collections.Generic;
using Fox.Common.Models;

namespace Fox.Provider.Models
{
    public class Providers : BaseMongoCollection
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Address> Addresses { get; set; }
        public bool IsActive { get; set; }

        public class Contact
        {
            public string Person { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string AdditionalInfo { get; set; }
        }

        public class Address
        {
            public string City { get; set; }
            public string Street { get; set; }
            public string AdditionalInfo { get; set; }
        }
    }
}
