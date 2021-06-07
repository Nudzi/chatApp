using Microsoft.ML.Data;

namespace chatModel.Recommender
{
    public class ImageNamePrediction
    {
        //public string ImageName { get; set; }
        [ColumnName("dense_2")]
        public float[] PredictedLabels;

    }
}
