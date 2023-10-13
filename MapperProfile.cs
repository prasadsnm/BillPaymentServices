using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InvoicePaymentServices.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Infra.DBEntities.Invoice, Core.Invoice>().ReverseMap();
        }
    }
}
