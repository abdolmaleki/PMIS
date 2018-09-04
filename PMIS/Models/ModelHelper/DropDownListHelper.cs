using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PMIS.Models.ModelHelper
{
    public static class MyHTMLHelper
    {
        public static MvcHtmlString BootstrapDropDownFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IList<SelectListItem> selectList)
        {
            MvcHtmlString label = LabelExtensions.LabelFor(helper, expression, new { @class = "control-label col-md-2" });
            MvcHtmlString select = SelectExtensions.DropDownListFor(helper, expression, selectList, new { @id = "dropdown" });
            MvcHtmlString validation = ValidationExtensions.ValidationMessageFor(helper, expression, null, new { @class = "text-danger" });
            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(select);
            innerHtml.Append(validation);
            TagBuilder innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml = innerHtml.ToString();
            StringBuilder outerHtml = new StringBuilder();
            outerHtml.Append(label);
            outerHtml.Append(innerDiv.ToString());
            TagBuilder outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass("form-group");
            outerDiv.InnerHtml = outerHtml.ToString();
            return MvcHtmlString.Create(outerDiv.ToString());
        }
    }
}
