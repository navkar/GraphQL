using GraphQL.Types;
using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL2.Types;
using GraphQL2.Documents;
using GraphQL2.Infrastructure;

namespace GraphQL2.Queries
{
    public class FeedCommentQuery : ObjectGraphType
    {
        private static readonly Uri _collectionUri = UriFactory.CreateDocumentCollectionUri(Constants.DATABASE_NAME, Constants.COLLECTION_NAME);
        private static readonly FeedOptions _feedOptions = new FeedOptions { EnableCrossPartitionQuery = true, MaxItemCount = -1, };

        public FeedCommentQuery(IDocumentClient documentClient)
        {
            Field<ListGraphType<FeedMessageType>>(
                "feedster",
                resolve: context => documentClient.CreateDocumentQuery<FeedMessage>(_collectionUri, _feedOptions)
            );
        }
    }
}
