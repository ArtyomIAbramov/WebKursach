
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

        public Car(Car a)
        {
            Id = a.Id;
            Brand = a.Brand;
            Model = a.Model;
            Color = a.Color;
            Cost = a.Cost;
            Max_speed = a.Max_speed;
            Power = a.Power;
            Position = a.Position;
        }
    }

    public enum Position
    {
        InStock,
        InShop,
        UnAvailable,
        Default
    }
}
