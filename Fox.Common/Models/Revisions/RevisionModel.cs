using System.Collections.Generic;

namespace Fox.Common.Models
{
    public class RevisionModel
    {
        public string Id { get; set; }
        public List<KeyValuePair<string, object>> Properties { get; set; }
    }
}
