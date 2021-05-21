### Securing sensitive info through ModelBinding

These set of demos demonstrate the functionality of Model Binders in .NET, the scenario would be to hide sensitive information in the URL to the human eye. This scenario is pretty useful for having a much more secure app after a typical login, when taking the user to another view that requires to be Authorize.

- Demo: it is a solution that contains a single project (.NET 5). In **Home/Index** view we have a form that could be filled with information about a user, when submitting via POST we encode the form using  Base64... then it's readed and binded to the model. Using all the functionality provided by the framework through ModelBinders and ValueProviders. Folders structure to be remmarked in the talk:
	- Attributes
	- Extentions
	- ModelBinders
	- ValueProviders

- Pending demo in .NET Framework