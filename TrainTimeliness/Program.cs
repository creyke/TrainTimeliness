using System.Linq;
using System.Threading.Tasks;
using TrainTimeliness.Client;
using TrainTimeliness.Client.Requests;

namespace TrainTimeliness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = Config.Load("Config.json");

            var client = new HspClient(config.HspClientBaseUrl, config.NationalRailDataPortalUsername, config.NationalRailDataPortalPassword);

            var metricsResponse = await client.ServiceMetricsAsync(new ServiceMetricsRequest
            {
                from_loc = "BTN",
                to_loc = "VIC",
                from_time = "0700",
                to_time = "0800",
                from_date = "2016-07-01",
                to_date = "2016-08-01",
                days = "WEEKDAY"
            });

            var detailsResponse = await client.ServiceDetailsAsync(new ServiceDetailsRequest
            {
                rid = metricsResponse.Services.First().serviceAttributesMetrics.rids.First()
            });
        }
    }
}
