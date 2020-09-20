using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Order : EntityBase
    {
        private string _customerName = null;
        private Payment _payment = null;

        public Order() : base()
        {
            if (OrderMenuItems == null)
                OrderMenuItems = new List<OrderMenuItem>();
        }

        public List<OrderMenuItem> OrderMenuItems { get; set; }
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

        public Payment Payment
        {
            get { return _payment; }
            set
            {
                if (value != null && OrderMenuItems.Any() && TotalCost() != value.TotalCost)
                    throw new PaymentException(nameof(Order), nameof(Payment), 
                        "Total cost of payment does not equal the total cost of menu items");

                _payment = value;
            }
        }

        private double TotalCost() => OrderMenuItems.Sum(omi => omi.MenuItemCount * omi.MenuItem.Cost);
    }
}