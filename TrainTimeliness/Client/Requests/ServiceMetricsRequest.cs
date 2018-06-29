namespace TrainTimeliness.Client.Requests
{
    public class ServiceMetricsRequest
    {
        public string from_loc { get; set; }
        public string to_loc { get; set; }
        public string from_time { get; set; }
        public string to_time { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string days { get; set; }
    }
}
