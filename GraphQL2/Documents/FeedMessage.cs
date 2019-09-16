using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL2.Documents
{
    public class FeedMessage
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string GroupId { get; set; }

        public string UserName { get; set; }

        public string ProfilePhoto { get; set; }

        public string FeedText { get; set; }

        public bool IsSystemMessage { get; set; }

        public bool IsFlagged { get; set; }

        public bool IsDeleted { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string MapUrl { get; set; }

        public string GiphyUrl { get; set; }

        public int CommentCount
        {
            get
            {
                if (Comments == null || Comments.Count <= 0) return 0;
                return Comments.Count;
            }
        }
        public List<FeedComment> Comments { get; set; }
    }
}
