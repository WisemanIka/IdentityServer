using System;
using Fox.Common.Configurations;
using Nest;

namespace Fox.Common.Providers
{
    public class ElasticSearchProvider
    {
        public ElasticClient Client { get; set; }
        public ConfigurationDocument ConfigurationDocument { get; set; }

        public ElasticSearchProvider(ConfigurationDocument configurationDocument, string defaultIndex)
        {
            var elasticSearchConfiguration = configurationDocument.GetAs<ConfigurationDocument>("ElasticSearch");
            var connectionString = elasticSearchConfiguration.GetAs<string>("ConnectionString");
            var username = elasticSearchConfiguration.GetAs<string>("Username");
            var password = elasticSearchConfiguration.GetAs<string>("Password");

            var connectionSettings = new ConnectionSettings(new Uri(connectionString)).DefaultIndex(defaultIndex);

            connectionSettings.BasicAuthentication(username, password);

            this.Client = new ElasticClient(connectionSettings);
            this.Client.ConnectionSettings.DefaultIndices.Add(typeof(string), defaultIndex);
        }
    }
}
