using Eetfestijnkassasystem.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Event : IEntity
    {
        public Event() 
        {
        
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public List<Order> Orders { get; set; }
    }
}
