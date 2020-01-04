using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using HttpMethod = System.Net.Http.HttpMethod;

namespace FizzyLogic.AspNetCore.Mvc.Testing
{
    /// <summary>
    /// Extension methods for the <see cref="HttpClient"/> class.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Submits a HTML form to the server
        /// </summary>
        /// <param name="client">HTTP client to use for submitting the form.</param>
        /// <param name="form">HTML Form to submit.</param>
        /// <returns>Returns the HTTP response received after submitting the form.</returns>
        public static Task<HttpResponseMessage> SubmitFormAsync(this HttpClient client, IHtmlFormElement form)
        {
            var submitButton = form.QuerySelector("[type=submit]") as IHtmlElement;

            return SubmitFormAsync(client, form, submitButton);
        }

        /// <summary>
        /// Submits a HTML form to the server
        /// </summary>
        /// <param name="client">HTTP client to use for submitting the form.</param>
        /// <param name="form">HTML Form to submit.</param>
        /// <param name="submitButton">Submit button to use for submitting the form.</param>
        /// <returns>Returns the HTTP response received after submitting the form.</returns>
        public static Task<HttpResponseMessage> SubmitFormAsync(this HttpClient client, IHtmlFormElement form, IHtmlElement submitButton)
        {
            var formSubmission = form.GetSubmission(submitButton);
            var targetUri = (Uri)formSubmission.Target;
            
            if (submitButton.HasAttribute("formaction"))
            {
                var formAction = submitButton.GetAttribute("formaction");
                targetUri = new Uri(formAction, UriKind.Relative);
            }
            
            var submitFormRequest = new HttpRequestMessage(new HttpMethod(formSubmission.Method.ToString()), targetUri)
            {
                Content = new StreamContent(formSubmission.Body)
            };

            foreach (var header in formSubmission.Headers)
            {
                submitFormRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                submitFormRequest.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return client.SendAsync(submitFormRequest);
        }

        /// <summary>
        /// Executes a HTTP GET request against the server and retrieves the HTML content.
        /// </summary>
        /// <param name="client">Client to use for getting the HTML content.</param>
        /// <param name="uri">URI to retrieve from the server.</param>
        /// <returns>Returns the downloaded HTML page.</returns>
        public static async Task<(HttpResponseMessage, IHtmlDocument)> GetHtmlDocumentAsync(this HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            var document = await HtmlDocumentFactory.CreateFromResponseAsync(response);

            return (response, document);
        }
    }
}
