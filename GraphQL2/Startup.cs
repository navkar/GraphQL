using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL2.Infrastructure;
using GraphQL2.Schema;
using GraphQL2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;

namespace GraphQL2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public const string GraphQlPath = "/graphql";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDocumentClient>(serviceProvider => {
                DbConnectionStringBuilder cosmosDBConnectionStringBuilder = new DbConnectionStringBuilder
                {
                    ConnectionString = serviceProvider.GetRequiredService<IConfiguration>()[Constants.CONNECTION_STRING_SETTING]
                };

                if (cosmosDBConnectionStringBuilder.TryGetValue("AccountKey", out object accountKey) && cosmosDBConnectionStringBuilder.TryGetValue("AccountEndpoint", out object accountEndpoint))
                {
                    return new DocumentClient(new Uri(accountEndpoint.ToString()), accountKey.ToString());
                }

                return null;
            });

            services.AddScoped<IDependencyResolver>(serviceProvider => new FuncDependencyResolver(serviceProvider.GetRequiredService));
            services.AddScoped<FeedsterSchema>();

            services.AddSingleton<IDocumentExecuter>(new DocumentExecuter());
            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true;
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddDataLoader();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<BlogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseGraphiQl(GraphQlPath);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
