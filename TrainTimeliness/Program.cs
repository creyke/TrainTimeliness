using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.IO;
using System.Threading.Tasks;
using TrainTimeliness.Client;
using TrainTimeliness.Client.Requests;
using TrainTimeliness.Database;
using TrainTimeliness.Model;

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

            if (config.RebuildModel)
            {
                await RebuildModelAsync(config);
            }

            Console.Read();
        }

        private static async Task RebuildDatabaseAsync(Config config)
        {
            Console.WriteLine("RebuildDatabase:");

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

            Console.WriteLine($"  Saving database to '{config.DatabasePath}'...");

            if (File.Exists(config.DatabasePath))
            {
                File.Delete(config.DatabasePath);
            }

            await database.SaveAsync(config.DatabasePath);

            Console.WriteLine("  Database rebuilt.");
        }

        private static async Task RebuildModelAsync(Config config)
        {
            Console.WriteLine("RebuildModel:");

            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader(config.DatabasePath).CreateFrom<TrainingDatabaseEntry>(useHeader: true, separator: ','));

            var e = new TrainingDatabaseEntry();

            pipeline.Add(new CategoricalOneHotVectorizer(nameof(e.globalTolerance)));

            pipeline.Add(new ColumnConcatenator("Features", nameof(e.toleranceValue),nameof(e.numNotTolerance),nameof(e.numTolerance),nameof(e.percentTolerance),nameof(e.globalTolerance), nameof(e.dayOfWeek)));

            pipeline.Add(new FastTreeBinaryClassifier() { NumLeaves = 5, NumTrees = 5, MinDocumentsInLeafs = 2 });

            var model = pipeline.Train<TrainingDatabaseEntry, TimelinessPrediction>();

            Console.WriteLine($"  Saving model to '{config.ModelPath}'...");

            await model.WriteAsync(config.ModelPath);

            Console.WriteLine("  Model rebuilt.");
        }
    }
}
