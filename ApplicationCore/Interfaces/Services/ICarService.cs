using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        List<Car> GetAllAvailableCars();
        List<Car> GetAllSoldCars();
        Car GetCar(int Id);
        bool CreateCar(
            string brand,
            string model,
            string color,
            int max_speed,
            int power);
        bool UpdateCar(Car p);
        bool DeleteCar(int id);
    }
}
