using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HtmlHelpers.Properties;

namespace HtmlHelpers
{
    public static class DateTimeEditors
    {


        private static string _resourceClassKey;

        public static string ResourceClassKey
        {
            get { return _resourceClassKey ?? String.Empty; }
            set { _resourceClassKey = value; }
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.DateTimeEditorFor(expression, format: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            return htmlHelper.DateTimeEditorFor(expression, format, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.DateTimeEditorFor(expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            return htmlHelper.DateTimeEditorFor(expression, format: format, htmlAttributes: System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.DateTimeEditorFor(expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DateTimeEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return DateTimeHelper(htmlHelper,
                                 metadata,
                                 metadata.Model,
                                 ExpressionHelper.GetExpressionText(expression),
                                 format,
                                 htmlAttributes);
        }


        private static MvcHtmlString DateTimeHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(htmlHelper,
                               metadata,
                               expression,
                               model,
                               format: format,
                               htmlAttributes: htmlAttributes);
        }


        private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, object value, string format, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Argument cannot be null or empty", nameof(fullName));
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);

            tagBuilder.AddCssClass("single-line");
            tagBuilder.AddCssClass("text-box");

            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName, true);

            string valueParameter = htmlHelper.FormatValue(value, format);

            string attemptedValue = (string)GetModelStateValue(htmlHelper, fullName, typeof(string));
            tagBuilder.MergeAttribute("value", attemptedValue ?? valueParameter, true);



            tagBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            var unobtrusiveValidationAttributes = GetUnobtrusiveValidationAttributes(htmlHelper, name, metadata);

            unobtrusiveValidationAttributes = FixUnobtrusiveValidationAttributesForDateTime(htmlHelper, metadata, unobtrusiveValidationAttributes);

            tagBuilder.MergeAttributes(unobtrusiveValidationAttributes);

            return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
        }


        public static IDictionary<string, object> FixUnobtrusiveValidationAttributesForDateTime(HtmlHelper htmlHelper, ModelMetadata metadata, IDictionary<string, object> attributes)
        {
            var fieldName = !string.IsNullOrWhiteSpace(metadata.DisplayName)
                ? metadata.DisplayName
                : metadata.PropertyName;

            var dataTypeAttributeErrorMsg = GetErrorMessageForDataTypeAttribute(metadata);
            string errorMessage;

            switch (metadata.DataTypeName)
            {
                case "DateTime":
                    if (attributes.ContainsKey("data-val-date"))
                    {
                        attributes.Remove("data-val-date");
                    }
                    errorMessage = dataTypeAttributeErrorMsg ??
                                       GetFieldMustBeDateTimeResource(
                                           htmlHelper.ViewContext.Controller.ControllerContext);
                    attributes.Add("data-val-datetime", String.Format(errorMessage, fieldName));
                    break;
                case "Date":
                    if (attributes.ContainsKey("data-val-date"))
                    {
                        attributes.Remove("data-val-date");
                    }
                    errorMessage = dataTypeAttributeErrorMsg ??
                                   GetFieldMustBeDateResource(htmlHelper.ViewContext.Controller.ControllerContext);
                    attributes.Add("data-val-date", String.Format(errorMessage, fieldName));
                    break;
                case "Time":
                    if (attributes.ContainsKey("data-val-date"))
                    {
                        attributes.Remove("data-val-date");
                    }
                    errorMessage = dataTypeAttributeErrorMsg ??
                                   GetFieldMustBeTimeResource(htmlHelper.ViewContext.Controller.ControllerContext);
                    attributes.Add("data-val-time", String.Format(errorMessage, fieldName));
                    break;
            }

            return attributes;
        }

        private static string GetErrorMessageForDataTypeAttribute(ModelMetadata metadata)
        {
            string retVal = null;

            var customTypeDescriptor = new AssociatedMetadataTypeTypeDescriptionProvider(metadata.ContainerType).GetTypeDescriptor(metadata.ContainerType);
            if (customTypeDescriptor != null)
            {
                var descriptor = customTypeDescriptor.GetProperties().Find(metadata.PropertyName, true);
                var req = (new List<Attribute>(descriptor.Attributes.OfType<Attribute>())).OfType<DataTypeAttribute>().FirstOrDefault();

                if (req != null)
                {
                    retVal = (string) req.ErrorMessageResourceType?.GetProperty(req.ErrorMessageResourceName)?.GetMethod.Invoke(null,null);
                }
                
            }

            return retVal;
        }

        private static string GetFieldMustBeDateTimeResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "FieldMustBeDateTime") ?? MvcResources.ClientDataTypeModelValidatorProvider_FieldMustBeDateTime;
        }
        private static string GetFieldMustBeDateResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "FieldMustBeDate") ?? MvcResources.ClientDataTypeModelValidatorProvider_FieldMustBeDate;
        }
        private static string GetFieldMustBeTimeResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "FieldMustBeTime") ?? MvcResources.ClientDataTypeModelValidatorProvider_FieldMustBeTime;
        }


        // If the user specified a ResourceClassKey try to load the resource they specified.
        // If the class key is invalid, an exception will be thrown.
        // If the class key is valid but the resource is not found, it returns null, in which
        // case it will fall back to the MVC default error message.
        private static string GetUserResourceString(ControllerContext controllerContext, string resourceName)
        {
            string result = null;

            if (!String.IsNullOrEmpty(ResourceClassKey) && (controllerContext != null) && (controllerContext.HttpContext != null))
            {
                result = controllerContext.HttpContext.GetGlobalResourceObject(ResourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string;
            }

            return result;
        }

        static object GetModelStateValue(HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }


        static bool EvalBoolean(HtmlHelper htmlHelper, string key)
        {
            return Convert.ToBoolean(htmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
        }


        static string EvalString(HtmlHelper htmlHelper, string key, string format)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }


        internal static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            Debug.Assert(tagBuilder != null);
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }


        public static IDictionary<string, object> GetUnobtrusiveValidationAttributes(HtmlHelper htmlHelper, string name)
        {
            return GetUnobtrusiveValidationAttributes(htmlHelper, name, metadata: null);
        }

        // Only render attributes if unobtrusive client-side validation is enabled, and then only if we've
        // never rendered validation for a field with this name in this form. Also, if there's no form context,
        // then we can't render the attributes (we'd have no <form> to attach them to).
        public static IDictionary<string, object> GetUnobtrusiveValidationAttributes(HtmlHelper htmlHelper, string name, ModelMetadata metadata)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();

            // The ordering of these 3 checks (and the early exits) is for performance reasons.
            if (!htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                return results;
            }

            FormContext formContext = GetFormContextForClientValidation(htmlHelper);
            if (formContext == null)
            {
                return results;
            }

            string fullName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (formContext.RenderedField(fullName))
            {
                return results;
            }

            formContext.RenderedField(fullName, true);

            //IEnumerable<ModelClientValidationRule> clientRules = htmlHelper.ClientValidationRuleFactory(name, metadata);
            //copy of the ClientValidationRuleFactory method
            IEnumerable<ModelClientValidationRule> clientRules = ModelValidatorProviders.Providers.GetValidators(metadata ?? ModelMetadata.FromStringExpression(name, htmlHelper.ViewData), htmlHelper.ViewContext).SelectMany(v => v.GetClientValidationRules());
            UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules, results);

            return results;
        }




        public static FormContext GetFormContextForClientValidation(HtmlHelper htmlHelper)
        {
            return (htmlHelper.ViewContext.ClientValidationEnabled) ? htmlHelper.ViewContext.FormContext : null;
        }


    }
}
