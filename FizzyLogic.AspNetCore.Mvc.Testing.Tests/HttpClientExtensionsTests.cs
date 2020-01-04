using System;
using System.Net;
using System.Threading.Tasks;
using FizzyLogic.AspNetCore.Mvc.Testing.Tests.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Xunit;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests
{
    public class HttpClientExtensionsTests
    {
        [Fact]
        public async Task CanSubmitForms()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
            var navigator = new Navigator(client);

            var (_, page) = await navigator.NavigateToAsync<SamplePageObject>("/");

            var response = await page.SubmitAsync();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CantSubmitNonExistingForms()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
            var navigator = new Navigator(client);

            var (_, page) = await navigator.NavigateToAsync<SamplePageObject>("/");

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await page.SubmitNonExistingAsync();
            });
        }

        [Fact]
        public async Task CantSubmitWithoutSpecifyingSubmitButton()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
            var navigator = new Navigator(client);

            var (_, page) = await navigator.NavigateToAsync<SamplePageObject>("/");

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await page.SubmitWithoutSubmitButton();
            });
        }

        [Fact]
        public async Task CanSubmitUsingFormActionAttributeOnSubmit()
        {
            var factory = new TestApplicationFactory();
            var client = factory.CreateClient();
            var navigator = new Navigator(client);

            var (_, page) = await navigator.NavigateToAsync<SamplePageObject>("/");

            var response = await page.SubmitWithOtherFormAction();
            
            Assert.Equal("Received on other: ", await response.Content.ReadAsStringAsync());
        }
    }
}