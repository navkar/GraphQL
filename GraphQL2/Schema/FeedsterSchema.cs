using GraphQL;
using GraphQL2.Queries;
using GraphQLSchema = global::GraphQL.Types.Schema;

namespace GraphQL2.Schema
{
    public class FeedsterSchema : GraphQLSchema
    {
        public FeedsterSchema(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<FeedCommentQuery>();
        }
    }
}
