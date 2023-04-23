using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Serilog;
using Serilog.Events;

try
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .CreateLogger();
    
    //TODO: store in appsettings.json
    string apiKey = "YourApiKey";
    
    //Pass this in from your application - hardcoded for testing
    string prompt = "Translate the following English text to French: 'Hello, how are you?'";

    Log.Information("Prompt: " + prompt);

    ScriptEngine engine = Python.CreateEngine();
    ScriptScope scope = engine.CreateScope();


// Load the Python script
    engine.ExecuteFile("path_to_file/openai_api.py", scope);

// Call the generate_text function
    dynamic generateText = scope.GetVariable("generate_text");
    var httpClientWrapper = new HttpClientWrapper();
    string result = generateText(prompt, apiKey, httpClientWrapper);

    Log.Information("Result: " + result);

    Console.WriteLine("Generated text: " + result);
    Log.CloseAndFlush();
}
catch (Exception ex)
{
    Log.Error(ex, "Error occurred");
}