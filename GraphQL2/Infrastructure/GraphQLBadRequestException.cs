using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL2.Infrastructure
{
    internal class GraphQLBadRequestException : Exception
    {
        public GraphQLBadRequestException(string message)
            : base(message)
        { }
    }
}
