using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Factory class used for constructing page model instances.
    /// </summary>
    public static class PageModelFactory
    {
        /// <summary>
        /// Creates a new page model instance from a received HTTP response.
        /// </summary>
        /// <param name="client">HTTP client that was used to get the response.</param>
        /// <param name="responseMessage">HTTP Response received from the server.</param>
        /// <typeparam name="TPageModel">The page model to create.</typeparam>
        /// <returns>Returns the created page model.</returns>
        public static async Task<TPageModel> CreateFromResponse<TPageModel>(HttpClient client, HttpResponseMessage responseMessage) where TPageModel: PageModel
        {
            var document = await HtmlDocumentFactory.CreateFromResponseAsync(responseMessage);
            return (TPageModel)Activator.CreateInstance(typeof(TPageModel), client, document);
        }
    }
}
