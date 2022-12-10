using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using Detector.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Detector.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task UploadAsync(IFileListEntry file)
        {
            var path = Path.Combine(_environment.ContentRootPath, "image", file.Name);
            var fi = new FileInfo(path);
            if (fi.Exists)
                return;
            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);
            using (FileStream f = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(f);
            }
        }
    }
}
