using System.Collections.Generic;

namespace Fox.Common.Models
{
    public class RevisionModel
    {
        public string Id { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }
}
