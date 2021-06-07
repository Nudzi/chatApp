using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using System;
using System.Collections.Generic;
using System.Text;

namespace chatModel.Recommender
{
    public class OnnxInput
    {
        [ColumnName("grid")]
        public float[] PredictedLabels;

    }
}
