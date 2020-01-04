using AngleSharp.Html.Dom;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Extension methods for <see cref="IHtmlDocument"/> extending its behavior with additional useful search queries.
    /// </summary>
    public static class HtmlDocumentExtensions
    {
        /// <summary>
        /// Finds a single form element in the HTML page.
        /// </summary>
        /// <param name="document">Document to search in.</param>
        /// <param name="id">Identifier of the form element to find.</param>
        /// <returns>Returns the found form element if it was found. Otherwise <c>null</c>.</returns>
        public static IHtmlFormElement Form(this IHtmlDocument document, string id)
        {
            return (IHtmlFormElement) document.QuerySelector($"form[id='{id}']");
        }
    }
}
