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
        public string ThreadHRef { get; set; } //Within the dateTime span, an aHref
        
    }
}
