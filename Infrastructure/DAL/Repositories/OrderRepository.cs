using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.DAL;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private DbAutoSalonContext db;
        public OrderRepository(DbAutoSalonContext db)
        {
            this.db = db;
        }

        public List<Order> GetList()
        {
            return db.Orders.ToList();
        }


        public Order GetItem(int Id)
        {
            return db.Orders.Find(Id);
        }

        public void Create(Order p)
        {
            db.Orders.Add(p);
        }

        public void Update(Order p)
        {
            db.Entry(p).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order p = db.Orders.Find(id);
            if (p != null)
            {
                db.Orders.Remove(p);
            }
        }
    }
}
