using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IDbRepository db;
        private IClientService _clientService;
        private ICarService _carService;
        private IEmployeeService _employeeService;

        public OrderService(IDbRepository dbRepos, IClientService clientService, ICarService carService, IEmployeeService employeeService)
        {
            db = dbRepos;
            _clientService = clientService;
            _carService = carService;
            _employeeService = employeeService;
        }

        public Order MakeOrder(
            DateTime dateTime,
            Client client,
            Car car,
            Employee employee)
        {
            var carnew = _carService.GetCar(car.Id);

            if (carnew == null)
            {
                return null;
            }

            var employeenew = _employeeService.GetEmployee(employee.Id);

            if (employeenew == null)
            {
                return null;
            }

            var clientnew = _clientService.GetClient(client.Id);

            if (clientnew == null)
            {
                return null;
            }


            Order order = new Order()
            {
                Car = carnew,
                Employee = employeenew,
                Client = clientnew,
                Contract_code = dateTime.ToString(),
                Order_date = dateTime,
                Order_price = car.Cost,
            };

            var orderCreated = db.Orders.Create(order);

            if (orderCreated)
            {
                employeenew.SoldCars.Add(carnew);
                clientnew.Cars.Add(carnew);
                carnew.CarPosition = CarPosition.Sold;
                clientnew.ClientPosition = Client.ClientPositionEnum.Default;

                if (db.Save() > 0)
                {
                    return GetOrder(order.Id);
                }
            }
            return null;
        }

        public List<Order> GetAllOrders()
        {
            return db.Orders.GetList();
        }

        public Order GetOrder(int Id)
        {
            return db.Orders.GetItem(Id);
        }
    }
}
