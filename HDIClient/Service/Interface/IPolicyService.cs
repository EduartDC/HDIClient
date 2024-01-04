using HDIClient.DTOs;
using System.Net;

namespace HDIClient.Service.Interface
{
    public interface IPolicyService
    {
        public Task<(List<PolicyDTO>?, HttpStatusCode)> GetPolicyByDriver(string token, string idDriver);
    }
}
