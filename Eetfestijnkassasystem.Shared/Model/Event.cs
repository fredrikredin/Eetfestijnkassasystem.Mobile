using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Event : EntityBase
    {
        private string _name = null;

        public Event() : base() 
        {
            if (Orders == null)
                Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException(nameof(EventDto), nameof(Name));

                _name = value.Trim();
            }
        }
    }
}
