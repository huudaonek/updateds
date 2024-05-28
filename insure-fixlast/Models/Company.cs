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
    public class CheckoutViewModel
    {
        public List<Service> SelectedServices { get; set; }
        public Dictionary<int, int> ServiceQuantities { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
    }

}
