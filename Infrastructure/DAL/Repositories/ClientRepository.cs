using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.Extensions;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private DbAutoSalonContext db;
        private readonly ILogger _logger;
        public ClientRepository(DbAutoSalonContext db, ILogger logger)
        {
            this.db = db;
            _logger = logger;
        }

        public List<Client> GetList()
        {
            try
            {
                var clients = db.Clients.Include(c => c.Cars).ToList();
                _logger.LogExtension("Get Clients", clients);

                return clients;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Clients", "", LogLevel.Error);
                return null;
            }
        }

        public Client GetItem(int Id)
        {
            try
            {
                var client = db.Clients.Include(c => c.Cars).ToList().Where(c => c.Id == Id).First();

                if (client == null)
                {
                    throw new Exception();
                }

                _logger.LogExtension("Get Client", client);

                return client;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Client with Id", Id, LogLevel.Error);
                return null;
            }
        }

        public bool Create(Client p)
        {
            try
            {
                db.Clients.Add(p);
                _logger.LogExtension("Create Client", p);
                return true;
            }
            catch
            {
                _logger.LogExtension("Couldn`t create Client", p, LogLevel.Error);
                return false;
            }
        }

        public bool Update(Client p)
        {
            try
            {
                if (p != null)
                {
                    db.Entry(p).State = EntityState.Modified;
                    _logger.LogExtension("Update Client", p);
                    return true;
                }
                throw new Exception();
            }
            catch
            {
                _logger.LogExtension("Couldn`t update Client", p, LogLevel.Error);
                return false;
            }
        }

        //haven`t reason for delete client
        public bool Delete(int id)
        {
            return false;
            //try
            //{
            //    Client p = db.Clients.Include(c => c.Cars).ToList().Where(c => c.Id == id).First();
            //    if (p != null)
            //    {
            //        db.Clients.Remove(p);
            //        _logger.LogExtension("Delete Client", p);
            //        return true;
            //    }
            //    throw new Exception();
            //}
            //catch
            //{
            //    _logger.LogExtension("Couldn`t delete Client with id", id, LogLevel.Error);
            //    return false;
            //}
        }
    }
}
