using System;
using System.Collections.Generic;
using System.Linq;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class EventModel : EntityBase, IModelFor<Event>
    {
        private string _name = null;

        public EventModel() : base() 
        {
            if (Orders == null)
                Orders = new List<OrderModel>();
        }

        public List<OrderModel> Orders { get; set; }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(Event), nameof(Name));

                _name = value.Trim();
            }
        }

        public Event ToTransferObject()
        {
            return new Event()
            {
                Id = this.Id,
                DateTimeCreated = this.DateTimeCreated,
                Name = this.Name,
                Orders = this.Orders.Select(o => o.ToTransferObject()).ToList(),
            };
        }
    }
}
