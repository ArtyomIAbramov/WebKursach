using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IClientService
    {
        List<Client> GetAllClients();
        Client GetClient(int Id);
        void CreateClient(
            Car car,
            string name,
            string surname,
            string phonenumber,
            string address,
            string passport);
        void UpdateClient(Client p);
        void DeleteClient(int id);
    }
}
