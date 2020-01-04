using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests.Helpers
{
    public class TestApplicationFactory: WebApplicationFactory<TestStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(hostBuilder => { hostBuilder.UseStartup<TestStartup>(); });
        }
    }
}