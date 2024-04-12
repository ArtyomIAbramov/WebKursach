using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IClientService
    {
        List<Client> GetAllClients();
        List<Client> GetAllNewClients();
        Client GetClient(int Id);
        bool CreateClient(
            string name,
            string surname,
            string phonenumber,
            string address,
            string passport,
            Car car = null);
        bool UpdateClient(Client p);
    }
}
