using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;

namespace FizzyLogic.AspNetCore.Mvc.Testing.Tests.Helpers
{
    public class SamplePageObject: PageModel
    {
        public SamplePageObject(HttpClient client, IHtmlDocument document) : base(client, document)
        {
            
        }
        
        public async Task<HttpResponseMessage> SubmitAsync()
        {
            return await Client.SubmitFormAsync(Document.Form("test"));
        }

        public async Task<HttpResponseMessage> SubmitNonExistingAsync()
        {
            return await Client.SubmitFormAsync(Document.Form("test-non-existing"));
        }

        public async Task<HttpResponseMessage> SubmitWithoutSubmitButton()
        {
            return await Client.SubmitFormAsync(Document.Form("test"),
                Document.Form("test").SubmitButton("non-existing"));
        }
        
        public async Task<HttpResponseMessage> SubmitWithOtherFormAction()
        {
            return await Client.SubmitFormAsync(Document.Form("test"),
                Document.Form("test").SubmitButton("other"));
        }
    }
}