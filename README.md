# Integration test library

While ASP.NET Core provides great utilities for testing web applications, it
doesn't provide a lot of help integration testing pages in a web application.

For example, it doesn't easily allow you to fill out forms and submit them
back to the server.

This library provides a set of components that make it easier to integration
test web application using the
[Page Object Model pattern](https://martinfowler.com/bliki/PageObject.html).

## System requirements

- .NET Core SDK 3.1
- ASP.NET Core 3.1

## Getting started

Follow these steps to get started using the library:

### Add a reference to the library

You can add a reference to the package using the dotnet CLI:

```shell
dotnet add package FizzyLogic.AspNetCore.Mvc.Testing
```

Alternatively, search for the package in the Visual Studio package manager.
Or add the package using the following command in the Package Manager Console
inside Visual Studio:

```powershell
Install-Package FizzyLogic.AspNetCore.Mvc.Testing
```

### Create a page object

Next, create a page object in your test project. In this example, we're going
to test a login page:

```csharp
public class LogOnPageModel : PageModel
{
        public LogOnPageModel(HttpClient client, IHtmlDocument document) : base(client, document)
        {
        }
}
```

Every page object derives from the PageModel class and should have a constructor
that accepts a `IHtmlDocument` instance and `HttpClient` instance.

We can extend the page model by adding properties for inputs that we expect the
user to fill out.

```csharp
public string EmailAddress
{
    set => Document.Form("logon").InputElement("Input.EmailAddress").Value = value;
    get => Document.Form("logon").InputElement("Input.EmailAddress").Value;
}

public string Password
{
    set => Document.Form("logon").InputElement("Input.Password").Value = value;
    get => Document.Form("logon").InputElement("Input.Password").Value;
}
```

To submit the page to the server we'll add a `SubmitAsync` method:

```csharp
public async Task<HttpResponseMessage> SubmitAsync()
{
    var logOnForm = Document.Form("logon");

    return await Client.SubmitFormAsync(logOnForm);
}
```

This method finds the form we want to submit and the submit button to use
for submitting the form. We then call the `SubmitFormAsync` method on the
`Client` instance that is included in the page model class.

Now that we have a page model object, let's use it in a test.

### Write a test

To write an integration test we'll need to set up a few things:

- A test class that uses the `WebApplicationFactory` for starting the web
  application.
- Write a test method that uses the `LogOnPageModel` we just created.

Let's start by creating a test class using xunit.

```csharp
public class LogOnTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly HttpClient _client;
    private readonly Navigator _navigator;

    public LogOnTests(AcademyWebApplicationFactory factory)
    {
        _factory = factory;

        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        _navigator = new Navigator(_client);
    }
}
```

The test class implements `IClassFixture<WebApplicationFactory<Startup>>` so
we'll automatically get an instance of `WebApplicationFactory<Startup>` injected
in the constructor of the class.

In the constructor, we're storing the factory, a HTTP Client instance and
an `Navigator` instance for use within the test class.

Once we have a test class, we can write on or more test methods to
check the behavior of the web application.

```csharp
[Fact]
public async Task PostPageWithValidCredentialsLogsUserIn()
{
    var (_,logOnPage) = await _navigator.NavigateToAsync<LogOnPageModel>("/Account/LogOn");

    logOnPage.Password = "SomePassword123!!";
    logOnPage.EmailAddress = "test@domain.org";

    var response = await logOnPage.SubmitAsync();

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/", response.Headers.Location.ToString());
}
```

In the test method we can use the `Navigator` instance to load web pages
in the web application.

Because we have a page model, we don't need to worry about CSS selectors, IDs,
or other HTML specific stuff. Instead we fill out the `EmailAddress` and
`Password` property and call the `SubmitAsync` method to send the data to the
server.

After we've submitted the form, we can use assertions to validate the response
sent by the server.

## Code of conduct

This project and everyone participating in it is governed by the
[AI Academy Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are
expected to uphold this code. Please report unacceptable behavior to
willem.meints@gmail.com.

## Contributing

Feel free to open up pull requests for tutorials, changes in the code, ideas
and bug reports. Please read [the contributor guide](CONTRIBUTING.md)
before submitting issues and suggestions.
