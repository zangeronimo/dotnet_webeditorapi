using System.Linq.Expressions;
using WEBEditorAPI.Application.Exceptions;

namespace WEBEditorAPI.Infrastructure.Persistence.Query;

public static class OrderByHelper
{
    public static IQueryable<T> ApplyOrdering<T>(
            IQueryable<T> query,
            string? orderBy,
            bool desc,
            Dictionary<string, Expression<Func<T, object?>>>? customMap = null,
            HashSet<string>? allowedFields = null)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        var field = orderBy.Trim();

        if (allowedFields != null && !allowedFields.Contains(field))
            throw new ApiBadRequestException($"Ordering por '{field}' não é permitido.");

        Expression<Func<T, object?>> expression;

        if (customMap != null && customMap.TryGetValue(field, out var customExpr))
        {
            expression = customExpr;
        }
        else
        {
            expression = BuildExpression<T>(field);
        }

        return desc
            ? query.OrderByDescending(expression)
            : query.OrderBy(expression);
    }

    private static Expression<Func<T, object?>> BuildExpression<T>(string propertyPath)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        Expression body = parameter;
        foreach (var member in propertyPath.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }
        body = Expression.Convert(body, typeof(object));
        return Expression.Lambda<Func<T, object?>>(body, parameter);
    }
}
