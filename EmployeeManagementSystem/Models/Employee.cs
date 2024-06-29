namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

}
