[![Build](https://github.com/soundarmoorthy/AspNetCore360/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/soundarmoorthy/AspNetCore360/actions/workflows/build.yml)

# AspNetCore360
Read requirements.md for deatils on the requirements. This is a basic .NET Core app with boilerplate for API, Swagger, Logging, Validation, Dependency Injection, HTTPS, github actions. 

#### Requiremets 
* Visual studio 2022 for Mac
* Dot Net core 6.0

#### Authentication
* The application uses twitter authentication. So if you don't have a twitter API Key and Secret, please get one. [Follow this](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/twitter-logins?view=aspnetcore-6.0)
* From command line set the api key and secret in the secrets manager storage in local machine
  * `cd <root project directory>`
  * `dotnet user-secrets init --project Web/API.csproj`
  * `dotnet user-secrets set "Twitter:ApiKey" "<twitterapikey>" --project Web/API.csproj` 
  * `dotnet user-secrets set "Twitter:ApiKeySecret" "<twitterapisecret>" --project Web/API.csproj`

#### Trying it out
* Goto  the root folder of the application from the command line
* Run `dotnet build`
* Run `dotnet Web/bin/Debug/netcoreapp3.1/Api.dll` . The Dll name might be different, so please check it. 
* From the browser goto the URL `https://localhost:5000/swagger/index.html`
* By default there are no data in the  database. It's cleaned up after every run.

#### Building 
* Clone and checkout master branch
* From command line run `dotnet build`


#### Running tests
* From command line run `dotnet test` from the root directory. 
* To see the coverage numbers run `dotnet test /p:CollectCoverage=true` from the root directory

> Note : Some of the tests depend on an internet connection to leverage 3rd party services. So 
> make sure you have an internet connection. 


#### Development

This code was built on Microsoft Visual Studio for Mac 2019. This doesn't have any external 
dependencies. This should work with Visual Studio 2019 for Windows as well.

You need to have the folllowing installed. 
> .NET Core runtime 5.0
> .NET standard 2.1

#### Bugs 
1. Running the tests in parallel makes some of the tests fail because of Cocnurrency issues
with InMemory database provider.

2. The UrlHelper.ActionLink api fails in .NET core 3.1 and when a resource is created the 
url for it is composed manually.
