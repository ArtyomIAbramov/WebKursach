using WebKursach.ApplicationCore.Interfaces.Repositories;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.BLL.Services
{
    public class CarService : ICarService
    {
        private IDbRepository db;
        public CarService(IDbRepository dbRepos)
        {
            db = dbRepos;
        }

        public List<Car> GetAllCars()
        {
            return db.Cars.GetList();
        }

        public Car GetCar(int Id)
        {
            return db.Cars.GetItem(Id);
        }

        public bool CreateCar(
            string brand, 
            string model, 
            string color,
            int max_speed,
            int power,
            Position position)
        {
            var carCreated = db.Cars.Create(new Car()
            {
                Brand = brand,
                Model = model,
                Color = color,
                Max_speed = max_speed,
                Power = power,
                Position = position,
            });
            if (carCreated)
            {
                Save();
                return true;
            }
            return false;

        }

        public bool UpdateCar(Car p)
        {
            Car ph = db.Cars.GetItem(p.Id);

            if(ph != null)
            {
                ph.Id = p.Id;
                ph.Brand = p.Brand;
                ph.Model = p.Model;
                ph.Color = p.Color;
                ph.Max_speed = p.Max_speed;
                ph.Power = p.Power;
                ph.Position = p.Position;
                if(db.Cars.Update(ph))
                {
                    Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteCar(int id)
        {
            Car p = db.Cars.GetItem(id);
            if(p == null)
                return false;

            var carDeleted = db.Cars.Delete(p.Id);
            if (carDeleted)
            {
                Save();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if (db.Save() > 0) 
                return true;
            return false;
        }
    }
}
