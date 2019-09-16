using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL2.Documents
{
    public class FeedComment
    {
        public string Id { get; set; }
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
