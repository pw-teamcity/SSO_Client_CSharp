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


####Login User
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

[Missing image]

Simply click the Install button next to the FinApps package you want to add to your project and watch as NuGet downloads the FinApps library package (and its dependencies) and adds the proper  references to your project.

[Missing image]

#### Installing via the Package Manager Console

To install a FinApp library using the Package Manager console, first open the console, then Use the _Install-Package_ command to install the different FinApps packages:

Install REST API client:

    Install-Package FinApps.ApiClient


