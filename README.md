# Hahn

WIP (not complete): 
* Swagger documentation
* Frontend
* Logging


Read requirements.md for deatils on the requirements.

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

#### Testing
A postman document is present in the root directory of the project that can be used to 
test the endpoints. The filename is Hahn.postman_collection.json. You can import this document
into postman and call the API's. 
1. Call create API
2. Call Get API
3. call Update API
4. Call Get API
5. Call delete API
6. Call Get API

#### Bugs 
1. Running the tests in parallel makes some of the tests fail because of Cocnurrency issues
with InMemory database provider.

2. The UrlHelper.ActionLink api fails in .NET core 3.1 and when a resource is created the 
url for it is composed manually.
