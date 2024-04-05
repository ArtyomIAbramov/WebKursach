using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IDbRepository db;
        public OrderService(IDbRepository dbRepos)
        {
            db = dbRepos;
        }

        public Order MakeOrder(
            DateTime dateTime,
            Client client,
            Car car,
            Employee employee)
        {
            Order order = new Order()
            {
                Car = car,
                Employee = employee,
                Client = client,
                Contract_code = dateTime.ToString(),
                Order_date = dateTime,
                Order_price = car.Cost,
            };
            db.Orders.Create(order);

            if (db.Save() > 0)
                return GetOrder(order.Id);
            return null;

        }

        public List<Order> GetAllOrders()
        {
            return db.Orders.GetList().Select(i => new Order(i)).ToList();
        }

        public Order GetOrder(int Id)
        {
            return new Order(db.Orders.GetItem(Id));
        }
    }
}
