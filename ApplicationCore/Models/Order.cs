
namespace WebKursach.ApplicationCore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Order_date { get; set; }

        public int Order_price { get; set; }

        public Car Car { get; set; }

        public Employee Employee { get; set; }

        public Client Client { get; set; }

        public string Contract_code { get; set; }

        public Order() { }

        public Order(Order order)
        {
            Id = order.Id;
            Order_date = order.Order_date;
            Order_price = order.Order_price;
            Car = order.Car;
            Employee = order.Employee;
            Client = order.Client;
            Contract_code = order.Contract_code;
        }
    }
}
