using Eetfestijnkassasystem.Shared.Abstract;
using System.Collections.Generic;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class EventDto : EntityBase 
    {
        public string Name { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
