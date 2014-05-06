FinApps API Client - C#
======================

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
