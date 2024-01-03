using AseguradoraApp.Models;

namespace HDIClient.Service.Interface
{
    public interface IClientService
    {
        public Task<int> RegisterNewClientDriver(DriverClient driverClient);
    }
}
