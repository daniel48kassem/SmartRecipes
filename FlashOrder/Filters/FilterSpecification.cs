// using System;
// using System.Linq;
// using System.Linq.Expressions;
// using FlashOrder.Data;
//
// namespace FlashOrder
// {
//     public abstract class FilterSpecification<T>
//     {
//         public abstract Expression<Func<T, IQueryable<T>>> SpecificationExpression(dynamic value);
//     }
//
//     public class ContainsIngredientsSpecification : FilterSpecification<Recipe>
//     {
//         public override Expression<Func<Recipe, IQueryable<Recipe>>> SpecificationExpression(dynamic value)
//         {
//             IQueryable<Recipe> query;
//             query.Where(q=>q.Ingredients.Where(ingredient=>ingredient.Item.Name.Equals()));
//                 // foreach (var single in value)
//                 // {
//                 //     p.Ingredients
//                 // }
//
//                 return query;
//             }
//     }
// }