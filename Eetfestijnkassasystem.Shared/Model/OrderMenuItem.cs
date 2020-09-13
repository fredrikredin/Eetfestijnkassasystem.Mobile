namespace Eetfestijnkassasystem.Shared.Model
{
    public class OrderMenuItem
    {
        public OrderMenuItem()
        { 
        }

        public OrderMenuItem(OrderModel order, MenuItemModel menuItem)
        {
            Order = order;
            OrderId = Order.Id;
            MenuItem = menuItem;
            MenuItemId = MenuItem.Id;
        }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItemModel MenuItem { get; set; }
        public int MenuItemCount { get; set; }
    }
}
