namespace TrainTimeliness.Client.Responses
{
    public class ServiceMetricsResponse
        {
        public Header header { get; set; }
        public Service[] Services { get; set; }
    }

    public class Header
    {
        public string from_location { get; set; }
        public string to_location { get; set; }
    }

    public class Service
    {
        public Serviceattributesmetrics serviceAttributesMetrics { get; set; }
        public Metric[] Metrics { get; set; }
    }

    public class Serviceattributesmetrics
    {
        public string origin_location { get; set; }
        public string destination_location { get; set; }
        public string gbtt_ptd { get; set; }
        public string gbtt_pta { get; set; }
        public string toc_code { get; set; }
        public string matched_services { get; set; }
        public string[] rids { get; set; }
    }

    public class Metric
    {
        public string tolerance_value { get; set; }
        public string num_not_tolerance { get; set; }
        public string num_tolerance { get; set; }
        public string percent_tolerance { get; set; }
        public bool global_tolerance { get; set; }
    }
}
