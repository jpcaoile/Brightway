using Brightway.Contracts;
using Brightway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brightway
{
    public class PizzaApplication
    {

        #region Constructor & Variables

        private readonly IPizzaToppingRepo _pizzaRepo;

        public PizzaApplication(IPizzaToppingRepo pizzaRepo)
        {
            _pizzaRepo = pizzaRepo;
        }

        #endregion
        

        public void Start()
        {
            var toppingCombinations = _pizzaRepo.GetToppingCombinations();

            if (toppingCombinations != null)
            {
                var report = GeneratePizzaReport(toppingCombinations);

                RenderToppingCombinationsReport(report.ToppingCombinationCounts);
                RenderIndividualToppingReport(report.ToppingSingleCounts);
            }
        }


        private IPizzaReportable GeneratePizzaReport(IList<ToppingCombination> toppingCombinations)
        {
            var toppingSingleCounts = new Dictionary<string, int>();
            var toppingCombinationCounts = new Dictionary<string, int>();

            foreach (var toppingCombo in toppingCombinations)
            {
                foreach (var topping in toppingCombo.Toppings)
                {
                    // sort through single toppings
                    if (toppingSingleCounts.ContainsKey(topping))
                        toppingSingleCounts[topping]++;

                    else
                        toppingSingleCounts.Add(topping, 1);

                    // sort through combinations
                    var toppingComboKey = GetPizzaCombinedKey(toppingCombo.Toppings);

                    if (toppingCombinationCounts.ContainsKey(toppingComboKey))
                        toppingCombinationCounts[toppingComboKey]++;

                    else
                        toppingCombinationCounts.Add(toppingComboKey, 1);
                }
            }

            return new PizzaReport { ToppingCombinationCounts = toppingCombinationCounts, ToppingSingleCounts = toppingSingleCounts };
        }


        private string GetPizzaCombinedKey(IList<string> toppings)
        {
            var str = new StringBuilder();
            foreach (var topping in toppings.OrderBy(t => t))
            {
                str.Append($"{topping.Trim()}|");
            }

            return str.ToString().Remove(str.Length - 1);
        }


        private Dictionary<string, int> SortIndividualToppings(IList<ToppingCombination> toppingCombinations)
        {
            var toppingCount = new Dictionary<string, int>();

            foreach (var toppingCombo in toppingCombinations)
            {
                foreach (var topping in toppingCombo.Toppings)
                {
                    if (toppingCount.ContainsKey(topping))
                        toppingCount[topping]++;

                    else
                        toppingCount.Add(topping, 1);
                }
            }

            return toppingCount;
        }


        private void RenderToppingCombinationsReport(Dictionary<string, int> toppingCombinationCounts)
        {
            var orderedCounts = toppingCombinationCounts.OrderByDescending(m => m.Value).Take(20);

            DisplayReport("Displaying Topping Combinations In Descending Order", orderedCounts);
        }


        private void RenderIndividualToppingReport(Dictionary<string, int> toppingCount)
        {
            DisplayReport("Displaying Toppings In Descending Order", toppingCount);
        }


        private void DisplayReport(string reportTitle, IEnumerable<KeyValuePair<string, int>> reportData)
        {
            Console.WriteLine($"\n---- {reportTitle} ----\n");
            foreach (var value in reportData.OrderByDescending(m => m.Value))
            {
                Console.Write(value.Key.PadRight(50));
                Console.WriteLine(value.Value);
            }
        }
    }
}
