using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.BLL.Services
{
    public class ClientService : IClientService
    {
        private IDbRepository db;
        public ClientService(IDbRepository dbRepos)
        {
            db = dbRepos;
        }

        public List<Client> GetAllClients()
        {
            return db.Clients.GetList().Select(i => new Client(i)).ToList();
        }

        public Client GetClient(int Id)
        {
            return new Client(db.Clients.GetItem(Id));
        }

        public void CreateClient(
            Car car,
            string name,
            string surname,
            string phonenumber,
            string address,
            string passport)
        {
            db.Clients.Create(new Client()
            {
                Name = name,
                Surname = surname,
                Phonenumber = phonenumber,
                Address = address,
                Passport = passport,
                Cars = new List<Car> { car }
            });

            Save();
        }

        public void UpdateClient(Client p)
        {
            Client ph = db.Clients.GetItem(p.Id);
            ph.Id = p.Id;
            ph.Name = p.Name;
            ph.Surname = p.Surname;
            ph.Phonenumber = p.Phonenumber;
            ph.Address = p.Address;
            ph.Passport = p.Passport;
            ph.Cars = p.Cars;

            Save();
        }

        public void DeleteClient(int id)
        {
            Client p = db.Clients.GetItem(id);
            if (p != null)
            {
                db.Clients.Delete(p.Id);
                Save();
            }
        }

        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
