using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Abstract;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class OrderDto : EntityBase
    {
        public OrderDto()
        {
            if (MenuItems == null)
                MenuItems = new List<MenuItemDto>();
        }

        public OrderDto(string customerName) : this()
        {
            CustomerName = customerName;
        }

        public string CustomerName { get; set; }
        public PaymentDto Payment { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
    }
}
