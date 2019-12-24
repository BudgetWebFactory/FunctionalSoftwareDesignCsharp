using System;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public class ErrorFunctions
    {
        public delegate ErrorCase ErrorFunction(Exception e);
    }

    public class ErrorCase
    {
        public string message { get; set; }
        public Exception exception { get; set; }
    }
}