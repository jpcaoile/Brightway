using Brightway.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Brightway.Services
{
    public class HttpWebClient : IRequestClient
    {
        private readonly WebClient _client;

        public HttpWebClient()
        {
            _client = new WebClient();
        }

        public IList<T> GetRequest<T>(string apiEndPoint) where T : IBrightwayModel
        {
            var data = _client.OpenRead(apiEndPoint);

            if (data == null)
                throw new Exception("Data Invalid");

            IList<T> requestResults;
            using (var sr = new StreamReader(data))
            {
                var streamResults = sr.ReadToEnd();
                requestResults = JsonConvert.DeserializeObject<IList<T>>(streamResults);
            }

            return requestResults;
        }

    }
}
