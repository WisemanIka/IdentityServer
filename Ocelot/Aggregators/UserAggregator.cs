using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace Ocelot.Gateway.Aggregators
{
    public class UserAggregator : IDefinedAggregator
    {
        public Task<DownstreamResponse> Aggregate(List<DownstreamResponse> responses)
        {
            Console.WriteLine("This should be written but isn't");
            return new Task<DownstreamResponse>(() => responses[0]);
        }
    }
}
