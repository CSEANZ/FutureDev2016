# The Repo

Welcome to the Repo - code examples and documentation to be read in conjunction with the [Satya Nadella Sydney Developer Event 2016](https://channel9.msdn.com/Events/Microsoft-Australia/Developer-Event-2016/Keynote). After watching Satya, you can skip to ~53 minutes for the developer portion of the event. 

The content covers a range of topics, with a special eye on developer expereince and getting the most out of tooling and platforms. 

Our view: there is far more to development than just code. 


The project we demonstrate with here is a [super simple news site](https://github.com/MSFTAuDX/FutureDev2016/tree/master/src/DXNewsAPI) with an API. 

We took each step of the demo and documented it here. 

## Project Set up

To get this up and running on your own machine you will need to configure some things. 

[Set up instructions](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/setup.md)

## Azure App Service

The core of our system in the demonstration is the [Azure App Service](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/appservice.md).


## Azure API Management

Exposing API's is a lot more difficult than just hosting it somewhere!

[Azure API Management](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/apimanagement.md) takes the pain away and provides an awesome expereince for developers along the way. 


## Service Bus

During the development we use Service Bus to help separate out the app and make it more ["Cloudy"](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/cloudy.md). 

## Logic Apps

Once our App is nice and event based by using Service Bus, we can add and extend functionality by using [Logic Apps](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/logicapps.md).

No need to engage the development team!

## Xamarin

Once you have an API, you need a way to take your app to where your users are - those supercomputers that are in everyone's pockets these days. 

That way is [Xamarin](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/xamarin.md). 

Best of all - Xamarin cross platform development is free!


## Swagger and AutoRest <a name="swagger"> </a>
Throughout the demonstration and this code Swagger features heavily. It's the glue between the various components. It self documents the API - allowing us to expose it via API Management, automatically generate SDK's and otherwise integrate it in a range of ways with minimal effort.  

[Get it going](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/swagger.md) on your projects now with minimal fuss!

It's so easy - you'd be crazy not to use it. 



## Cognitive Services

Microsoft [Cognitve Services](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/cognitive.md)
 enables your services to become more human. It's AI as a service - a preconfigured machine learning platform that is accessible via simple RESTful end points. 


As with most stuff here - you can get started for free. 

It's like a service that converts the world in to JSON.  

## Bot Framework

Now that we can make our code intelligent and all human like we can take it to the next level with the Microsoft [Bot Framework](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/bots.md).

Now your code can run inside the most popular chat engines like Slack, Skype and Facebook Messenger. 


## DevOps Overview

## VSTS - What you need to know

## OSS and Microsoft

## OSS and DevOps

## .NET Core and Docker

## Kubernetes Awesomeness