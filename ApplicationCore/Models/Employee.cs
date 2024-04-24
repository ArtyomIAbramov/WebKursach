
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

        public List<Car>? SoldCars { get; set; }

        public EmployeePositionEnum EmployeePosition { get; set; } = EmployeePositionEnum.Default;

        public Employee() { }

        public enum EmployeePositionEnum
        {
            Default,
            Fired,
        }
    }
}
