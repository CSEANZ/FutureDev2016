$OutputFile = $PSScriptRoot + "\\swagger.json"
Invoke-WebRequest -Uri "http://dxnews.azurewebsites.net/swagger/v1/swagger.json" -OutFile $OutputFile
Set-Location $PSScriptRoot
& ..\packages\autorest.0.17.3\tools\AutoRest.exe -CodeGenerator CSharp -Modeler Swagger -Input $OutputFile -NameSpace DXNews.SDK -AddCredentials false
