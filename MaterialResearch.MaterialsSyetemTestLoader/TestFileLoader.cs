using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MaterialResearch.MaterialsSyetemTestLoader;

public class TestFileLoader
{
    private readonly ILogger _logger;

    public TestFileLoader(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TestFileLoader>();
    }

    [Function("TestFileLoader")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        StreamReader reader = new StreamReader( req.Body );
        string requestText = reader.ReadToEnd();
        string repoName = req.Query["repoName"];
        dynamic data = JsonConvert.DeserializeObject(requestText);
        
        repoName = repoName ?? data?.repoName;
        response.WriteString(repoName);

        return response;
    }
}