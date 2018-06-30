using Microsoft.ML.Runtime.Api;

namespace TrainTimeliness.Model
{
    public class TimelinessPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool percentTolerance;
    }
}
