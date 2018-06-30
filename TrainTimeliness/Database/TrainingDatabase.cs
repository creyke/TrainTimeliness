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

                await writer.WriteLineAsync($@"{nameof(e.toleranceValue)},{nameof(e.numNotTolerance)},{nameof(e.numTolerance)},{nameof(e.percentTolerance)},{nameof(e.globalTolerance)},{nameof(e.dayOfWeek)}");

                foreach (var entry in entries)
                {
                    await writer.WriteLineAsync($@"{entry.toleranceValue},{entry.numNotTolerance},{entry.numTolerance},{entry.percentTolerance},{entry.globalTolerance},{entry.dayOfWeek}");
                }
            }
        }
    }
}