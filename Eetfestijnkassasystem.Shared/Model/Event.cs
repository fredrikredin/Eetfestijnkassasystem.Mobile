using System;
using System.Collections.Generic;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Event : EntityBase, IEntity
    {
        private string _name = null;

        public Event() : base() { }

        public List<Order> Orders { get; set; }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException<Event>(this, nameof(Name));

                _name = value.Trim();
            }
        }
    }
}
