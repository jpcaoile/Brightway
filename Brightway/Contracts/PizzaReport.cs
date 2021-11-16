using System.Collections.Generic;

namespace Brightway.Contracts
{
    public class PizzaReport : IPizzaReportable
    {
        public Dictionary<string, int> ToppingSingleCounts { get; set; }
        public Dictionary<string, int> ToppingCombinationCounts { get; set; }
    }
}
