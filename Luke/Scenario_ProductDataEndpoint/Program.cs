using System;
using System.Reflection;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var productJson = ProductEndpoint.GetProductJson(1234567);
            Console.WriteLine(productJson);
        }
    }
}