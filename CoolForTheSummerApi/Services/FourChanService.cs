using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;
using Microsoft.OpenApi.Extensions;

namespace CoolForTheSummerApi.Services
{
    public class FourChanService : IFourChanService
    {
        private readonly IWebScraperService _webScraper;
        private readonly Random _random;
        private readonly BoardEnum[] _boards;

        public FourChanService(IWebScraperService webScraper)
        {
            _webScraper = webScraper;
            _random = new Random();
            _boards = (BoardEnum[]) Enum.GetValues(typeof(BoardEnum));
        }

        public async Task<string> GetRandomPostFromRandomBoard()
        {
            return await _webScraper.ScrapeForPost(GetRandomBoardStringName());
        }

        public async Task<string> GetRandomPostFromBoard(string board)
        {
            return await _webScraper.ScrapeForPost(board);
        }

        private string GetRandomBoardStringName()
        {
            return _boards[_random.Next(_boards.Length)].GetDisplayName().ToLower();
        }
    }
}
