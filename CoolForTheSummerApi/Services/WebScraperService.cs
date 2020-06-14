using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Services
{
    public class WebScraperService : IWebScraperService
    {
        private string Title { get; set; }
        private string Url { get; set; }
        private readonly string _siteUrl = "http://www.4chan.org/";
        public string[] QueryTerms { get; } = { "Ocean", "Nature", "Pollution" };

        public WebScraperService()
        {

        }

        public async Task<IElement> ScrapeForPost(string board)
        {
            var site = _siteUrl + board + "/";
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "fuck MooT");
            HttpResponseMessage request = await httpClient.GetAsync(site);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            var threadDivs = document.All.Where(m => m.LocalName == "div" &&
                                                 m.HasAttribute("class") &&
                                                 m.GetAttribute("class").StartsWith("thread"));

            threadDivs = threadDivs.Where(t => !t.InnerHtml.Contains("stickyIcon"));

            var random = new Random();
            var threadDivsLength = threadDivs.Count()-1;
            var randomElement = random.Next(threadDivsLength);

            return threadDivs.ToList()[randomElement];
        }
    }
}
