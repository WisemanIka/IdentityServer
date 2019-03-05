using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task IdentityServerClientTest()
        {
            var client = new HttpClient();

            var discover = await client.GetDiscoveryDocumentAsync("http://localhost:8080");
            if (discover.IsError)
            {
                _testOutputHelper.WriteLine(discover.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discover.TokenEndpoint,
                ClientId = "Angular",
                ClientSecret = "c0359956-eb75-480b-adde-2c33de5f3900",
                Scope = "BasketAPI"
            });

            if (tokenResponse.IsError)
            {
                _testOutputHelper.WriteLine(tokenResponse.Error);
                return;
            }

            var tokeResponseSerialize = JsonConvert.SerializeObject(tokenResponse.Json);
            _testOutputHelper.WriteLine(tokeResponseSerialize);
            _testOutputHelper.WriteLine("\n\n");
        }
    }
}
