using AutoMapper;
using InvoicePaymentServices.Api.Extensions;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InvoicePaymentServices.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Infra.DBEntities.Invoice, Core.Models.Invoice>().IgnoreMember( x=> x.AvailableAction);
        }       
    }
}


