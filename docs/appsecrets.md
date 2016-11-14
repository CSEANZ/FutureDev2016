# App Secrets

This project makes use of app secrets to keep sensitive connection strings out of the GitHub repo (you are all dodgey!)

For more information on how to add this to your own projects see the [ASP.NET Core](https://docs.asp.net/en/latest/security/app-secrets.html) doco. 

To install a secret run the following command in the same folder as the project.json file. 

```
dotnet user-secrets set MySecret ValueOfMySecret
```

Keep in mind that this only works in dev mode as the project.json file is not copied during publish of a release build. 
