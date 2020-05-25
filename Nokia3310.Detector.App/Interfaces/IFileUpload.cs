
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Nokia3310Detector.Interfaces
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
    }
}
