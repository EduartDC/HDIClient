using HDIClient.DTOs;

namespace HDIClient.Service.Interface
{
    public interface IUserService
    {
        public Task<TokenDTO> Login(string user, string pass);
    }
}
