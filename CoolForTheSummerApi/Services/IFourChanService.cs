using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Services
{
    public interface IFourChanService
    {
        public Task<PostViewModel> GetRandomPostFromRandomBoard();
        public Task<PostViewModel> GetRandomPostFromSpecifiedBoard(string board);
    }
}
