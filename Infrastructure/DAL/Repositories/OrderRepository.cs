using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.Extensions;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private DbAutoSalonContext db;
        private readonly ILogger _logger;
        public OrderRepository(DbAutoSalonContext db, ILogger logger)
        {
            this.db = db;
            _logger = logger;
        }

        public List<Order> GetList()
        {
            try
            {
                var orders = db.Orders.Include(c => c.Client.Cars).Include(car => car.Car).Include(e => e.Employee.SoldCars).ToList();
                _logger.LogExtension("Get Orders", orders);

                return orders;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Orders", "", LogLevel.Error);
                return null;
            }
        }


        public Order GetItem(int Id)
        {
            try
            {
                var order = db.Orders.Include(c => c.Client.Cars).Include(car => car.Car).Include(e => e.Employee.SoldCars).ToList().Where(p => p.Id == Id).First();

                if (order == null)
                {
                    throw new Exception();
                }

                _logger.LogExtension("Get Order", order);

                return order;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Order with Id", Id, LogLevel.Error);
                return null;
            }
        }

        public bool Create(Order p)
        {
            try
            {
                db.Orders.Add(p);
                _logger.LogExtension("Create Order", p);
                return true;
            }
            catch
            {
                _logger.LogExtension("Couldn`t create Order", p, LogLevel.Error);
                return false;
            }
        }

        public bool Update(Order p)
        {
            return false;
            //try
            //{
            //    if(p != null)
            //    {
            //        db.Entry(p).State = EntityState.Modified;
            //        _logger.LogExtension("Update Order", p);
            //        return true;
            //    }
            //    throw new Exception();
            //}
            //catch
            //{
            //    _logger.LogExtension("Couldn`t update Order", p, LogLevel.Error);
            //    return false;
            //}
        }

        public bool Delete(int id)
        {
            return false;
            //try
            //{
            //    Order p = db.Orders.Find(id);
            //    if (p != null)
            //    {
            //        db.Orders.Remove(p);
            //    }
            //    _logger.LogExtension("Delete Order", p);
            //}
            //catch
            //{
            //    _logger.LogExtension("Couldn`t delete Order with id", id, LogLevel.Error);
            //}
        }
    }
}
