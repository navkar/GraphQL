using GraphQL.Types;
using GraphQL2.Documents;

namespace GraphQL2.Types
{
    public class FeedCommentType : ObjectGraphType<FeedComment>
    {
        public FeedCommentType()
        {
            Field(t => t.Id);
            Field(t => t.CreatedOn);
            Field(t => t.ProfilePhoto);
            Field(t => t.UserName);
            Field(t => t.CommentText);
        }
    }
}
