using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using CoolForTheSummerApi.Mappers;
using CoolForTheSummerApi.Models;
using Microsoft.OpenApi.Extensions;

namespace CoolForTheSummerApi.Services
{
    public class FourChanService : IFourChanService
    {
        private readonly IWebScraperService _webScraper;
        private readonly Random _random;
        private readonly BoardEnum[] _boards;
        private readonly IPostMapper _postMapper;
        private readonly IImageDownloaderService _imageDownloader;

        public FourChanService(IWebScraperService webScraper, IPostMapper postMapper, IImageDownloaderService imageDownloader)
        {
            _webScraper = webScraper;
            _random = new Random();
            _boards = (BoardEnum[]) Enum.GetValues(typeof(BoardEnum));
            _postMapper = postMapper;
            _imageDownloader = imageDownloader;
        }

        private async Task<PostViewModel> GetRandomPostFromBoard(string board)
        {
            var element = await _webScraper.ScrapeForPost(board);
            var mutableChildList = element.Children.ToList();

            mutableChildList = mutableChildList.Where(c =>
                !(c.InnerHtml.Contains("images omitted") || c.InnerHtml.Contains("replies omitted"))).ToList();

            var divs = new List<IElement>();

            foreach (var child in mutableChildList)
            {
                foreach (var subChild in child.Children)
                {
                    if (subChild.HasAttribute("class") && (subChild.GetAttribute("class").Equals("post op") || subChild.GetAttribute("class").Equals("post reply")))
                    {
                        divs.Add(subChild);
                    }
                }
            }

            var divsCount = divs.Count - 1;
            return _postMapper.Map(divs[_random.Next(divsCount)], board);
        }

        public async Task<PostViewModel> GetRandomPostFromRandomBoard()
        {
            return await GetRandomPostFromBoard(GetRandomBoardStringName());
        }

        public async Task<PostViewModel> GetRandomPostFromSpecifiedBoard(string board)
        {
            var postViewModel = await GetRandomPostFromBoard(board);
            if (postViewModel.FileThumbHref != "" && postViewModel.FileHref != "")
            {
                postViewModel.LocalThumbFileHref = _imageDownloader.DownloadImageToSiteDirectory(postViewModel.FileThumbHref);
                postViewModel.LocalFileHref = _imageDownloader.DownloadImageToSiteDirectory(postViewModel.FileHref);
            }

            return postViewModel;
        }

        private string GetRandomBoardStringName()
        {
            return _boards[_random.Next(_boards.Length)].GetDisplayName().ToLower();
        }
    }
}
