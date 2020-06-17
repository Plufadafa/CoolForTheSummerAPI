using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Mappers
{
    public class PostMapper : IPostMapper
    {
        private string _board;

        public PostViewModel Map(IElement element, string board)
        {
            _board = board;
            var spanValues = GetSpanValues(element);
            var hrefValues = GetHrefValues(element);

            return new PostViewModel()
            {
                Board = board,
                Name = spanValues.name,
                PostTimeAndDate = spanValues.postTimeAndDate,
                PostNumber = spanValues.postNumber,
                ThreadHref = hrefValues.threadHref,
                FileHref = hrefValues.fileHref,
                FileThumbHref = hrefValues.fileThumbHref,
                FileThumbStyles = hrefValues.fileThumbStyles,
                PostMessage = GetMessage(element),
                FileThumbBase64 = "",
                FileBase64 = "",
            };
        }

        private (string name, string postTimeAndDate, string postNumber) GetSpanValues(IElement element)
        {
            var spans = element.GetElementsByTagName("span");
            var name =  spans.First(s => s.HasAttribute("class") && s.GetAttribute("class").Equals("name")).TextContent;
            var postTimeAndDate = spans.First(s => s.HasAttribute("class") && s.GetAttribute("class").StartsWith("dateTime")).TextContent;
            var postNumber = spans.First(s => s.HasAttribute("class") && s.GetAttribute("class").StartsWith("dateTime")).Children.First(c => c.HasAttribute("title") && c.GetAttribute("title").Equals("Reply to this post")).TextContent;

            return (name: name, postTimeAndDate: postTimeAndDate, postNumber: postNumber);
        }

        private (string threadHref, string fileHref, string fileThumbHref, string fileThumbStyles) GetHrefValues(IElement element)
        {
            var hyperlinks = element.GetElementsByTagName("a");
            var threadHref = hyperlinks.First(h => h.HasAttribute("title") && h.GetAttribute("title").Equals("Link to this post")).GetAttribute("href");
            threadHref = "boards.4chan.org/" + _board + "/"+ threadHref;

            var fileElement = element.GetElementsByTagName("div")
                .FirstOrDefault(d => d.HasAttribute("class") && d.GetAttribute("class").Equals("file"));

            var fileHref = "";
            var fileThumbHref = "";
            var fileThumbStyles = "";

            if (fileElement != null)
            {
                var fileHrefs = fileElement.GetElementsByTagName("a");
                var fileThumb = fileHrefs.First(f =>
                    f.HasAttribute("class") && f.GetAttribute("class").Equals("fileThumb"));
                fileHref = fileThumb.GetAttribute("href");
                fileThumbHref = fileThumb.GetElementsByTagName("img").First().GetAttribute("src");
                fileThumbStyles = fileThumb.GetElementsByTagName("img").First().GetAttribute("style");
            }

            return (threadHref: threadHref, fileHref: fileHref, fileThumbHref: fileThumbHref, fileThumbStyles: fileThumbStyles);
        }

        private string GetMessage(IElement element)
        {
            var rawMessageHtml = element.GetElementsByTagName("blockquote").First().InnerHtml;
            rawMessageHtml = RemoveClass(rawMessageHtml);
            rawMessageHtml = FormatAllHrefInMessage(rawMessageHtml);

            return rawMessageHtml;
        }

        private string RemoveClass(string s)
        {
            Regex regex = new Regex("class=\".*?\"");
            return regex.Replace(s, string.Empty);
        }

        private string FormatAllHrefInMessage(string message)
        {
            Regex regex = new Regex("href=\"\\/" + _board + ".*?" + "\"");
            var match = regex.Match(message);
            if (match.Success)
            {
                var replacementHref = match.Value.Substring(6);
                replacementHref = "href=\"https://boards.4chan.org" + replacementHref;
                replacementHref = replacementHref + " " + "target=\"_blank\"";
                message = regex.Replace(message, replacementHref, 1);
                return FormatAllHrefInMessage(message);
            }
            return message;
        }
    }
}
