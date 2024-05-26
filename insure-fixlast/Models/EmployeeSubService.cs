namespace insure_fixlast.Models
{
    public class EmployeeSubService
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set;}
    }
}
