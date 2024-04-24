using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.Extensions;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private DbAutoSalonContext db;
        private readonly ILogger _logger;
        public EmployeeRepository(DbAutoSalonContext db, ILogger logger)
        {
            this.db = db;
            _logger = logger;
        }

        public List<Employee> GetList()
        {
            try
            {
                var employees = db.Employees.Include(c => c.SoldCars).ToList();
                _logger.LogExtension("Get Employees", employees);

                return employees;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Employees", "", LogLevel.Error);
                return null;
            }
        }


        public Employee GetItem(int Id)
        {
            try
            {
                var employee = db.Employees.Include(c => c.SoldCars).ToList().Where(c => c.Id == Id).First();

                if (employee == null)
                {
                    throw new Exception();
                }
                _logger.LogExtension("Get Employee", employee);

                return employee;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Employee with Id", Id, LogLevel.Error);
                return null;
            }
        }

        public bool Create(Employee p)
        {
            try
            {
                db.Employees.Add(p);
                _logger.LogExtension("Create Employee", p);
                return true;
            }
            catch
            {
                _logger.LogExtension("Couldn`t create Employee", p, LogLevel.Error);
                return false;
            }
        }

        public bool Update(Employee p)
        {
            try
            {
                if (p != null)
                {
                    if (p.SoldCars != null && p.SoldCars.Any())
                    {
                        foreach (var c in p.SoldCars)
                        {
                            c.Id = 0;
                            c.CarPosition = CarPosition.Deleted;
                        }
                    }
                    db.Entry(p).State = EntityState.Modified;
                    _logger.LogExtension("Update Employee", p);
                    return true;
                }
                throw new Exception();

            }
            catch
            {
                _logger.LogExtension("Couldn`t update Employee", p, LogLevel.Error);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Employee p = db.Employees.Include(c => c.SoldCars).ToList().Where(c => c.Id == id).First();
                if (p != null)
                {
                    p.EmployeePosition = Employee.EmployeePositionEnum.Fired;
                    _logger.LogExtension("Delete Employee", p);
                    return true;
                }
                throw new Exception();
            }
            catch
            {
                _logger.LogExtension("Couldn`t delete Employee with id", id, LogLevel.Error);
                return false;
            }
        }
    }
}
