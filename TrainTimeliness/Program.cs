using System;
using System.Threading.Tasks;
using TrainTimeliness.Client;
using TrainTimeliness.Client.Requests;
using TrainTimeliness.Database;

namespace TrainTimeliness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = Config.Load("Config.json");

            if (config.RebuildDatabase)
            {
                await RebuildDatabaseAsync(config);
            }
        }

        private static async Task RebuildDatabaseAsync(Config config)
        {
            var database = new TrainingDatabase();

            var client = new HspClient(config.HspClientBaseUrl, config.NationalRailDataPortalUsername, config.NationalRailDataPortalPassword);

            var startDate = new DateTime(2018, 01, 01);
            var toDate = new DateTime(2018, 02, 01);

            var request = new ServiceMetricsRequest
            {
                from_loc = "WDB",
                to_loc = "IPS",
                from_time = "0830",
                to_time = "0840",
                days = Days.WEEKDAY
            };

            Console.WriteLine("RebuildDatabase:");

            Console.WriteLine($"  Requesting data...");

            while (startDate < toDate)
            {
                request.from_date = startDate.ToHspDate();
                var nextDay = startDate.AddDays(1);
                request.to_date = nextDay.ToHspDate();

                var metricsResponse = await client.ServiceMetricsAsync(request);

                foreach (var service in metricsResponse.Services)
                {
                    foreach (var metric in service.Metrics)
                    {
                        var entry = new TrainingDatabaseEntry(metric, startDate);
                        Console.WriteLine($"    Adding entry: {entry}");
                        database.AddEntry(entry);
                    }
                }

                startDate = nextDay;
            }

            Console.WriteLine($"  Saving data...");

            await database.SaveAsync(config.DatabaseLocation);

            Console.WriteLine($"  Database rebuilt.");
        }
    }
}
