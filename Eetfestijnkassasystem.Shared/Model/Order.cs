using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Order : EntityBase 
    {
        private string _customerName = null;

        public Order() : base() 
        {
            if (OrderMenuItems == null)
                OrderMenuItems = new List<OrderMenuItem>();
        }

        public List<OrderMenuItem> OrderMenuItems { get; set; }
        public Payment Payment { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(OrderDto), nameof(CustomerName));

                _customerName = value.Trim();
            }
        }

        //[JsonIgnore]
        //public bool IsPaid => Payment != null;
       
        //[JsonIgnore]
        //public double TotalCost => OrderMenuItems?.Sum(o => o.MenuItem.Cost) ?? 0;
    }
}