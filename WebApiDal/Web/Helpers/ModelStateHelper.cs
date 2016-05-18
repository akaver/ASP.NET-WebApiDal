using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    public static class ModelStateHelper
    {
        /// <summary>
        /// Strongly typed ModelState.Remove
        /// usage: ModelState.RemoveFor<SomeViewModel>(x => model.SomeObject.SomeValue)
        /// </summary>
        /// <typeparam name="TModel">ViewModel</typeparam>
        /// <param name="modelState"></param>
        /// <param name="expression">Lambda for parameter inside ViewModel</param>
        public static void RemoveFor<TModel>(this ModelStateDictionary modelState,
            Expression<Func<TModel, object>> expression)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);

            foreach (var ms in modelState.ToArray())
            {
                if (ms.Key.StartsWith(expressionText + ".") || ms.Key == expressionText)
                {
                    modelState.Remove(ms);
                }
            }
        }


        public static void AddFor<TModel>(this ModelStateDictionary modelState,
            Expression<Func<TModel, object>> expression, string errorMessage)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);
            modelState.AddModelError(expressionText, errorMessage);
        }
    }
}