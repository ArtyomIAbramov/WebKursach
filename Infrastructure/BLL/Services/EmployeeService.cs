using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IDbRepository db;
        public EmployeeService(IDbRepository dbRepos)
        {
            db = dbRepos;
        }

        public List<Employee> GetAllEmployees()
        {
            return db.Employees.GetList().Where(e=>e.EmployeePosition != Employee.EmployeePositionEnum.Fired).ToList();
        }

        public Employee GetEmployee(int Id)
        {
            return db.Employees.GetItem(Id);
        }

        public bool CreateEmployee(
            string name,
            string surname,
            string post,
            string phonenumber,
            string address,
            string passport,
            string email,
            int salary)
        {
            var employeeCreated = db.Employees.Create(new Employee()
            {
                Name = name,
                Surname = surname,
                Post = post,
                Empphonenumber = phonenumber,
                Empaddress = address,
                Emppassport = passport,
                Email = email,
                Salary = salary,
                TotalSold = 0,
                SoldCars = new List<Car>(),
            });

            if (employeeCreated)
            {
                Save();
                return true;
            }
            return false;
        }

        public bool UpdateEmployee(Employee p)
        {
            Employee ph = db.Employees.GetItem(p.Id);

            if(ph != null)
            {
                ph.Id = p.Id;
                ph.Name = p.Name;
                ph.Surname = p.Surname;
                ph.Post = p.Post;
                ph.Empphonenumber = p.Empphonenumber;
                ph.Empaddress = p.Empaddress;
                ph.Emppassport = p.Emppassport;
                ph.Email = p.Email;
                ph.Salary = p.Salary;
                ph.TotalSold = p.TotalSold;
                ph.SoldCars = p.SoldCars;

                if (db.Employees.Update(ph))
                {
                    Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            Employee p = db.Employees.GetItem(id);
            if(p != null)
            {
                var employeeDeleted = db.Employees.Delete(p.Id);

                if (employeeDeleted)
                {
                    Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
