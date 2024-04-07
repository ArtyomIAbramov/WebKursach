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
            return db.Clients.GetList();
        }

        public Client GetClient(int Id)
        {
            return db.Clients.GetItem(Id);
        }

        public bool CreateClient(
            Car car,
            string name,
            string surname,
            string phonenumber,
            string address,
            string passport)
        {
            var clientCreated = db.Clients.Create(new Client()
            {
                Name = name,
                Surname = surname,
                Phonenumber = phonenumber,
                Address = address,
                Passport = passport,
                Cars = new List<Car> { car }
            });

            if (clientCreated)
            {
                car.Position = Position.UnAvailable;
                Save();
                return true;
            }
            return false;
        }

        public bool UpdateClient(Client p)
        {
            Client ph = db.Clients.GetItem(p.Id);
            if(ph != null)
            {
                ph.Id = p.Id;
                ph.Name = p.Name;
                ph.Surname = p.Surname;
                ph.Phonenumber = p.Phonenumber;
                ph.Address = p.Address;
                ph.Passport = p.Passport;
                ph.Cars = p.Cars;

                if (db.Clients.Update(ph))
                {
                    Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteClient(int id)
        {
            Client p = db.Clients.GetItem(id);
            if (p != null)
            {
                var clientDeleted = db.Clients.Delete(p.Id);

                if (clientDeleted)
                {
                    Save();
                    return true;
                }
                return false;
            }

            return false;

        }

        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
