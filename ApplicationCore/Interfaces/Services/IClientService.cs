using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IClientService
    {
        List<Client> GetAllClients();
        Client GetClient(int Id);
        bool CreateClient(
            Car car,
            string name,
            string surname,
            string phonenumber,
            string address,
            string passport);
        bool UpdateClient(Client p);
        bool DeleteClient(int id);
    }
}
