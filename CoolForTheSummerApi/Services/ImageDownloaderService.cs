using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;


namespace CoolForTheSummerApi.Services
{
    public class ImageDownloaderService : IImageDownloaderService
    {
        public ImageDownloaderService()
        {

        }

        // Yes Mike, I know this is terrible but 4Chan won't let me directly hotlink their image links so I did this to save time, fuck you
        public string DownloadImageToBase64(string url)
        {
            var fileName = "";
            url = url.Substring(2);
            var regex = new Regex("\\/.*?\\.");
            fileName = regex.Match(url).Value;
            regex = new Regex("\\/");
            fileName = regex.Replace(fileName, "");
            fileName = fileName + "jpg";
            url = "http://" + url;

            var base64Image = "";

            using (WebClient wc = new WebClient())
            {
                byte[] bytes = wc.DownloadData(url);
                MemoryStream ms = new MemoryStream(bytes);
                var imageBytes = ms.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }

            return base64Image;
        }
    }
}
