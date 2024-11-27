﻿using Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Repo
{
    public static class PradicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return param => true; }

        /// <summary>
        /// Creates a predicate that evaluates to false.
        /// </summary>
        public static Expression<Func<T, bool>> False<T>() { return param => false; }

        /// <summary>
        /// Creates a predicate expression from the specified lambda expression.
        /// </summary>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate) { return predicate; }

        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// Negates the predicate.
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, object>> CreateSortExpression<T>(string fieldName)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                throw new Exception($"No field Found named {fieldName}");

            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.Convert(Expression.Property(param, property), typeof(object));

            return Expression.Lambda<Func<T, object>>(body, param);
        }

        public static Expression GetExpression<T>(ParameterExpression param, CommonFilterParams filter)
        {
            var prop = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(filter.FieldName, StringComparison.OrdinalIgnoreCase));

            if (prop is null)
                throw new Exception($"Invalid field name {filter.FieldName}");

            // The member you want to evaluate (x => x.FirstName)
            MemberExpression member = Expression.Property(param, prop);

            // The value you want to evaluate
            var filterOperator = Operators.GetValue(filter.Operator);

            string refinedFilterValue = filter.Value?.ToString()?.Replace("%", "[%]")?.Replace("_", "[_]")?.Replace("[", "[[]")!;
            var value = filterOperator switch
            {
                Operator.Contains => $"%{refinedFilterValue}%",
                Operator.StartsWith => $"{refinedFilterValue}%",
                Operator.EndsWith => $"%{refinedFilterValue}",
                _ => filter.Value,
            };
            ConstantExpression constant = Expression.Constant(value);


            // Determine how we want to apply the expression
            switch (filterOperator)
            {
                case Operator.Equals:
                    return Expression.Equal(member, constant);
                
                case Operator.NotEquals:
                    return Expression.NotEqual(member, constant);

                case Operator.Contains:
                    return Expression.Call(EF.Functions.GetType().GetMethod("Like")!, member, constant);

                case Operator.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Operator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Operator.LessThan:
                    return Expression.LessThan(member, constant);

                case Operator.LessThanOrEqualTo:
                    return Expression.LessThanOrEqual(member, constant);

                case Operator.StartsWith:
                    return Expression.Call(EF.Functions.GetType().GetMethod("Like")!, member, constant);

                case Operator.EndsWith:
                    return Expression.Call(EF.Functions.GetType().GetMethod("Like")!, member, constant);
            }

            return null;
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}