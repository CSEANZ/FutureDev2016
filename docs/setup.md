### Set up instructions

To get the [API](https://github.com/MSFTAuDX/FutureDev2016/tree/master/src/DXNewsAPI) going you will need to set up some extra services. 

This is true even if you are running locally. 

We recommend you install [Visual Studio](https://www.visualstudio.com/downloads/). Community edition is free! This will allow you to edit and build the code as well as interact with Microsoft Azure when you get that far along.  

You will need to install the [Azure SDK](https://azure.microsoft.com/en-us/downloads/). 

You can get a free trial of Microsoft Azure [here](https://azure.microsoft.com/en-us/free/). If you are an MSDN subscriber you may get some free credit each month! Check it out in your MSDN portal. 

You will need a trial to use the Service Bus and Logic App bits of the project. 

Once you have the code up and running there are some secret settings you will need to install. We don't have them pre-installed in the GitHub code becasue then ... you'd be able to use our stuff!

We keep the secrets our of code by using [app secrets](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/appsecrets.md) - a cool feature in .NET core to keep your passwords out of your code repositories. 

#### Table Storage

```
dotnet user-secrets set  "ConnectionStrings:TableStorage:ConnectionString" "UseDevelopmentStorage=true"
```

You can [create a storage account](https://docs.microsoft.com/en-us/azure/storage/storage-create-storage-account-classic-portal) in Azure and use a live account instead. You'll *can* do this from your local machine, but you *must* do it when running from a real Azure deployment. 

Also - try the [Store Explorer](https://docs.microsoft.com/en-us/azure/vs-azure-tools-storage-manage-with-storage-explorer) to help you see what's going on in your storage accounts!

#### Service Bus

```
dotnet user-secrets set  "ConnectionStrings:ServiceBus:ConnectionString" "[YourConnectionHere]"
```

Our exmaple project relies on the Service Bus to make it more ["Cloudy"](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/cloudy.md).

Use this [documentation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions) to create a Service Bus topic and insert your key in to the app-secrets.

