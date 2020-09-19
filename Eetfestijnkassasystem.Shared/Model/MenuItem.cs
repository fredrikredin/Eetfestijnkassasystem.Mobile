using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class MenuItem : EntityBase
    {
        private double _cost = 0.0;
        private string _name = null;

        public MenuItem() : base() 
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(MenuItemDto), nameof(Name));

                _name = value.Trim();
            }
        }

        public double Cost 
        {
            get { return _cost; }
            set 
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(MenuItemDto), nameof(Cost), value);

                _cost = value;
            }
        }
    }
}
