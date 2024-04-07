
namespace WebKursach.ApplicationCore.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int Cost { get; set; }

        public int Max_speed { get; set; }

        public int Power { get; set; }

        public Position Position { get; set; }

        public Car() { }
    }

    public enum Position
    {
        InStock,
        InShop,
        UnAvailable,
        Default
    }
}
