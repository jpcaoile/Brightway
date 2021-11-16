using Brightway.Contracts;
using System.Collections.Generic;

namespace Brightway.Services
{
    public interface IRequestClient
    {
        IList<T> GetRequest<T>(string apiEndPoint) where T : IBrightwayModel;
    }
}
