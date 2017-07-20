![Twizo](https://www.twizo.com/wp-content/themes/twizo/_/images/twizo-logo-0474ce6f.png) 


# Twizo C\# API #

Connect to the Twizo API using C\#. This API includes functions to send verifications (2FA), SMS and Number Lookup.

## Requirements ##
* .NET >= 4.5

## Get application secret and api host ##
To use the Twizo API client, the following things are required:

* Create a [Twizo account](https://register.twizo.com/)
* Login on the Twizo portal
* Find your [application](https://portal.twizo.com/applications/) secret
* Find your nearest api [node](https://www.twizo.com/developers/documentation/#introduction_api-url)

## Usage ##

Start with initializing the Twizo Api using your api secret and api host

```cs
using System;
using TwizoAPI;
using TwizoAPI.Entity;

class MyClass
{
    static void Main(string[] args)
    {
        Twizo twizo = new Twizo("My_API_KEY", "API_HOST");
    }
}
```

Create a new verification

```cs
    Verification verification = twizo.CreateVerification("310123456789");
    verification.Send();
```

Verify token

```cs
    bool success = twizo.VerifyToken("012345", verification.messageId);
```

Send sms

```cs
    Sms sms = twizo.CreateSms('310123456789', 'Test message body', 'Twizo');
    sms.Send();
```

## Examples ##

In the examples directory you can find a collection of examples of how to use the api.

## License ##
[The MIT License](https://opensource.org/licenses/mit-license.php).
Copyright (c) 2016-2017 Twizo

## Support ##
Contact: [www.twizo.com](http://www.twizo.com/) — support@twizo.com
