using System.Collections.Generic;
using Fox.Common.Models;

namespace RabbitMQ.Consumer.Models
{
    public class ProductRevisions
    {
        public string Id { get; set; }
        public List<RevisionModel> Revisions { get; set; }
    }
}
