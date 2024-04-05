using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCar(int Id);
        void CreateCar(
            string brand,
            string model,
            string color,
            int max_speed,
            int power,
            Position position);
        void UpdateCar(Car p);
        void DeleteCar(int id);
    }
}
