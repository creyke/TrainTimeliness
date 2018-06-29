using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
                foreach (var entry in entries)
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(entry));
                }
            }
        }
    }
}