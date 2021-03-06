﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Services
{
    public interface IWebScraperService
    {
        public Task<IElement> ScrapeForPost(string board);
    }
}
