using Newtonsoft.Json;
using System.IO;

namespace TrainTimeliness
{
    public class Config
    {
        public string NationalRailDataPortalUsername { get; set; }
        public string NationalRailDataPortalPassword { get; set; }
        public string HspClientBaseUrl { get; set; }
        public string DatabasePath { get; set; }
        public string ModelPath { get; set; }
        public bool RebuildDatabase { get; set; }
        public bool RebuildModel { get; set; }

        public static Config Load(string file)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(file));
        }
    }
}
