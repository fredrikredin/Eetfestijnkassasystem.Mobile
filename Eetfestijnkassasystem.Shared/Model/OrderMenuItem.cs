namespace Eetfestijnkassasystem.Shared.Model
{
    public class OrderMenuItem
    {
        public OrderMenuItem()
        { 
        }

        public OrderMenuItem(Order order, MenuItem menuItem)
        {
            Order = order;
            OrderId = Order.Id;
            MenuItem = menuItem;
            MenuItemId = MenuItem.Id;
        }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int MenuItemCount { get; set; }
    }
}
