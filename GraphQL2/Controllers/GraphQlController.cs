using GraphQL.Server.Internal;
using GraphQL2.Infrastructure;
using GraphQL2.Schema;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GraphQL2.Controllers
{
    [Route(Startup.GraphQlPath)]
    public class GraphQlController : Controller
    {
        private readonly IGraphQLExecuter<FeedsterSchema> _graphQLExecuter;
        public GraphQlController(IGraphQLExecuter<FeedsterSchema> graphQLExecuter)
        {
            _graphQLExecuter = graphQLExecuter ?? throw new ArgumentNullException(nameof(graphQLExecuter));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            try
            {
                var executionResult = await _graphQLExecuter.ExecuteAsync(query.OperationName, query.Query, query.Variables, null);

                if (executionResult.Errors != null)
                {
                    return BadRequest(executionResult.Errors);
                }

                return new GraphQLExecutionResult(executionResult);
            }
            catch (GraphQLBadRequestException ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }
    }
}
