﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolForTheSummerApi.Services
{
    public interface IImageDownloaderService
    {
        public string DownloadImageToBase64(string url);
    }
}
