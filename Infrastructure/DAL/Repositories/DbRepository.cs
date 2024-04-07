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

        private readonly ILogger<DbAutoSalonContext> _logger;

        public DbRepository(DbAutoSalonContext dbAutoSalonContext, ILogger<DbAutoSalonContext> logger)
        {
            db = dbAutoSalonContext;
            _logger = logger;
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (carRepository == null)
                    carRepository = new CarRepository(db, _logger);
                return carRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db, _logger);
                return clientRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db, _logger);
                return orderRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db, _logger);
                return employeeRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
