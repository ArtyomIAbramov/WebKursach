using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.DAL;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private DbAutoSalonContext db;
        public ClientRepository(DbAutoSalonContext db)
        {
            this.db = db;
        }

        public List<Client> GetList()
        {
            return db.Clients.ToList();
        }

        public Client GetItem(int Id)
        {
            return db.Clients.Find(Id);
        }

        public void Create(Client p)
        {
            db.Clients.Add(p);
        }

        public void Update(Client p)
        {
            db.Entry(p).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Client p = db.Clients.Find(id);
            if (p != null)
            {
                db.Clients.Remove(p);
            }
        }
    }
}
