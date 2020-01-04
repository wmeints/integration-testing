using System.Net.Http;
using AngleSharp.Html.Dom;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Inherit from this class to create page models in the integration tests.
    /// </summary>
    public abstract class PageModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PageModel"/>.
        /// </summary>
        /// <param name="client">HTTP client to use performing additional HTTP requests related to the page.</param>
        /// <param name="document">HTML document wrapped by the page model.</param>
        protected PageModel(HttpClient client, IHtmlDocument document)
        {
            Client = client;
            Document = document;
        }

        /// <summary>
        /// Gets the HTTP Client to use for communicating with the web server.
        /// </summary>
        protected HttpClient Client { get; }

        /// <summary>
        /// Gets the HTML document wrapped by the page object.
        /// </summary>
        protected IHtmlDocument Document { get; }
    }
}
