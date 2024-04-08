
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

        public ClientPositionEnum ClientPosition { get; set; } = ClientPositionEnum.IsNew;

        public Client() { }

        public enum ClientPositionEnum
        {
            IsNew,
            Default,
        }
    }
}
