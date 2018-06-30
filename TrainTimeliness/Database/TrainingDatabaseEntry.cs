using Microsoft.ML.Runtime.Api;
using Newtonsoft.Json;
using System;
using TrainTimeliness.Client.Responses;

namespace TrainTimeliness.Database
{
    public class TrainingDatabaseEntry
    {
        [Column("0")]
        public float toleranceValue;
        [Column("1")]
        public float numNotTolerance;
        [Column("2")]
        public float numTolerance;
        [Column(ordinal: "3", name: "Label")]
        public float percentTolerance;
        [Column("4")]
        public string globalTolerance;
        [Column("5")]
        public float dayOfWeek;

        public TrainingDatabaseEntry()
        {

        }

        public TrainingDatabaseEntry(Metric metric, DateTime date)
        {
            toleranceValue = Convert.ToSingle(metric.tolerance_value);
            numNotTolerance = Convert.ToSingle(metric.num_not_tolerance);
            numTolerance = Convert.ToSingle(metric.num_tolerance);
            percentTolerance = Convert.ToSingle(metric.percent_tolerance);
            globalTolerance = metric.global_tolerance.ToString();
            this.dayOfWeek = Convert.ToSingle((int)date.DayOfWeek);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}