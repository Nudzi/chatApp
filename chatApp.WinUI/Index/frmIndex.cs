using chatModel.Recommender;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace chatApp.WinUI.Index
{
    public partial class frmIndex : Form
    {
        static string ONNX_MODEL_PATH = "example.onnx";
        public frmIndex()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                FileInfo fInfo = new FileInfo("example.onnx");
                if (fInfo.Exists)
                {
                    string imagesFolder = fInfo.DirectoryName + "/pictures";
                    //System.Drawing.Image mojaSlika = System.Drawing.Image.FromFile(imagesFolder + "/chair.png");
                    //Bitmap bitmap = new Bitmap(mojaSlika, new System.Drawing.Size(224, 224));


                    MLContext mlContext = new MLContext();
                    IEnumerable<ImageNetData> images = ImageNetData.ReadFromFile(imagesFolder);
                    IDataView imageDataView = mlContext.Data.LoadFromEnumerable(images);

                    var modelScorer = new OnnxModelScorer(imagesFolder, ONNX_MODEL_PATH, mlContext);

                    //// Use model to score data
                    IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);
                    YoloOutputParser parser = new YoloOutputParser();

                    var boundingBoxes =
                        probabilities
                        .Select(probability => parser.ParseOutputs(probability))
                        .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

                    //// just predict

                    ////ITransformer transform = modelScorer.Load(imageDataView);
                    ////var onnxPredictionEngine = mlContext.Model.CreatePredictionEngine<ImageNetData, ImageNamePrediction>(transform);


                    ////ImageNetData imageNetData = new ImageNetData
                    ////{
                    ////    SamplingKeyColumn = ConvertByteArrayToFloat(byteImage)
                    ////};
                    ////float[] mojNoviFloat = new float[150528];
                    ////var somethig = imageNetData.SamplingKeyColumn.Length;
                    ////float[] newMine = ConvertByteArrayToFloat(byteImage).Take(150528).ToArray();
                    ////Array.Resize(ref byteImage, 1024);
                    ////var testInput = new ImageNetData
                    ////{
                    ////    SamplingKeyColumn = ConvertByteArrayToFloat(byteImage)
                    ////};
                    ////var prediction = onnxPredictionEngine.Predict(testInput);

                    var outputFolder = fInfo.DirectoryName + "/pictures2";
                    for (var i = 0; i < images.Count(); i++)
                    {
                        string imageFileName = images.ElementAt(i).Label;
                        IList<YoloBoundingBox> detectedObjects = boundingBoxes.ElementAt(i);
                        DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);
                        LogDetectedObjects(imageFileName, detectedObjects);
                        Console.WriteLine("========= End of Process..Hit any Key ========");
                        Console.ReadLine();
                        MessageBox.Show("Error", detectedObjects.ToString(), MessageBoxButtons.OK);
                    }



                    //ScriptRuntime ipy = Python.CreateRuntime();
                    //ScriptEngine engine = Python.CreateEngine();
                    //ScriptSource source = engine.CreateScriptSourceFromFile("modelLoader.py");
                    //ScriptScope scope = engine.CreateScope();
                    //source.Execute(scope);

                    ////dynamic Calculator = scope.GetVariable("Calculator");
                    ////dynamic calc = Calculator();
                    ////int result = calc.add(4, 5);


                    //dynamic Models = scope.GetVariable("Models");
                    //dynamic mdl = Models();
                    //string imagePath = "/Users/korisnik/Desktop/chair.png";
                    //string image = mdl.prediction(imagePath);

                    //MessageBox.Show(image, "result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong username or password", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static float[] ConvertByteArrayToFloat(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            if (bytes.Length % 4 != 0)
                throw new ArgumentException
                      ("bytes does not represent a sequence of floats");

            return Enumerable.Range(0, bytes.Length / 4)
                             .Select(i => BitConverter.ToSingle(bytes, i * 4))
                             .ToArray();
        }
        private void DrawBoundingBox(string inputImageLocation, string outputImageLocation, string imageName, IList<YoloBoundingBox> filteredBoundingBoxes)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path.Combine(inputImageLocation, imageName)); ;

            var originalImageHeight = image.Height;
            var originalImageWidth = image.Width;

            foreach (var box in filteredBoundingBoxes)
            {
                var x = (uint)Math.Max(box.Dimensions.X, 0);
                var y = (uint)Math.Max(box.Dimensions.Y, 0);
                var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                x = (uint)originalImageWidth * x / OnnxModelScorer.ImageNetSettings.imageWidth;
                y = (uint)originalImageHeight * y / OnnxModelScorer.ImageNetSettings.imageHeight;
                width = (uint)originalImageWidth * width / OnnxModelScorer.ImageNetSettings.imageWidth;
                height = (uint)originalImageHeight * height / OnnxModelScorer.ImageNetSettings.imageHeight;
                string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";
                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Define Text Options
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new SolidBrush(System.Drawing.Color.Black);
                    System.Drawing.Point atPoint = new System.Drawing.Point((int)x, (int)y - (int)size.Height - 1);

                    // Define BoundingBox options
                    Pen pen = new Pen(box.BoxColor, 3.2f);
                    SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                    thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

                    // Draw bounding box on image
                    thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                }
            }
            if (!Directory.Exists(outputImageLocation))
            {
                Directory.CreateDirectory(outputImageLocation);
            }

            image.Save(Path.Combine(outputImageLocation, imageName));
        }
        private static void LogDetectedObjects(string imageName, IList<YoloBoundingBox> boundingBoxes)
        {
            Console.WriteLine($".....The objects in the image {imageName} are detected as below....");

            foreach (var box in boundingBoxes)
            {
                Console.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
            }

            Console.WriteLine("");
        }
    }
}
