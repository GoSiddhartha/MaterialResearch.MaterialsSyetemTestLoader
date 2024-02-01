using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MaterialResearch.MaterialsSyetemTestLoader;

public class TestFileLoader
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public TestFileLoader(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _logger = loggerFactory.CreateLogger<TestFileLoader>();
        _configuration = configuration;
    }

    [Function("TestFileLoader")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
    
        var testConfig = _configuration["secret"];
        if(string.IsNullOrEmpty(testConfig))
            response.WriteString("The secret is empty");
        else
            response.WriteString(testConfig);
        
        return response;
    }
}