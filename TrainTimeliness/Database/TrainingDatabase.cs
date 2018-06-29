using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTimeliness.Database
{
    public class TrainingDatabase
    {
        public List<TrainingDatabaseEntry> entries;
        
        public TrainingDatabase()
        {
            entries = new List<TrainingDatabaseEntry>();
        }

        public void AddEntry(TrainingDatabaseEntry entry)
        {
            entries.Add(entry);
        }

        public async Task SaveAsync(string path)
        {
            using (var writer = new StreamWriter(path))
            {
                var e = entries.First();

                await writer.WriteLineAsync($@"{nameof(e.tolerance_value)},{nameof(e.num_not_tolerance)},{nameof(e.num_tolerance)},{nameof(e.percent_tolerance)},{nameof(e.global_tolerance)},{nameof(e.day_of_week)}");

                foreach (var entry in entries)
                {
                    await writer.WriteLineAsync($@"{entry.tolerance_value},{entry.num_not_tolerance},{entry.num_tolerance},{entry.percent_tolerance},{entry.global_tolerance},{entry.day_of_week}");
                }
            }
        }
    }
}