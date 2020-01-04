using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FizzyLogic.AspNetCore.Mvc.Testing.Tests.Helpers;
using Xunit;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests
{
    public class NavigatorTests
    {
        [Fact]
        public void CantCreateNavigatorWithoutHttpClient()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var navigator = new Navigator(null);
            });
        }

        [Fact]
        public async Task CanNavigateToPages()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
                
            var navigator = new Navigator(client);
            var (response, pageObject) = await navigator.NavigateToAsync<SamplePageObject>("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task NavigateWithEmptyUrlThrowsException()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
                
            var navigator = new Navigator(client);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await navigator.NavigateToAsync<SamplePageObject>(""));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await navigator.NavigateToAsync<SamplePageObject>(null));
        }

        [Fact]
        public async Task NavigateToNonExistingPageReturnsOnlyResponse()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
                
            var navigator = new Navigator(client);

            var (response, page) = await navigator.NavigateToAsync<SamplePageObject>("/SomeSillyUrl");
            
            Assert.NotNull(response);
            Assert.Null(page);
        }
    }
}