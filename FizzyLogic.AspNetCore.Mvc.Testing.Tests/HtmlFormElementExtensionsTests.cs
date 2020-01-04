using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Xunit;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests
{
    public class HtmlFormElementExtensionsTests
    {
        [Fact]
        public async Task CanLocateInputs()
        {
            var content = "<html><body><form id=\"test\"><input type=\"text\" name=\"input1\"></form></body></html>";
            
            var config = Configuration.Default;
            var context = BrowsingContext.New();
            var document = (IHtmlDocument)await context.OpenAsync(req => req.Content(content));

            Assert.NotNull(document.Form("test"));
            Assert.NotNull(document.Form("test").InputElement("input1"));
        }
    }
}