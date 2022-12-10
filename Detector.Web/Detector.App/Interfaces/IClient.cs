
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Detector.Interfaces
{
    public interface IDetectorClient
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> CreateUserAsync(string username, string password);
        Task<bool> LogoutAsync();
    }
}
