using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Repositories
{
    public interface IDbRepository
    {
        IRepository<Car> Cars { get; }
        IRepository<Order> Orders { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Client> Clients { get; }
        int Save();
    }
}
