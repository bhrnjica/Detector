using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia3310Detector.ML.Shared.Entities
{
    public class ImagePrediction
    {
        public string PredictedLabel { get; set; }
        public float[] Score { get; set; }
    }

    public class ImageData
    {
        private readonly byte[] _image;
        private readonly string _label;
        private readonly string _imageFileName;
       
        ///
        public ImageData(byte[] image, string label, string imageFileName)
        {
            _image = image;
            _label = label;
            _imageFileName = imageFileName;
        }

        //
        public byte[] Image => _image;
        public string Label => _label;
        public string ImageFileName => _imageFileName;

    }
}
