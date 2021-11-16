using Brightway.Contracts;
using System.Collections.Generic;

namespace Brightway.Services
{
    public interface IPizzaToppingRepo
    {
        IList<ToppingCombination> GetToppingCombinations();
    }
}
