using System.Collections.Generic;

namespace Brightway.Contracts
{
    public interface IPizzaReportable
    {
        Dictionary<string, int> ToppingSingleCounts { get; set; }
        Dictionary<string, int> ToppingCombinationCounts { get; set; }
    }
}
