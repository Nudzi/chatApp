using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.Friends;
using chatModel.Requests.Histories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Microsoft.ML.HalLearners;
using Android.Renderscripts;
using Microsoft.ML.OnnxRuntime;
using chatModel.Recommender;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;
using Microsoft.ML;

namespace chatApp.Mobile.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");
        private readonly APIService _historiesService = new APIService("histories");
        public ObservableCollection<HistoryList> Histories { get; set; } = new ObservableCollection<HistoryList>();
        private InferenceSession _session = null;
        static string ONNX_MODEL_PATH = "example.onnx";
        public ChatViewModel()
        {
        }
        public FriendList FriendList { get; set; }
        public string FriendlyName { get; set; }
        public byte[] byteImage { get; set; }
        public Xamarin.Forms.Image imageXamarn { get; set; }
        public async Task Init()
        {
            try
            {
                //ScriptRuntime ipy = Python.CreateRuntime();
                ////var onnxPredictionPipeline = mlContext.Transforms.ApplyOnnxModel(
                ////    outputColumnNames: outputColumns,
                ////    inputColumnNames: inputColumns,
                ////    ONNX_MODEL_PATH);

                //var emptyDv = mlContext.Data.LoadFromEnumerable(new OnnxInput[] { });

                ////onnxPredictionPipeline.Fit(emptyDv);

                //string file = "C://Users/korisnik/Desktop/example.onnx";
                //_session = new InferenceSession(file);

                HistoriesSearchRequest historiesSearchRequest = new HistoriesSearchRequest
                {
                    UserIdSecondary = Global.LoggedUser.Id,
                    UserIdPrimary = FriendList.UserIdSecondary,
                    Status = false
                };
                var historyListUnseen = await _historiesService.Get<List<Histories>>(historiesSearchRequest);
                foreach (var item in historyListUnseen)
                {
                    item.Status = true;
                    await _historiesService.Update<Histories>(item.Id, item);
                }


                var historylist = await _historiesService.Get<List<Histories>>(null);
                var histories = new List<Histories>();
                foreach (var history in historylist)
                {
                    if (history.UserIdPrimary == Global.LoggedUser.Id || history.UserIdPrimary == FriendList.UserIdSecondary)
                    {
                        if (history.UserIdSecondary == Global.LoggedUser.Id || history.UserIdSecondary == FriendList.UserIdSecondary)
                        {
                            histories.Add(history);
                        }
                    }
                }

                Histories.Clear();
                foreach (var history in histories)
                {
                    HistoryList tmp = new HistoryList
                    {
                        Id = history.Id,
                        Image = history.Image,
                        ImageThumb = history.ImageThumb,
                        Message = history.Message,
                        ModifiedDate = history.ModifiedDate,
                        Status = history.Status,
                        UserIdPrimary = history.UserIdPrimary,
                        UserIdSecondary = history.UserIdSecondary,
                        isPrimary = false,
                        isVisibleImage = true,
                        isVisibleMessage = true
                    };
                    if (history.Image.Length == 0)
                        tmp.isVisibleImage = false;
                    if (history.Message == null)
                        tmp.isVisibleMessage = false;
                    if (tmp.UserIdPrimary == Global.LoggedUser.Id)
                        tmp.isPrimary = true;
                    Histories.Add(tmp);

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async Task SaveMessage(string message)
        {
            try
            {
                HistoriesUpsertRequest historiesUpsertRequest = new HistoriesUpsertRequest
                {
                    Image = byteImage,
                    ImageThumb = byteImage,
                    Message = message,
                    UserIdPrimary = Global.LoggedUser.Id,
                    UserIdSecondary = FriendList.UserIdSecondary,
                    ModifiedDate = DateTime.Now,
                    Status = false
                };
                await _historiesService.Insert<Histories>(historiesUpsertRequest);
                FileInfo fInfo = new FileInfo("example.onnx");
                if (fInfo.Exists)
                {
                    string imagesFolder = fInfo.DirectoryName + "/pictures";
                    //System.Drawing.Image mojaSlika = System.Drawing.Image.FromFile(imagesFolder + "/chair.png");
                    //Bitmap bitmap = new Bitmap(mojaSlika, new System.Drawing.Size(224, 224));


                    //MLContext mlContext = new MLContext();
                    //IEnumerable<ImageNetData> images = ImageNetData.ReadFromFile(imagesFolder, ConvertByteArrayToFloat(byteImage));
                    //IDataView imageDataView = mlContext.Data.LoadFromEnumerable(images);

                    //var modelScorer = new OnnxModelScorer(imagesFolder, ONNX_MODEL_PATH, mlContext);

                    //// Use model to score data
                    //IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);
                    //YoloOutputParser parser = new YoloOutputParser();

                    //var boundingBoxes =
                    //    probabilities
                    //    .Select(probability => parser.ParseOutputs(probability))
                    //    .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

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

                    //var outputFolder = fInfo.DirectoryName + "/pictures2";
                    //for (var i = 0; i < images.Count(); i++)
                    //{
                    //    string imageFileName = images.ElementAt(i).Label;
                    //    IList<YoloBoundingBox> detectedObjects = boundingBoxes.ElementAt(i);
                    //    DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);
                    //    LogDetectedObjects(imageFileName, detectedObjects);
                    //    Console.WriteLine("========= End of Process..Hit any Key ========");
                    //    Console.ReadLine();
                    //    await Application.Current.MainPage.DisplayAlert("Error", detectedObjects.ToString(), "OK");
                    //}

                    //var something = mlContext.Transforms.ApplyOnnxModel(ONNX_MODEL_PATH);
                    //var onnxPredictionPipeline = GetPredictionPipeline(mlContext);
                    //var onnxPredictionEngine = mlContext.Model.CreatePredictionEngine<OnnxInput, OnnxOutput>(onnxPredictionPipeline);

                    //var testInput = new OnnxInput
                    //{
                    //    //grid =
                    //};

                    //var prediction = onnxPredictionEngine.Predict(testInput);
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
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
            public ITransformer GetPredictionPipeline(MLContext mlContext)
        {
            string[] inputColumns = new string[] { byteImage.ToString() };

            var outputColumns = new string[] { "grid" };

            var onnxPredictionPipeline = mlContext
                .Transforms
                .ApplyOnnxModel(
                outputColumnNames: outputColumns,
                inputColumnNames: inputColumns,
                ONNX_MODEL_PATH);

            var emptyDv = mlContext.Data.LoadFromEnumerable(new OnnxInput[] { });

            return onnxPredictionPipeline.Fit(emptyDv);
        }
        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
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