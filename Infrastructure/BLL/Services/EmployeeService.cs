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
            return db.Employees.GetList().Select(i => new Employee(i)).ToList();
        }

        public Employee GetEmployee(int Id)
        {
            return new Employee(db.Employees.GetItem(Id));
        }

        public void CreateEmployee(
            string name,
            string surname,
            string post,
            string phonenumber,
            string address,
            string passport,
            string email,
            int salary)
        {
            db.Employees.Create(new Employee()
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

            Save();
        }

        public void UpdateEmployee(Employee p)
        {
            Employee ph = db.Employees.GetItem(p.Id);
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

            Save();
        }

        public void DeleteEmployee(int id)
        {
            Employee p = db.Employees.GetItem(id);
            if (p != null)
            {
                db.Employees.Delete(p.Id);
                Save();
            }
        }

        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
