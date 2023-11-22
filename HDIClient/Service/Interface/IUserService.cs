namespace HDIClient.Service.Interface
{
    public interface IUserService
    {
        public Task<string> Login(string user, string pass);
    }
}
