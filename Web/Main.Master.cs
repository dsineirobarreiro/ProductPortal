using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.TagServiceNS;
using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PracticaMad.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if(lnkProfile != null) lnkProfile.Visible = false;
                if(lnkLogOut != null) lnkLogOut.Visible = false;
            }
            else
            {
                if (lnkLogin != null) lnkLogin.Visible = false;
                if (lnkProfile != null) lnkProfile.Visible = true;
                if (lnkLogOut != null) lnkLogOut.Visible = true;
            }

            int startIndex, count;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("tagIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("taCount"));
            }
            catch (ArgumentNullException)
            {
                count = 5; //Meter aqui default
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagService tagService = iocManager.Resolve<ITagService>();

            /* Get Accounts Info */
            TagBlock tagBlock =
                tagService.GetTagsByBlock(startIndex, count);

            string prevUrl = "?";
            string nextUrl = "?";
            foreach(var param in DecodeQueryParameters(Request.Url))
            {
                if (!param.Key.Equals("tagIndex") && !param.Key.Equals("tagCount"))
                {
                    prevUrl += param.Key + "=" + param.Value + "&";
                    nextUrl += param.Key + "=" + param.Value + "&";
                }
            }

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                prevUrl +=
                    "tagIndex=" + (startIndex - count) + "&tagCount=" +
                    count;

                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.InnerText = "<<";
                a.Attributes.Add("href", prevUrl);
                li.Controls.Add(a);
                tagList.Controls.Add(li);

            }

            foreach (var tag in tagBlock.Tags)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.InnerText = tag.nombre;
                a.Attributes.Add("href", "ShowCommentsByTag.aspx?tag=" + tag.nombre);
                li.Controls.Add(a);
                tagList.Controls.Add(li);
            }

            /* "Next" link */
            if (tagBlock.ExistMoreTags)
            {

                nextUrl +=
                    "tagIndex=" + (startIndex + count) + "&tagCount=" +
                    count;

                HtmlGenericControl li = new HtmlGenericControl("li");
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.InnerText = ">>";
                a.Attributes.Add("href", nextUrl);
                li.Controls.Add(a);
                tagList.Controls.Add(li);
            }
        }

        private static Dictionary<string, string> DecodeQueryParameters(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (uri.Query.Length == 0)
                return new Dictionary<string, string>();

            return uri.Query.TrimStart('?')
                            .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                            .GroupBy(parts => parts[0],
                                     parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                            .ToDictionary(grouping => grouping.Key,
                                          grouping => string.Join(",", grouping));
        }
    }
}