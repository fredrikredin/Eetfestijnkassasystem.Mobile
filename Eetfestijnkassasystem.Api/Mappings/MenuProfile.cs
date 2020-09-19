﻿using System.Linq;
using AutoMapper;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Api.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dto => dto.Orders, o => o.MapFrom(model => model.OrderMenuItems.Select(o => o.OrderId).ToList()))
                .ReverseMap();
        }
    }
}
