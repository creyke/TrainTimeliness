using Newtonsoft.Json;
using System;
using TrainTimeliness.Client.Responses;

namespace TrainTimeliness.Database
{
    public class TrainingDatabaseEntry : Metric
    {
        public DateTime date { get; set; }
        public DayOfWeek dayOfWeek { get; set; }

        public TrainingDatabaseEntry(Metric metric, DateTime date)
        {
            global_tolerance = metric.global_tolerance;
            num_not_tolerance = metric.num_not_tolerance;
            num_tolerance = metric.num_tolerance;
            percent_tolerance = metric.percent_tolerance;
            tolerance_value = metric.tolerance_value;
            this.date = date;
            this.dayOfWeek = date.DayOfWeek;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}