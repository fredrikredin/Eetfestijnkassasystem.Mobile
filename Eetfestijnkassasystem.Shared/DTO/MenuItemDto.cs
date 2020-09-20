using Eetfestijnkassasystem.Shared.Abstract;
using System.Collections.Generic;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class MenuItemDto : EntityBase
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public List<int> AddedToOrders { get; set; }
        public int AccumulatedOrderCount { get; set; }

        public MenuItemDto()
        {
            if (AddedToOrders == null)
                AddedToOrders = new List<int>();
        }

        public MenuItemDto(string name) : this()
        {
            Name = name;
        }
    }
}
