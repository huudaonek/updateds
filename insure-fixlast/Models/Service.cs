using NuGet.Protocol.Plugins;

namespace insure_fixlast.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string benefit { get; set; }
        public decimal price { get; set; }
        public Rank rank { get; set; }
        public int Quantity { get; set; }
        public bool isDelete { get; set; } = false;
    }
    public enum Rank
    {
        R, SR, SSR, UR, LR
    }
}
