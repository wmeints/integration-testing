using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests.Helpers
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapPost("/post", async context =>
                {
                    var formContent = await context.Request.ReadFormAsync();
                    var inputText = formContent["input1"].FirstOrDefault();

                    await context.Response.WriteAsync($"Received: {inputText}");
                });
                
                routes.MapPost("/OtherPost", async context =>
                {
                    var formContent = await context.Request.ReadFormAsync();
                    var inputText = formContent["input1"].FirstOrDefault();

                    await context.Response.WriteAsync($"Received on other: {inputText}");
                });
                
                routes.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(
                        @"
                            <html>
                                <body>
                                    <form id=""test"" method=""post"" action=""/post"">
                                        <input type=""text"" name=""input1""/>
                                        <button type=""submit"">Submit</button>
                                        <button type=""submit"" formaction=""/OtherPost"" id=""other"">Other Action</button>
                                    </form>
                                </body>
                            </body>"
                        );
                });
            });
        }
    }
}