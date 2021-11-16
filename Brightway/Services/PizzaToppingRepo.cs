using Brightway.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Brightway.Services
{
    public class PizzaToppingRepo : IPizzaToppingRepo
    {
        private IRequestClient _client;
        private readonly IConfiguration _config;

        public PizzaToppingRepo(IRequestClient requestClient, IConfiguration config)
        {
            _client = requestClient;
            _config = config;
        }

        public IList<ToppingCombination> GetToppingCombinations()
        {
            var endPoint = _config.GetValue("ApiEndPoints:ToppingCombination", string.Empty);
            var toppingCombinations = _client.GetRequest<ToppingCombination>(endPoint);

            return toppingCombinations;
        }
    }
}
