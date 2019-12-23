using System.Dynamic;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public class Rating
    {
        public long id { get; set; }
        public long productId {get; set;}
        public long userId {get; set;}
        public int stars {get; set;}
    }
}