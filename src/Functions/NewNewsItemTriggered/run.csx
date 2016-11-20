

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public static void Run(string mySbMsg,  out IDictionary<string, string> notification, TraceWriter log)
{
    log.Info($"C# Queue trigger function processed: {mySbMsg}");
    notification = GetTemplateProperties(mySbMsg);
}

private static IDictionary<string, string> GetTemplateProperties(string message)
{
    Dictionary<string, string> templateProperties = new Dictionary<string, string>();
    templateProperties["message"] = message;
    return templateProperties;
}