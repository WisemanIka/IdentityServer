using System.Collections.Generic;

namespace Fox.Provider.Models.ViewModels.Provider
{
    public class CreateProviderRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProviderContactModel> Contacts { get; set; }
        public List<ProviderAddressModel> Addresses { get; set; }
        public bool IsActive { get; set; }

        public class ProviderContactModel
        {
            public string Person { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string AdditionalInfo { get; set; }
        }

        public class ProviderAddressModel
        {
            public string City { get; set; }
            public string Street { get; set; }
            public string AdditionalInfo { get; set; }
        }
    }
}
