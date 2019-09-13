
## Postman



## GraphQL 

* In graphql-dotnet, each query field (in this case: author, posts and socials) must be represented as a GraphQL.Types.ObjectGraphType in order to encapsulate each subfield definitions.

* The EnumerationGraphType represents the default GraphType handler for enums in graphql-dotnet. The generic class must be provided subsequently along with the exact type name as a string. 

```csharp
using GraphQL.Types;
using GraphQL_SimpleTalk.Entities;
namespace GraphQL_SimpleTalk.Queries.Types
{
    public class SNTypeType : EnumerationGraphType<SNType>
    {
        public SNTypeType()
        {
            Name = "SNTypeType";
        }
    }
}
```

For lists and arrays, in addition, you must use ListGraphType as the default handler. Notice, too, that the Categories field was defined as non-nullable, another possible config you’re going to use for testing.

```csharp
using GraphQL.Types;
using GraphQL_SimpleTalk.Entities;
namespace GraphQL_SimpleTalk.Queries.Types
{
    public class PostType : ObjectGraphType<Post>
    {
        public PostType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
            Field(x => x.Url);
            Field(x => x.Date);
            Field(x => x.Description);
            Field<AuthorType>("author");
            Field<RatingType>("rating");
            Field<ListGraphType<CommentType>>("comments");
            Field(x => x.Categories, nullable: true);
        }
    }
}
```

### Author Query

* The first important impression is the new arguments field defining a new `QueryArguments` for the id of an author represented as an `IntGraphType`.
* The code snippet `var id = context.GetArgument<int>("id");` is responsible for retrieving this argument based on its previous definition.

```csharp
using GraphQL.Types;
using GraphQL_SimpleTalk.Services;
using GraphQL_SimpleTalk.Queries.Types;
namespace GraphQL_SimpleTalk.Queries
{
    public class AuthorQuery : ObjectGraphType
    {
        public AuthorQuery(BlogService blogService)
        {
            Field<AuthorType>(
                name: "author",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return blogService.GetAuthorById(id);
                }
            );
            Field<ListGraphType<PostType>>(
                name: "posts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return blogService.GetPostsByAuthor(id);
                }
            );
            Field<ListGraphType<SocialNetworkType>>(
                name: "socials",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return blogService.GetSNsByAuthor(id);
                }
            );
        }
    }
}
```

### GraphQL query

The '!' sign says that this parameter is required, otherwise the query won’t work.

```json
query GetBlogData($id: Int!) {
  author(id: $id) {
    id
    name
  }
  posts(id: $id) {
    author {
      bio
    }
    categories
    comments {
      description
      count
      url
    }
  }
  socials(id: $id) {
    nickName
    type
  }
}
```

### Query variable

```json
{
    "id":1
}
```

### References

*[getting-started-with-graphql-in-asp-net](https://www.red-gate.com/simple-talk/dotnet/net-development/getting-started-with-graphql-in-asp-net/)