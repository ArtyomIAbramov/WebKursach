using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int Id);
        bool CreateEmployee(
            string name,
            string surname,
            string post,
            string phonenumber,
            string address,
            string passport,
            string email,
            int salary);
        bool UpdateEmployee(Employee p);
        bool DeleteEmployee(int id);
    }
}
