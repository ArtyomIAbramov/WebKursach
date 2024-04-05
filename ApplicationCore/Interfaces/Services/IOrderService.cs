using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IOrderService
    {
        Order MakeOrder(
            DateTime dateTime,
            Client client,
            Car car,
            Employee employee);
        List<Order> GetAllOrders();
        Order GetOrder(int Id);
    }
}
