using AutoMapper;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Api.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
