using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Factory class used to create HTML documents.
    /// </summary>
    public static class HtmlDocumentFactory
    {
        /// <summary>
        /// Creates a new HTML document from a received HTTP response.
        /// </summary>
        /// <param name="response">HTTP response received from the server.</param>
        /// <returns>Returns the created HTML document.</returns>
        public static async Task<IHtmlDocument> CreateFromResponseAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            
            var document = await BrowsingContext
                .New()
                .OpenAsync(ResponseFactory, CancellationToken.None);
            
            return (IHtmlDocument)document;

            void ResponseFactory(VirtualResponse htmlResponse)
            {
                htmlResponse
                    .Address(response.RequestMessage.RequestUri)
                    .Status(response.StatusCode);

                MapHeaders(response.Headers);
                MapHeaders(response.Content.Headers);

                htmlResponse.Content(content);

                void MapHeaders(HttpHeaders headers)
                {
                    foreach (var header in headers)
                    {
                        foreach (var value in header.Value)
                        {
                            htmlResponse.Header(header.Key, value);
                        }
                    }
                }
            }
        }
    }
}
