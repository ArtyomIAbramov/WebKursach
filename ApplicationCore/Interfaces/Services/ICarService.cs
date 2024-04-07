using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCar(int Id);
        bool CreateCar(
            string brand,
            string model,
            string color,
            int max_speed,
            int power,
            Position position);
        bool UpdateCar(Car p);
        bool DeleteCar(int id);
    }
}
