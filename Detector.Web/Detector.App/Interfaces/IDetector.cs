﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Detector.Interfaces
{
    public interface IDetector
    {
        Task<bool> DetectAsync(string fileName);
    }
}
