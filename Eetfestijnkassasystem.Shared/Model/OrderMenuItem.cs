namespace Eetfestijnkassasystem.Shared.Model
{
    public class OrderMenuItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
