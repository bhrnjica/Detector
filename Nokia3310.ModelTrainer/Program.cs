using Microsoft.ML;
using Microsoft.ML.Vision;
using Nokia3310.ModelTrainer.MLModel;
using Nokia3310Detector.ML.Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nokia3310.ModelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            //get full path for th relative folder
            var relativeFolderPath = "../../../../image_set";

            var loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo _dataRoot = new FileInfo(loc);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeFolderPath);

            //begin building model process
            var mlContext = new MLContext(seed: 20200525);
            //we need to crate logger of ML.NET context in order to know what is heppening in it.
            mlContext.Log += MlContext_Log;


            ModelBuilder model = new ModelBuilder(mlContext, fullPath);

            var imageSet = LoadImageSet(fullPath+"/train");


            //
            //Definition of the hype-parameters
            var opt = new ImageClassificationTrainer.Options()
            {
                //Feature and Label name
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                // Just by changing/selecting InceptionV3/MobilenetV2/ResnetV250  
                // you can try a different DNN architecture (TensorFlow pre-trained model). 
                Arch = ImageClassificationTrainer.Architecture.InceptionV3,

                //epoch
                Epoch = 500,
                LearningRate = 0.01f,

                //logging training progress
                MetricsCallback = (m) =>
                {
                    if (m.Train != null && m.Train.Epoch != 0 && m.Train.Epoch % 10 == 0)
                    {
                        Console.WriteLine(m.ToString());
                    }
                    //Console.WriteLine($"Epoch: {m.Train.Epoch}; Accuracy {m.Train.Accuracy}");
                    else if (m.Train == null && m.Bottleneck.Index % 100 == 0)
                        Console.WriteLine($"{m.Bottleneck.DatasetUsed} set, preprocessed {m.Bottleneck.Index} images so far......");

                },
            };
            //
            model.BuildAndTrain(imageSet, opt); 
        }

        private static void MlContext_Log(object sender, LoggingEventArgs e)
        {
            var strMsg = e.Message.Substring(e.Message.LastIndexOf("]") + 1);

            //we want only Info log
            if (e.Message.Contains("Kind=Info"))
                Console.WriteLine(e.Message);
        }


        /// <summary>
        /// Load training images into App memory
        /// </summary>
        /// <param name="folder"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static IEnumerable<ImageData> LoadImageSet(string folder)
        {
            var files = Directory.GetFiles(folder, "*", searchOption: SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    continue;

                // the image label is determined by the parent folder
                var label = Directory.GetParent(file).Name; 

                yield return new ImageData(null, label, file);
            }
        }
    }
}
