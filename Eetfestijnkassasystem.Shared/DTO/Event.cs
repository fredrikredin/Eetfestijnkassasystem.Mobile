using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;
using System.Collections.Generic;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class Event : EntityBase, ITransferObjectFor<EventModel>
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }

        public EventModel ToModelEntity()
        {
            return new EventModel()
            {
                Name = this.Name
            };
        }
    }
}
