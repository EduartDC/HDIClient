using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IPolicyService
    {
        public Task<List<PolicyDTO>> GetPolicyByDriver(string token, string idDriver);
    }
}
