using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Web.Helpers
{
    public static class HtmlHelper
    {
        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Replace("\n", "<br />\r\n");

            if (String.IsNullOrEmpty(model))
                return html.DisplayFor(expression);

            return MvcHtmlString.Create(model);
        }

        public static MvcHtmlString RawHtmlDecodeDisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            //Html.Raw(HttpUtility.HtmlDecode(Html.DisplayNameFor(item => participant.DOB).ToString()));

            var result = html.DisplayNameFor(expression).ToString();
            return new MvcHtmlString(html.Raw(System.Web.HttpUtility.HtmlDecode(result)).ToString());
        }


        //public static MvcHtmlString DateTimeEditorFor<TModel, TValue>(
        //    this HtmlHelper<TModel> html,
        //    Expression<Func<TModel, TValue>> expression, object additionalViewData
        //    )
        //{
        //    var name = ExpressionHelper.GetExpressionText(expression);
        //    var fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

        //    var tagBuilder = new TagBuilder("input");

        //    tagBuilder.MergeAttribute("name", fullHtmlFieldName);
        //    //tagBuilder.MergeAttributes(new RouteValueDictionary(additionalViewData?.htmlAttributes));


        //    return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        //}

    }
}