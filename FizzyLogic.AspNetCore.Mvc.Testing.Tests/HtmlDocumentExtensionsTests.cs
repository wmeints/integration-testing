using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Xunit;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests
{
    public class HtmlDocumentExtensionsTests
    {
        [Fact]
        public async Task CanLocateFormsUsingId()
        {
            var content = "<html><body><form id=\"test\"></form></body></html>";
            
            var config = Configuration.Default;
            var context = BrowsingContext.New();
            var document = (IHtmlDocument)await context.OpenAsync(req => req.Content(content));

            Assert.NotNull(document.Form("test"));
        }
    }
}