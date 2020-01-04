using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Use this to navigate to specific pages inside the application.
    /// </summary>
    public class Navigator
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="Navigator"/>
        /// </summary>
        /// <param name="client">HTTP client instance to use for navigation.</param>
        public Navigator(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Navigates to a HTML page in the application.
        /// </summary>
        /// <param name="uri">URI of the page to retrieve.</param>
        /// <typeparam name="TPage">Type of page to render.</typeparam>
        /// <returns>Returns a tuple containing the HTTP response and the loaded page if the response indicated success.</returns>
        public async Task<(HttpResponseMessage, TPage)> NavigateToAsync<TPage>(string uri) where TPage : PageModel
        {
            if(string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri));
            
            var (response, page) = await _client.GetHtmlDocumentAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var pageObject = await PageModelFactory.CreateFromResponse<TPage>(_client, response);
                return (response, pageObject);
            }

            return (response, null);
        }
    }
}