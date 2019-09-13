using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL2.Entities
{
    public class SocialNetwork
    {
        public SNType Type { get; set; }
        public string NickName { get; set; }
        public string Url { get; set; }
        public Author Author { get; set; }
    }
    public enum SNType
    {
        INSTAGRAM, TWITTER, YOUTUBE
    }
}
