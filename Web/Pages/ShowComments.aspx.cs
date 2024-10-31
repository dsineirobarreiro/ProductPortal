using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaMad.Web.Pages
{
    public partial class ShowComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get User Identifier passed as parameter in the request from
             * the previous page
             */
            string prodName = Request.Params.Get("prodName");
            long prodId = Convert.ToInt64(Request.Params.Get("id"));

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 5; //Meter aqui default
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService prodService = iocManager.Resolve<IProductService>();

            /* Get Accounts Info */
            CommentBlock comBlock =
                prodService.SeeProductComments(prodId, startIndex, count);

            this.gvProducts.DataSource = comBlock.Comentarios;
            this.gvProducts.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "~/Pages/ShowComments.aspx" + "?id=" + prodId + "&prodName=" + prodName +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (comBlock.ExistMoreProducts)
            {
                String url =
                    "~/Pages/ShowComments.aspx" + "?id=" + prodId + "&prodName=" + prodName +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}