using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class DbRepository : IDbRepository
    {
        private DbAutoSalonContext db;
        private CarRepository carRepository;
        private ClientRepository clientRepository;
        private OrderRepository orderRepository;
        private EmployeeRepository employeeRepository;

        public DbRepository(DbAutoSalonContext dbAutoSalonContext)
        {
            db = dbAutoSalonContext;
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (carRepository == null)
                    carRepository = new CarRepository(db);
                return carRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
