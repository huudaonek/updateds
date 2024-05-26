using Microsoft.AspNetCore.Identity;

namespace insure_fixlast.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
		public int AccountId { get; set; }
		public Account Account { get; set; }
		public ICollection<Employee> Employees { get; set; }
        public bool isDelete { get; set; } = false; 
    }
}
