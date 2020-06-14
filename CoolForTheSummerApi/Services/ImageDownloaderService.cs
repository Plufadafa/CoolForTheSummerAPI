using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;

namespace CoolForTheSummerApi.Services
{
    public class ImageDownloaderService : IImageDownloaderService
    {
        public ImageDownloaderService()
        {

        }

        // Yes Mike, I know this is terrible but 4Chan won't let me directly hotlink their image links so I did this to save time, fuck you
        public string DownloadImageToSiteDirectory(string url)
        {
            var localUrl = "";
            using (WebClient client = new WebClient())
            {
                url = url.Substring(2);
                var regex = new Regex("\\/.*?\\.");
                var fileName = regex.Match(url).Value;
                regex = new Regex("\\/");
                fileName = regex.Replace(fileName, "");
                url = "http://" + url;
                localUrl = @"C:\Project\CoolForTheSummerWebsite\cool-for-the-summer-website\src\assets\images\" + fileName + "jpg";
                client.DownloadFile(url, localUrl);
            }

            return localUrl;
        }
    }
}
