FinApps API - Single Sign On
===========================

####Company Level Authentication:


Tenant Level authentication, is in a http header field named:

``X-FinApps-Token``

The format of the value of the Tenant Header Field is as follows:

``CompanyIdentifier=<CompanyToken>``

eg. ``X-FinApps-Token : CompanyIdentifier=CompanyToken``


####User Level Authentication:

User Level authentication, is done via the http authentication field.

The http authentication field is:

 ``Authorization``

The format of the field is as follows:

``Basic UserIdentifier:UserToken``

The value portion ( UserIdentifier:UserToken ) is Base64 Encoded.



###Company Api 


####Create New User
##### `POST /v1/users/new` 
```

Input:

Requires Company Level Authentication
Body:
{
  "firstname" :	User's First Name (optional)
  "lastname" : User's First Name (optional)
  "email" : User's Email, must be unique.
  "password" : User's Password, min size 6 chars, must contain UPPER and lowercase letters and a number
  "postalcode" : User's Postal Code
}

 
Output:

{
  "Result": 0,
  "ResultString": "Successful",
  "ResultObject": {
      "UserToken": "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
  }
}


```

####Update User Profile
##### `PUT /v1/users/update` 
```

Input:

Requires Company Level Authentication
Requires User Level Authentication
Body:
{
  "firstname" :	User's First Name (optional)
  "lastname" : User's First Name (optional)
  "email" : User's Email, must be unique.
  "postalcode" : User's Postal Code
}

 
Output:

{
  "Result": 0,
  "ResultString": "Successful",
  "ResultObject": {
      "UserToken": "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
  }
}


```

####Update User Password
##### `PUT /v1/users/updatepassword` 
```

Input:

Requires Company Level Authentication
Requires User Level Authentication
Body:
{
  "oldpassword" : User's old Password
  "newpassword" : User's new Password, min size 6 chars, must contain UPPER and lowercase letters and a number
}

 
Output:

{
  "Result": 0,
  "ResultString": "Successful",
  "ResultObject": {
      "UserToken": "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
  }
}


```

####Delete User's Account
##### `DELETE /v1/users/delete` 
```

Input:

Requires Company Level Authentication
Requires User Level Authentication

 
Output:

{
  "Result": 0,
  "ResultString": "Successful",
  "ResultObject": {}
}


```

####Login User (create new session)
##### `POST /v1/users/login` 
```

Input:

Requires Company Level Authentication
Requires User Level Authentication
Body:
{
    "clientip" :	User's IP (optional, recommended)
}
 
Output:

{
  "Result": 0,
  "ResultString": "Successful",
  "ResultObject": {
      "RedirectToUrl": "https://www.finapps.com/app/session/new/df9078f4-fe7d-4cbc-a5fa-b595e399ab23",
      "SessionToken": "df9078f4-fe7d-4cbc-a5fa-b595e399ab23"
  }
}


```


FinApps API - C# API Client
===========================

### Adding FinApps libraries to your .NET project

The best and easiest way to add the FinApps libraries to your .NET project is to use the NuGet package manager.  NuGet is a Visual Studio extension that makes it easy to install and update third-party libraries and tools in Visual Studio.  

NuGet is available for Visual Studio, and you can find instructions for installing the NuGet extension on the NuGet.org website:

