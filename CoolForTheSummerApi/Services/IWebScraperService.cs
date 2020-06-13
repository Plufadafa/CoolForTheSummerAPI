using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Services
{
    public interface IWebScraperService
    {
        public Task<string> ScrapeForPost(string board);
    }
}
