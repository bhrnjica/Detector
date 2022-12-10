using Microsoft.ML;
using Microsoft.ML.Data;
using Detector.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

using static Microsoft.ML.Transforms.ValueToKeyMappingEstimator;
using Microsoft.ML.Vision;
using Microsoft.ML.Trainers;
namespace Nokia3310.ModelTrainer.MLModel
{
    public class ModelBuilder
    {
        private readonly MLContext mlContext;
        private readonly string fullImagesetFolderPath;
        public ModelBuilder(MLContext cnt, string fullImgFolder)
        {
            mlContext = cnt;
            fullImagesetFolderPath = fullImgFolder;
        }

        /// <summary>
        /// Training model by splitting image set to Train and Validation part manually.
        /// </summary>
        /// <param name="trainValidSet"></param>
        /// <returns></returns>
        public void BuildAndTrain(IEnumerable<ImageData> imageSet, ImageClassificationTrainer.Options hyperParams)
        {
            // 1. Load image information (filenames and labels) in IDataView
            //Load the initial single full Image-Set
            IDataView fullImagesDataset = mlContext.Data.LoadFromEnumerable(imageSet);
            //make image set more random to perform shuffling
            IDataView shuffledFullImageFilePathsDataset = mlContext.Data.ShuffleRows(fullImagesDataset);

           
            // 2. Load images in-memory while applying image transformations 
            IDataView imageDataSet = mlContext.Transforms.Conversion.
                     MapValueToKey(outputColumnName: "LabelAsKey", inputColumnName: "Label", keyOrdinality: KeyOrdinality.ByValue)
                    .Append(mlContext.Transforms.LoadRawImageBytes(
                                                    outputColumnName: nameof(ImageData.Image),
                                                    imageFolder: fullImagesetFolderPath,
                                                    inputColumnName: nameof(ImageData.ImageFileName)))
                    .Fit(shuffledFullImageFilePathsDataset)
                    .Transform(shuffledFullImageFilePathsDataset);


            // 3. Define the model's training pipeline using Transfer Learning using pre-trained Tensor-flow model InceptionV3
            var pipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(hyperParams)
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel",
                                                                      inputColumnName: "PredictedLabel"));

            // 4. Train/create the ML model
            ITransformer trainedModel = pipeline.Fit(imageDataSet);

            // 5. Get the quality metrics (accuracy, etc.)
            IDataView predictionsDataView = trainedModel.Transform(imageDataSet);

            var metrics = mlContext.MulticlassClassification.Evaluate(predictionsDataView, labelColumnName: "LabelAsKey", predictedLabelColumnName: "PredictedLabel");
            ConsoleHelper.PrintMultiClassClassificationMetrics("TensorFlow DNN Transfer Learning", metrics);
            ConsoleHelper.ConsolePrintConfusionMatrix(metrics.ConfusionMatrix);

            //// 6. Save the model to assets/outputs
            var retVal = saveTrainedModel(predictionsDataView, trainedModel);

        }

        /// <summary>
        /// Save trained model
        /// </summary>
        /// <param name="cvDataView"></param>
        /// <param name="trainedMOdel"></param>
        /// <returns></returns>
        private bool saveTrainedModel(IDataView cvDataView, ITransformer trainedMOdel)
        {
            Console.WriteLine("Would you like to save trained model? y- Yes; anykey - No");
            if (Console.Read() == 'y')
            {
                var modelName = $"{fullImagesetFolderPath}/NokiaDetectorModel-v{DateTime.Now.Ticks}.zip";
                mlContext.Model.Save(trainedMOdel, cvDataView.Schema, modelName);
                Console.WriteLine($"Model saved at: {modelName}");
                return true;
            }
            else
            {
                Console.WriteLine($"Model have not saved.");
                return false;
            }
        }

        
    }
}
