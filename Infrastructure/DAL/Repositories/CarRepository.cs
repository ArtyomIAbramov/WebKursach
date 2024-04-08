using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.Extensions;

namespace WebKursach.Infrastructure.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private DbAutoSalonContext db;
        private readonly ILogger _logger;
        public CarRepository(DbAutoSalonContext db, ILogger logger)
        {
            this.db = db;
            _logger = logger;
        }

        public List<Car> GetList()
        {
            try
            {
                var cars = db.Cars.ToList();
                _logger.LogExtension("Get Cars", cars);

                return cars;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Cars", "", LogLevel.Error);
                return null;
            }
        }

        public Car GetItem(int Id)
        {
            try
            {
                var car = db.Cars.Find(Id);

                if(car == null)
                {
                    throw new Exception();
                }

                _logger.LogExtension("Get Car", car);

                return car;
            }
            catch
            {
                _logger.LogExtension("Couldn`t get Car with Id", Id, LogLevel.Error);
                return null;
            }
        }

        public bool Create(Car p)
        {
            try
            {
                db.Cars.Add(p);
                _logger.LogExtension("Create Car", p);
                return true;
            }
            catch
            {
                _logger.LogExtension("Couldn`t create Car", p, LogLevel.Error);
                return false;
            }
        }

        public bool Update(Car p)
        {
            try
            {
                if(p != null)
                {
                    db.Entry(p).State = EntityState.Modified;
                    _logger.LogExtension("Update Car", p);
                    return true;
                }
                throw new Exception();

            }
            catch
            {
                _logger.LogExtension("Couldn`t update Car", p, LogLevel.Error);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Car p = db.Cars.Find(id);
                if (p != null)
                {
                    p.CarPosition = CarPosition.Deleted;
                    _logger.LogExtension("Delete Car", p);
                    return true;
                }
                throw new Exception();
            }
            catch
            {
                _logger.LogExtension("Couldn`t delete Car with id ", id, LogLevel.Error);
                return false;
            }
        }
    }
}
