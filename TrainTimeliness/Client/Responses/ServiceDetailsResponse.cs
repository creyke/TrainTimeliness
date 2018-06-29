namespace TrainTimeliness.Client.Responses
{
    public class ServiceDetailsResponse
    {
        public Serviceattributesdetails serviceAttributesDetails { get; set; }
    }

    public class Serviceattributesdetails
    {
        public string date_of_service { get; set; }
        public string toc_code { get; set; }
        public string rid { get; set; }
        public Location[] locations { get; set; }
    }

    public class Location
    {
        public string location { get; set; }
        public string gbtt_ptd { get; set; }
        public string gbtt_pta { get; set; }
        public string actual_td { get; set; }
        public string actual_ta { get; set; }
        public string late_canc_reason { get; set; }
    }
}
