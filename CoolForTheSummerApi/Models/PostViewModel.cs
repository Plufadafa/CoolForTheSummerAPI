using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolForTheSummerApi.Models
{
    public class PostViewModel
    {
        public string Board { get; set; }
        public string Name { get; set; } //Usually Anonymous. Span class="name"
        public string PostTimeAndDate { get; set; } //Span class="dateTime
        public string PostNumber { get; set; } //Within the dateTime span, an aHref with the title "Reply to this post"
        public string ThreadHref { get; set; } //Within the dateTime span <a>, an aHref
        public string FileHref { get; set; }// within div class="fileText"
        public string FileThumbHref { get; set; }// class="fileThumb"
        public string FileThumbStyles { get; set; }// within the img tag of the fileThumb class <a>
        public string PostMessage { get; set; }//class="postMessage"
        public string LocalFileHref { get; set; }
        public string LocalThumbFileHref { get; set; }
    }
}
