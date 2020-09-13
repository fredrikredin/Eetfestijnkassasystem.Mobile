using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class Order : EntityBase, ITransferObjectFor<OrderModel>
    {
        public Order()
        {
            if (MenuItems == null)
                MenuItems = new List<MenuItem>();
        }

        public Order(string customerName) : this()
        {
            CustomerName = customerName;
        }

        public string CustomerName { get; set; }
        public Payment Payment { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public OrderModel ToModelEntity()
        {
            return new OrderModel()
            {
                CustomerName = this.CustomerName,
                Payment = this.Payment?.ToModelEntity(),
                Seating = this.Seating,
                Comment = this.Comment,
            };
        }
    }
}
