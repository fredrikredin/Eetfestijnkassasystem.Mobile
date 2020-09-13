using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class MenuItem : EntityBase, ITransferObjectFor<MenuItemModel>
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        //public List<Order> Orders { get; set; }
        public List<int> Orders { get; set; }

        public MenuItem()
        {
            if (Orders == null)
                Orders = new List<int>();
        }

        public MenuItem(string name) : this()
        {
            Name = name;
        }

        public MenuItemModel ToModelEntity()
        {
            return new MenuItemModel()
            {
                Name = this.Name,
                Cost = this.Cost,
            };
        }
    }
}
