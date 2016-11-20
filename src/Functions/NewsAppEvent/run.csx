using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log, out string outputSbMsg)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string topicData = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "topicData", true) == 0)
        .Value;
    
    outputSbMsg = topicData;

    if(topicData == null){
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a topicData on the query string");
    }  

   return req.CreateResponse(HttpStatusCode.OK, "Thanks, we've passed along " + topicData + " and it will be handled at our nearest convenience");
}