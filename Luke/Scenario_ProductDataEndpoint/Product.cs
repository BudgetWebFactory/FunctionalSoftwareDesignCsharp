using System.Collections.Generic;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public class Product
    {
        public long id { get; set; }
        public string title { get; set; }

        public IDictionary<string, object> extras { get; set; }

        public Product()
        {
            extras = new Dictionary<string, object>();
        }
    }
}