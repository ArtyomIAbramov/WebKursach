using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.DAL;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private DbAutoSalonContext db;
        public EmployeeRepository(DbAutoSalonContext db)
        {
            this.db = db;
        }

        public List<Employee> GetList()
        {
            return db.Employees.ToList();
        }


        public Employee GetItem(int Id)
        {
            return db.Employees.Find(Id);
        }

        public void Create(Employee p)
        {
            db.Employees.Add(p);
        }

        public void Update(Employee p)
        {
            db.Entry(p).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Employee p = db.Employees.Find(id);
            if (p != null)
            {
                db.Employees.Remove(p);
            }
        }
    }
}
