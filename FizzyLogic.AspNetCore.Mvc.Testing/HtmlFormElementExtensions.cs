using System.Linq;
using AngleSharp.Html.Dom;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Extension methods for <see cref="IHtmlFormElement"/>.
    /// </summary>
    public static class HtmlFormElementExtensions
    {
        /// <summary>
        /// Finds a single input element in a HTML form.
        /// </summary>
        /// <param name="form">Form to find the element in.</param>
        /// <param name="name">Name of the element.</param>
        /// <returns></returns>
        public static IHtmlInputElement InputElement(this IHtmlFormElement form, string name)
        {
            return (IHtmlInputElement)form[name];
        }

        /// <summary>
        /// Finds a single submit button inside a HTML form.
        /// </summary>
        /// <param name="form">Form to look for the submit button.</param>
        /// <param name="identifier">ID of the element to locate.</param>
        /// <returns>Returns the located submit button.</returns>
        public static IHtmlElement SubmitButton(this IHtmlFormElement form, string identifier)
        {
            return (IHtmlElement)form
                .QuerySelectorAll("button[type='submit']")
                .FirstOrDefault(x => x.Id == identifier);
        }
    }
}
