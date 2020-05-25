
using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;
using Nokia3310Detector.Interfaces;
using Nokia3310Detector.ML.Shared.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nokia3310Detector.Services
{
    public class Detector : IDetector
    {
        private readonly IWebHostEnvironment _environment;
        private readonly MLContext mlContext;
        private readonly PredictionEngine<ImageData, ImagePrediction> mlEngine;

        public Detector(IWebHostEnvironment environment)
        {
            //load model into MLContext
            _environment = environment;
            var path = Path.Combine(_environment.ContentRootPath, "MLModel", "NokiaDetectorModel.zip");

            //create ML.NET prediction engine
            mlContext = new MLContext(seed: 1);
            var mlnetModel = mlContext.Model.Load(path, out _);
            mlEngine = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(mlnetModel);

        }
        public async Task<bool> DetectAsync(string fileName)
        {
            try
            {
                var imgPath = Path.Combine(_environment.ContentRootPath, "image", fileName);

                var imageToPredict = new ImageData(System.IO.File.ReadAllBytes(imgPath), "", imgPath);

                var prediction = await Task.Run(()=> mlEngine.Predict(imageToPredict));

                return prediction.PredictedLabel=="3310";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
