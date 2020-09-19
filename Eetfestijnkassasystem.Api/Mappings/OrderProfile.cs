using AutoMapper;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Model;
using System.Linq;
using System.Collections.Generic;

namespace Eetfestijnkassasystem.Api.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.MenuItems, o => o.MapFrom<MenuItemsResolver>())
                .ReverseMap();
        }
    }

    public class MenuItemsResolver : IValueResolver<Order, OrderDto, List<MenuItemDto>>
    {
        public List<MenuItemDto> Resolve(Order source, OrderDto destination, List<MenuItemDto> destMember, ResolutionContext context)
        {
            List<MenuItemDto> dtos = new List<MenuItemDto>();

            foreach (OrderMenuItem omi in source.OrderMenuItems)
                foreach (int count in Enumerable.Range(0, omi.MenuItemCount))
                    dtos.Add(context.Mapper.Map<MenuItemDto>(omi.MenuItem));

            return dtos;
        }
    }
}
