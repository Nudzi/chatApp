using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace chatModel.Recommender
{
    //public class ImageNetData
    //{
    //    public byte[] Image { get; set; }
    //    //public static IEnumerable<ImageNetData> ReadFromFile(string imageFolder)
    //    //{
    //    //    return Directory
    //    //        .GetFiles(imageFolder)
    //    //        .Where(filePath => Path.GetExtension(filePath) != ".md")
    //    //        .Select(filePath => new ImageNetData { ImagePath = filePath, Label = Path.GetFileName(filePath) });
    //    //}
    //}
    public class ImageNetData
    {
        [ColumnName("input_1"), VectorType(1, 224, 224, 3)]
        public float[] SamplingKeyColumn { get; set; }
        [LoadColumn(0)]
        public string ImagePath;

        [LoadColumn(1)]
        public string Label;

        public static IEnumerable<ImageNetData> ReadFromFile(string imageFolder, float[] image)
        {
            return Directory
                .GetFiles(imageFolder)
                .Where(filePath => Path.GetExtension(filePath) != ".md")
                .Select(filePath => new ImageNetData { SamplingKeyColumn = image, ImagePath = filePath, Label = Path.GetFileName(filePath) });
        }
    }
}
