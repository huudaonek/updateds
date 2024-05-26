namespace insure_fixlast.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Level Level { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public ICollection<EmployeeSubService> EmployeeSubServices { get; set; }
        public bool isDelete { get; set; } = false;
    }

    public enum Level
    {
        manager,
        admin,
        employee
    }
}
    