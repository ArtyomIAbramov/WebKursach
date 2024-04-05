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
            return db.Cars.GetList().Select(i => new Car(i)).ToList();
        }

        public Car GetCar(int Id)
        {
            return new Car(db.Cars.GetItem(Id));
        }

        public void CreateCar(
            string brand, 
            string model, 
            string color,
            int max_speed,
            int power,
            Position position)
        {
            db.Cars.Create(new Car()
            {
                Brand = brand,
                Model = model,
                Color = color,
                Max_speed = max_speed,
                Power = power,
                Position = position,
            });
            Save();
        }

        public void UpdateCar(Car p)
        {
            Car ph = db.Cars.GetItem(p.Id);
            ph.Id = p.Id;
            ph.Brand = p.Brand;
            ph.Model = p.Model;
            ph.Color = p.Color;
            ph.Max_speed = p.Max_speed;
            ph.Power = p.Power;
            ph.Position = p.Position;

            Save();
        }

        public void DeleteCar(int id)
        {
            Car p = db.Cars.GetItem(id);
            if (p != null)
            {
                db.Cars.Delete(p.Id);
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
