
namespace WebKursach.ApplicationCore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Post { get; set; }

        public string Empphonenumber { get; set; }

        public string Empaddress { get; set; }

        public string Emppassport { get; set; }

        public string Email { get; set; }

        public int Salary { get; set; }

        public int TotalSold { get; set; }

        public List<Car> SoldCars { get; set; }

        public Employee() { }

        public Employee(Employee a)
        {
            Id = a.Id;
            Name = a.Name;
            Surname = a.Surname;
            Post = a.Post;
            Empphonenumber = a.Empphonenumber;
            Empaddress = a.Empaddress;
            Emppassport = a.Emppassport;
            Email = a.Email;
            Salary = a.Salary;
            TotalSold = 0;
            foreach (var car in a.SoldCars)
            {
                TotalSold += car.Cost;

            }
            SoldCars = a.SoldCars;
        }
    }
}
