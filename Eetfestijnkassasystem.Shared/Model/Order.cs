using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Order : EntityBase, IEntity
    {
        private string _customerName = null;

        public Order() : base() { } 

        public List<OrderMenuItem> OrderMenuItems { get; set; }

        public Payment Payment { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException<Order>(this, nameof(CustomerName));

                _customerName = value.Trim();
            }
        }

        // [JsonIgnore]
        public IEnumerable<MenuItem> Items => OrderMenuItems.Select(o => o.MenuItem);
        public bool IsPaid => Payment != null;
        public double TotalCost => OrderMenuItems?.Sum(o => o.MenuItem.Cost) ?? 0;
    }
}