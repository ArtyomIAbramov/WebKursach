using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private DbAutoSalonContext db;
        public CarRepository(DbAutoSalonContext db)
        {
            this.db = db;
        }

        public List<Car> GetList()
        {
            return db.Cars.ToList();
        }


        public Car GetItem(int Id)
        {
            return db.Cars.Find(Id);
        }

        public void Create(Car p)
        {
            db.Cars.Add(p);
        }

        public void Update(Car p)
        {
            db.Entry(p).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Car p = db.Cars.Find(id);
            if (p != null)
            {
                db.Cars.Remove(p);
            }
        }
    }
}
