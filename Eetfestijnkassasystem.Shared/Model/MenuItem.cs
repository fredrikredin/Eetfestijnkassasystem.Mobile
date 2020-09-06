using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class MenuItem : EntityBase, IEntity
    {
        private double _cost = 0.0;
        private string _name = null;

        public MenuItem() : base() { }

        public List<OrderMenuItem> OrderMenuItems { get; set; }

        public string Name
        {
            get { return _name; }
            set 
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException<MenuItem>(this, nameof(Name));

                _name = value.Trim();
            }
        }

        public double Cost 
        {
            get { return _cost; }
            set 
            {
                if (value < 0)
                    throw new NegativeValueException<MenuItem>(this, nameof(Cost), value);

                _cost = value;
            }
        }

        // [JsonIgnore]
        public IEnumerable<Order> Orders => OrderMenuItems.Select(o => o.Order);
    }
}
