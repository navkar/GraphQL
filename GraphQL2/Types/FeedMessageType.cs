using GraphQL.Types;
using GraphQL2.Documents;

namespace GraphQL2.Types
{
    public class FeedMessageType : ObjectGraphType<FeedMessage>
    {
        public FeedMessageType()
        {
            Field(t => t.Id);
            Field(t => t.GroupId);
            Field(t => t.GiphyUrl);
            Field(t => t.FeedText);
            Field<ListGraphType<FeedCommentType>>("comments");
            Field(t => t.CommentCount);
            Field(t => t.CreatedOn);
            Field(t => t.IsSystemMessage);
            Field(t => t.IsFlagged);
            Field(t => t.Lat);
            Field(t => t.Lng);
            Field(t => t.MapUrl);
            Field(t => t.ProfilePhoto);
            Field(t => t.UserId);
            Field(t => t.UserName);
            Field(t => t.IsDeleted);
        }
    }
}
