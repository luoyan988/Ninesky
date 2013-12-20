////////////////////
//Easyui控件Mvc扩展
//版本v1
//创建日期2013-09-28
////////////////////
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// EasyUi控件//计划删除 使用后台json传递服务器验证
    /// </summary>
    public static class EasyuiExtensions
    {
        public static MvcHtmlString EasyuiInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string type = "input")
        {
            return htmlHelper.EasyuiInput(expression, null,type:type);
        }
        public static MvcHtmlString EasyuiInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string type = "input")
        {
            return htmlHelper.EasyuiInput(expression, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),type:type);
        }

        public static MvcHtmlString EasyuiInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, string type = "input")
        {
            ModelMetadata _metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string _name = ExpressionHelper.GetExpressionText(expression);
            string _fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(_name);
            string _htmlCtrlstr = string.Empty;//控件Html字符
            if (String.IsNullOrEmpty(_fullName))
            {
                throw new ArgumentException(_name + " 字段不存在！", "name");
            }
            TagBuilder tagBuilder = new TagBuilder(type);
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", _fullName, true);
            tagBuilder.MergeAttribute("id", _fullName, true);
            //值
            if (_metadata.Model != null)
            {
                if (type.ToLower() == "input") tagBuilder.MergeAttribute("value", _metadata.Model.ToString());
                else if (type.ToLower() == "textarea") tagBuilder.InnerHtml = _metadata.Model.ToString();
                
            }
            ///验证部分代码开始
            Dictionary<string, object> _results = new Dictionary<string, object>();
            string _validType = string.Empty;
            if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                IEnumerable<ModelClientValidationRule> _clientRules = ModelValidatorProviders.Providers.GetValidators(_metadata ?? ModelMetadata.FromStringExpression(_name, htmlHelper.ViewData), htmlHelper.ViewContext).SelectMany(v => v.GetClientValidationRules());
                if (_clientRules.Count() > 0)
                {
                    _validType = string.Empty;
                    foreach (ModelClientValidationRule rule in _clientRules)
                    {
                        switch (rule.ValidationType)
                        {
                            case "required":
                                break;
                            case "length":
                                if (!string.IsNullOrEmpty(_validType)) _validType += ",";
                                if (rule.ValidationParameters.ContainsKey("min")) _validType += "'" + rule.ValidationType + "[" + rule.ValidationParameters["min"].ToString() + "," + rule.ValidationParameters["max"].ToString() + "]'";
                                else _validType += "'" + rule.ValidationType + "[0," + rule.ValidationParameters["max"].ToString() + "]'";
                                break;
                            default:
                                if (!string.IsNullOrEmpty(_validType)) _validType += ",";
                                _validType += "'" + rule.ValidationType + "'";
                                break;
                        }
                    }
                    if(!string.IsNullOrEmpty(_validType)) _validType = "validType:["+_validType+"]";
                    if (_metadata.IsRequired)
                    {
                        if (string.IsNullOrEmpty(_validType)) _validType = "required:true";
                        else _validType = "required:true," + _validType;
                    }
                    if (!string.IsNullOrEmpty(_validType)) { tagBuilder.MergeAttribute("data-options", _validType); tagBuilder.AddCssClass("easyui-validatebox"); }
                }
            }
            ///验证部分代码结束
            if (type.ToLower() == "input") _htmlCtrlstr = tagBuilder.ToString((TagRenderMode.SelfClosing));
            else if (type.ToLower() == "textarea") _htmlCtrlstr = tagBuilder.ToString();
            return new MvcHtmlString(_htmlCtrlstr);
        }
    }
}