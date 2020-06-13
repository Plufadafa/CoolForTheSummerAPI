using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Services
{
    public interface IFourChanService
    {
        public Task<string> GetRandomPostFromRandomBoard();
        public Task<string> GetRandomPostFromBoard(string board);
    }
}
