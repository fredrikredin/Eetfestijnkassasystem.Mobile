using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class OrderModel : EntityBase, IModelFor<Order>
    {
        private string _customerName = null;

        public OrderModel() : base() 
        {
            if (OrderMenuItems == null)
                OrderMenuItems = new List<OrderMenuItem>();
        }

        public List<OrderMenuItem> OrderMenuItems { get; set; }

        public PaymentModel Payment { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(Order), nameof(CustomerName));

                _customerName = value.Trim();
            }
        }

        public Order ToTransferObject()
        {
            Order dto = new Order()
            {
                Id = this.Id,
                DateTimeCreated = this.DateTimeCreated,
                CustomerName = this.CustomerName,
                Payment = this.Payment?.ToTransferObject(),
                Seating = this.Seating,
                Comment = this.Comment,
            };

            foreach (OrderMenuItem omi in OrderMenuItems)
                foreach (int count in Enumerable.Range(0, omi.MenuItemCount))
                    dto.MenuItems.Add(omi.MenuItem.ToTransferObject());

            return dto;
        }

        //[JsonIgnore]
        //public bool IsPaid => Payment != null;
       
        //[JsonIgnore]
        //public double TotalCost => OrderMenuItems?.Sum(o => o.MenuItem.Cost) ?? 0;
    }
}