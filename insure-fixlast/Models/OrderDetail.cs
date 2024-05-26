namespace insure_fixlast.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int EmployeeSubServiceId { get; set; }
        public EmployeeSubService EmployeeSubServices { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
