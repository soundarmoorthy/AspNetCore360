![Build](https://github.com/soundarmoorthy/Hahn/workflows/Build/badge.svg?branch=master&event=status)

# AspNetCore360
Read requirements.md for deatils on the requirements. This is a basic .NET Core app did for training purposes  based on an interview question from internet.

#### Requiremets 
* Visual studio 2019 for Mac
* Dot Net Core 360
* 

#### Trying it out
* Goto  the root folder of the application from the command line
* Run `dotnet build`
* Run `dotnet <path to web project bin folder>/Hahn.ApplicatonProcess.May2020.Web.dll`
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
> .NET Core runtime 3.1 
> .NET standard 2.0

#### Bugs 
1. Running the tests in parallel makes some of the tests fail because of Cocnurrency issues
with InMemory database provider.

2. The UrlHelper.ActionLink api fails in .NET core 3.1 and when a resource is created the 
url for it is composed manually.
# stackoverflow.examples
