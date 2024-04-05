using System.Drawing;

namespace WebKursach.ApplicationCore.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phonenumber { get; set; }

        public string Address { get; set; }

        public string Passport { get; set; }

        public List<Car> Cars { get; set; }

        public Client() { }

        public Client(Client a)
        {
            Id = a.Id;
            Name = a.Name;
            Surname = a.Surname;
            Phonenumber = a.Phonenumber;
            Address = a.Address;
            Passport = a.Passport;
            Cars = a.Cars;
        }
    }
}
