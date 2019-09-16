﻿using GraphQL;
using GraphQL.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GraphQL2.Infrastructure
{
    internal class GraphQLExecutionResult : ActionResult
    {
        private const string CONTENT_TYPE = "application/json";

        private readonly ExecutionResult _executionResult;

        public GraphQLExecutionResult(ExecutionResult executionResult)
        {
            _executionResult = executionResult ?? throw new ArgumentNullException(nameof(executionResult));
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            IDocumentWriter documentWriter = context.HttpContext.RequestServices.GetRequiredService<IDocumentWriter>();

            HttpResponse response = context.HttpContext.Response;
            response.ContentType = CONTENT_TYPE;
            response.StatusCode = StatusCodes.Status200OK;

            return documentWriter.WriteAsync(response.Body, _executionResult);
        }
    }
}