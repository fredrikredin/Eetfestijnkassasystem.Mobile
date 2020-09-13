using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class MenuItemModel : EntityBase, IModelFor<MenuItem>
    {
        private double _cost = 0.0;
        private string _name = null;

        public MenuItemModel() : base() 
        {
            if (OrderMenuItems == null)
                OrderMenuItems = new List<OrderMenuItem>();
        }

        public List<OrderMenuItem> OrderMenuItems { get; set; }

        public string Name
        {
            get { return _name; }
            set 
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(MenuItem), nameof(Name));

                _name = value.Trim();
            }
        }

        public double Cost 
        {
            get { return _cost; }
            set 
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(MenuItem), nameof(Cost), value);

                _cost = value;
            }
        }

        public MenuItem ToTransferObject()
        {
            return new MenuItem()
            {
                Id = this.Id,
                DateTimeCreated = this.DateTimeCreated,
                Name = this.Name,
                Cost = this.Cost,
                Orders = this.OrderMenuItems.Select(o => o.OrderId).ToList()
            };
        }
    }
}
