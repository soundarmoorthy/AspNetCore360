#### Requirement Summary
We want you to build a Web Solution with RestEndpoints, as well as an User interface / Form to apply Data. Therefore you will have to separate the business logic, the data / persistence layer and the web project. The Api should have the following actions:  
  
* POST for Creating an Object – returning an 201 on successful creation of the object and the url were the object can be called  
* GET with id parameter – to ask for an object by id  
* PUT – to update the object with the given id  
* DELETE – to delete the object with the given id  
  
The Object we want to handle is the class named Applicant with the following properties:  
  
> ID ( int )  
Name ( string )  
FamilyName ( string )  
Address ( string )  
CountryOfOrigin ( string )  
EMailAdress ( string )  
Age (int)  
Hired (bool) – false if not provided.  
  
The WebApi accepts and returns `application/json` data.  
 
#### Validations  
The object and the properties should be validated by fluentValidation ( nuget ) with the following rules:  
  
  
| Column | Validation criteria |
|--|--|
| Name | at least 5 Characters |
| FamilyName | at least 5 Characters |
|Adress | at least 10 Characters  
|CountryOfOrigin | must be a valid Country – therefore ask with an httpclient here [https://restcountries.eu/rest/v2/…](https://restcountries.eu/rest/v2/name/aruba?fullText=true) – ApiDescription: [https://restcountries.eu/#api-endpoints-full-name](https://restcountries.eu/#api-endpoints-full-name) if the country is found, the country is valid.  
|EmailAdress |must be an valid email (only check for valid syntax *@*.[valid topleveldomain])  
|Age | must be between 20 and 60  
|Hired | If provided should not be null  


#### API Requirements
If the object is invalid ( on post and put ) – return 400 and an information what property does not fullyfy the requirements and which requirement is not fullyfied.  
  
If anything on the serverside goes wrong  return 500 Internal server error with proper message to user.
 
 #### Documentation
Describe the API with swagger therefore use Swashbuckle v5 host the swaggerUI under [localhost]/swagger. Use [https://github.com/mattfrear/…](https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters)  
  
Do not forget to provide example data in the SwaggerUI, so when someone click on try it out there is already useful valid data in the object that  
can be posted.  
 
 #### Logging
Use netcore logging to log each interaction with the API whereever it’s meaningful to do so and also implement this  
  
.AddFilter("Microsoft", LogLevel.Information)  
.AddFilter("System", LogLevel.Error)  
  
Write the log to a serilog rolling file sink the name needs to be setable in the applicationsettings.json file. Don’t use serilog in Domain – if you want to log in domain project use [https://www.nuget.org/packages/…](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/)  
  
#### Frontend
The including Form must be an Aurelia ( [http://aurelia.io](http://aurelia.io/) ) Application which uses the API to Post Data AND Validate all the inputs with  
the exact same parameters as the API does.  
- use Typescript  
- use Webpack  
- Form can only be send if the data is valid  
- Use Boostrap for the UI  
- Use aurelia-validation  
- Use a Bootstrap FormRenderer  
- invalid fields must be marked with an red border and an explanation why the date is invalid  
- all strings must be using i18next  
- the form has to buttons- send and reset.  
- clicking the reset button an aurelia-dialog is shown - which ask if the user is really sure to reset all the data  
- the reset button is only enabled if the user has typed in data -> if all fields are empty the reset button is not enabled.  
- when the user has touched a field but afterwards deleted all entries, the reset button is also not enabled.  
- The send button is only active if all required fields are filled out and are valid.  
- after sending the data, the aurelia router redirects to a view which confirms the sending.  
- if the sending was not successful an error message is shown in a aurelia-dialog. Describing what was going wrong.  

#### Localization
For all strings you use, use localization and a Jsonfile as resource file.  

#### Persistence
To save the data use entityframework core 3.1 and entityframework in memory database.

#### Final note  
If you have any questions – do not hesitate to ask. A Task like this is our daily business.
