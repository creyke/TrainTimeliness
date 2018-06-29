using Newtonsoft.Json;
using System.IO;

namespace TrainTimeliness
{
    public class Config
    {
        public string NationalRailDataPortalUsername { get; set; }
        public string NationalRailDataPortalPassword { get; set; }
        public string HspClientBaseUrl { get; set; }

        public static Config Load(string file)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(file));
        }
    }
}
