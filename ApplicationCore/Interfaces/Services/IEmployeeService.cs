using WebKursach.ApplicationCore.Models;

namespace WebKursach.ApplicationCore.Interfaces.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int Id);
        void CreateEmployee(
            string name,
            string surname,
            string post,
            string phonenumber,
            string address,
            string passport,
            string email,
            int salary);
        void UpdateEmployee(Employee p);
        void DeleteEmployee(int id);
    }
}
