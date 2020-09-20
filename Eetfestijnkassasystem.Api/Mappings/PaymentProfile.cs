using AutoMapper;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Api.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
