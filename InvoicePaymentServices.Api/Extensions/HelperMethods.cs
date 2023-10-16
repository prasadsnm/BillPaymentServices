using AutoMapper;
using System.Linq.Expressions;

namespace InvoicePaymentServices.Api.Extensions
{
    public static class HelperMethods
    {
        public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector
        )
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}
