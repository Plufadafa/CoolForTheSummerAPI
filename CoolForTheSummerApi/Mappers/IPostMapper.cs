using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using CoolForTheSummerApi.Models;

namespace CoolForTheSummerApi.Mappers
{
    public interface IPostMapper
    {
        public PostViewModel Map(IElement element, string board);
    }
}
