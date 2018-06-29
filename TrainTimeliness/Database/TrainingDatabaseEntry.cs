using Microsoft.ML.Runtime.Api;
using Newtonsoft.Json;
using System;
using TrainTimeliness.Client.Responses;

namespace TrainTimeliness.Database
{
    public class TrainingDatabaseEntry
    {
        [Column("0")]
        public float tolerance_value;
        [Column("1")]
        public float num_not_tolerance;
        [Column("2")]
        public float num_tolerance;
        [Column("3")]
        public float percent_tolerance;
        [Column("4")]
        public string global_tolerance;
        [Column("5")]
        public float day_of_week;

        public TrainingDatabaseEntry()
        {

        }

        public TrainingDatabaseEntry(Metric metric, DateTime date)
        {
            tolerance_value = Convert.ToSingle(metric.tolerance_value);
            num_not_tolerance = Convert.ToSingle(metric.num_not_tolerance);
            num_tolerance = Convert.ToSingle(metric.num_tolerance);
            percent_tolerance = Convert.ToSingle(metric.percent_tolerance);
            global_tolerance = metric.global_tolerance.ToString();
            this.day_of_week = Convert.ToSingle((int)date.DayOfWeek);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}