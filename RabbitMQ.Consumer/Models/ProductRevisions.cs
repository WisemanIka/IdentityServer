using System;
using System.Collections.Generic;

namespace RabbitMQ.Consumer.Models
{
    public class ProductRevisions
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<object> Revisions { get; set; }
    }
}
