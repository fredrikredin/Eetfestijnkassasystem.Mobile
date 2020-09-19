using Eetfestijnkassasystem.Shared.Abstract;
using System.Collections.Generic;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class MenuItemDto : EntityBase
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public List<int> Orders { get; set; }

        public MenuItemDto()
        {
            if (Orders == null)
                Orders = new List<int>();
        }

        public MenuItemDto(string name) : this()
        {
            Name = name;
        }
    }
}
