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
        private string _siteUrl = "http://www.4chan.org/";
        public string[] QueryTerms { get; } = { "Ocean", "Nature", "Pollution" };

        public WebScraperService()
        {

        }

        public async Task<string> ScrapeForPost(string board)
        {
            _siteUrl += board + "/";
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "fuck MooT"); 
            HttpResponseMessage request = await httpClient.GetAsync(_siteUrl);
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

            return threadDivs.ToList()[random.Next(threadDivs.Count())].InnerHtml;
        }
    }
}
