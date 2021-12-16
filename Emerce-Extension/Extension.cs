using Emerce_Model.Product;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Emerce_Extension
{
    public static class Extension
    {
        public static void ToTurkishLira( this ProductViewModel product )
        {
            product.Price = $"{product.Price} ₺";
        }
        public static IQueryable<TEntity> OrderBy<TEntity>( this IQueryable<TEntity> source, string orderByProperty, bool desc )
        {
            //lowercase property cannot be found because entity has upper case first letters
            //orderByProperty = name doesnt work but orderByProperty = Name works. 
            //this is why we convert first letter to uppercase
            //we use ToUpperInvariant() because of culture diffrences.
            //Turkish 'i' is different from English 'i' and so
            //when converted with ToUpper() it becomes 'İ' not 'I'.
            orderByProperty = char.ToUpperInvariant(orderByProperty[0]) + orderByProperty.Substring(1);
            //bool desc = true => OrderByDescending
            //bool desc = false => OrderBy
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
