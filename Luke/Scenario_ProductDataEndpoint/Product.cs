using System.Collections.Generic;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public class Product
    {
        public long id { get; set; }
        public string title { get; set; }
        public List<Rating> ratings { get; set; }
        public List<Comment> comments { get; set; }

        public Product()
        {
            ratings = new List<Rating>();
            comments = new List<Comment>();
        }
    }
}