[http://docs.nuget.org/docs/start-here/installing-nuget](http://docs.nuget.org/docs/start-here/installing-nuget)

Once you have installed the NuGet extension, you can choose to install the FinApps libraries using either the Package Manager dialog, or using the Package Manager console.

#### Installing via the Package Manager Dialog

To install a FinApps library using the Package Manager dialog, first open the dialog by right-clicking the References folder in your project and selecting the package manager option:

![](https://lh4.googleusercontent.com/f7arKv3rtF3_0x8ckYwDC4d9qr3lfcHcIYROjAAI2h6StebF_szFVy_irxjDuKtUlemg2PC9uWaUKjtSuZfwPh6PatIN76BrksWaL8slscC5yDpxxtQ)

When the package manager dialog opens simply search the online catalog for _‘FinApps’_.  The screen shot below shows the results returned from the NuGet catalog:

![](![](http://i.imgur.com/lQHpfdM.png)

Simply click the Install button next to the FinApps package you want to add to your project and watch as NuGet downloads the FinApps library package (and its dependencies) and adds the proper  references to your project.


#### Installing via the Package Manager Console

To install a FinApp library using the Package Manager console, first open the console, then Use the _Install-Package_ command to install the different FinApps packages:

Install REST API client:

    Install-Package FinApps.ApiClient

### Sample Usage

#### .Net 4.5.1
===============

#### Create a user:

    using FinApps.SSO.MVC5.Models;
    using FinApps.SSO.RestClient_Base.Annotations;
    using FinApps.SSO.RestClient_Base.Model;
    using FinApps.SSO.RestClient_NET451;
    
    var user = new FinAppsUser
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "user@example.com",
                Password = "Password@2",
                PostalCode = "10001"
            };
    var client = new FinAppsRestClient(
                baseUrl: "https://finapps.com/api/v1/",
                companyIdentifier: "ACME Inc.",
                companyToken: "my-secret-token!");
                
    FinAppsUser newUser = await _client.NewUser(user);
    if (newUser.Errors != null && newUser.Errors.Any())
        ' handle errors
    } else {
        string userToken = newUser.UserToken;
        ' save userToken locally
    }
    


#### Initiate a FinApps session:

    using FinApps.SSO.MVC5.Models;
    using FinApps.SSO.RestClient_Base.Annotations;
    using FinApps.SSO.RestClient_Base.Model;
    using FinApps.SSO.RestClient_NET451;
    

    var credentials = new FinAppsCredentials
            {
                Email = "user@example.com",
                FinAppsUserToken = "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
            };
    var client = new FinAppsRestClient(
                baseUrl: "https://finapps.com/api/v1/",
                companyIdentifier: "ACME Inc.",
                companyToken: "my-secret-token!");           
                
    FinAppsUser newSessionUser = await _client.NewSession(user.ToFinAppsCredentials(), Request.UserHostAddress);
    if (newSessionUser.Errors != null && newSessionUser.Errors.Any())
        ' handle errors
    } else {
        string redirectUrl = newSessionUser.SessionRedirectUrl;
        ' redirect to FinApps using above url
    }    
      
    
    
#### Update a User Profile:

    using FinApps.SSO.MVC5.Models;
    using FinApps.SSO.RestClient_Base.Annotations;
    using FinApps.SSO.RestClient_Base.Model;
    using FinApps.SSO.RestClient_NET451;
    

    var credentials = new FinAppsCredentials
            {
                Email = "user@example.com",
                FinAppsUserToken = "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
            };
    var user = new FinAppsUser
            {
                FirstName = "John L",
                LastName = "Perez",
                Email = "user.new@example.com",
                PostalCode = "12345"
            };            
    var client = new FinAppsRestClient(
                baseUrl: "https://finapps.com/api/v1/",
                companyIdentifier: "ACME Inc.",
                companyToken: "my-secret-token!");           
                
    FinAppsUser updatedUser = await _client.UpdateUserProfile(credentials, user);
    if (updatedUser.Errors != null && updatedUser.Errors.Any())
        ' handle errors
    } else {
        string userToken = updatedUser.UserToken;
        ' save userToken locally
    }   
    
    
#### Update a User Password:

    using FinApps.SSO.MVC5.Models;
    using FinApps.SSO.RestClient_Base.Annotations;
    using FinApps.SSO.RestClient_Base.Model;
    using FinApps.SSO.RestClient_NET451;
    

    var credentials = new FinAppsCredentials
            {
                Email = "user@example.com",
                FinAppsUserToken = "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
            };
            
    var client = new FinAppsRestClient(
                baseUrl: "https://finapps.com/api/v1/",
                companyIdentifier: "ACME Inc.",
                companyToken: "my-secret-token!");           
                
    FinAppsUser updatedUser = await _client.UpdateUserPassword(credentials, oldPassword, newPassword);
    if (updatedUser.Errors != null && updatedUser.Errors.Any())
        ' handle errors
    } else {
        string userToken = updatedUser.UserToken;
        ' save userToken locally
    }    
        
         
#### Delete a User:

    using FinApps.SSO.MVC5.Models;
    using FinApps.SSO.RestClient_Base.Annotations;
    using FinApps.SSO.RestClient_Base.Model;
    using FinApps.SSO.RestClient_NET451;
    

    var credentials = new FinAppsCredentials
            {
                Email = "user@example.com",
                FinAppsUserToken = "4Btuz6TJQU/KcKe8Te+l8F2Gi0ut4x7HMSD56vh3rUk="
            };
            
    var client = new FinAppsRestClient(
                baseUrl: "https://finapps.com/api/v1/",
                companyIdentifier: "ACME Inc.",
                companyToken: "my-secret-token!");           
                
    FinAppsUser deletedUser = await _client.DeleteUser(credentials);
    if (deletedUser.Errors != null && deletedUser.Errors.Any())
        ' handle errors
    } 
    
            
#### Demo Application     
    
[http://sso-mvc5.apphb.com/](http://sso-mvc5.apphb.com/)


    
    
