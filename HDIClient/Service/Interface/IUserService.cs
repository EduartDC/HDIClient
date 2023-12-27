using HDIClient.DTOs;
using System.Net;

namespace HDIClient.Service.Interface
{
    public interface IUserService
    {
        public Task<(TokenDTO, HttpStatusCode)> Login(string user, string pass);
    }
}
