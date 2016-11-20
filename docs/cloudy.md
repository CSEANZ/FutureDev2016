# Making Cloudy Code

*Separating concerns to create extensible code with Azure Service Bus and Logic Apps*

There is more to making your code "cloud awesome" than just sticking it on a new managed host like the Azure App Service and calling it a day. 

There are many tenets - one of them is separation of concerns. It's an oldy, but a goody. It's been around forever and it's super imporant. Used properly in the cloud, it not only means more maintainable code but more extensible code too. 

The basic idea is that each code module should do only one job, and each module should know as little as possible about other modules and how they work. 

### Example

In our demonstration - when a new news item is added we place a message in to a [Service Bus Topic](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions).

[In this service](https://github.com/MSFTAuDX/FutureDev2016/blob/master/src/DXNewsAPI/src/DXNewsAPI/Model/Service/NotificationService.cs) we post the data to a URL. This URL is backed by an Azure Function which takes what we post and pops it on a Service Bus Topic. 

You may ask - why not just post straight to the Service Bus from our code file? There are a few reasons:

- Azure Functions have a GUI that allows you connect it to things like Service Bus - i.e. it's natively built for it.
- By placing the functionality to raise events somewhere else, we've extracted the functionality out of our main project and made a microservice instead. This is the beginnings of breaking down the monolithic code in to a group of microservices
- The Azure Function is accessible to more code than just our News API. Now any project in our system can use the end point - none of them need to know the underlying mechanisms of how to first this kind of event - they just need to know a URL. 

#### Azure Functions

Creating a function that takes a message from a URL query string and posts it to a Service Bus Topic is super simple, you can do it in a Wizard... almost. 

There is still some code to write, so no need to hang up your hat just yet. 

The main premise of Azure Functions is you define an input and an output and then you link them in your code. 

You could come in from a URL and output to a notification hub, or in from a file changed notification and out to a WebHook callback somewhere. 

Importantly you get to do some processing on the way through. 

*Note: We could have also used Logic Apps for this - they are a littel bit like functions except you don't have to write any code... super simple, but less customisable.*

