using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Web.HTTP.Session;
using PracticaMad.Model.ProductServiceNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PracticaMad.Model;
using PracticaMad.Model.ProductoDTO;

namespace PracticaMad.Web.Pages
{
    public partial class ShowProds : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblInvalidProdName.Visible = false;

            /* Get User Identifier passed as parameter in the request from
             * the previous page
             */
            string prodName = Request.Params.Get("search");

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
            ProductBlock accountBlock =
                prodService.FindProductByName(prodName, startIndex, count);

            if (accountBlock.Products.Count == 0)
            {
                lblInvalidProdName.Visible = true;
                return;
            }

            this.gvProducts.DataSource = accountBlock.Products;
            this.gvProducts.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "~/Pages/ShowProds.aspx" + "?search=" + prodName +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (accountBlock.ExistMoreProducts)
            {
                String url =
                    "~/Pages/ShowProds.aspx" + "?search=" + prodName +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
        
        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowIndex != -1)
            {
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService prodService = iocManager.Resolve<IProductService>();

                Console.WriteLine(e.Row.Cells[0].Text);
                ProductoDTO prod = prodService.Find(Convert.ToInt64(e.Row.Cells[0].Text));

                if (prodService.GetNumberOfCommentsByProdId(prod.prodId) == 0)
                {
                    e.Row.Cells[7].Visible = false;
                }
            }
        }
    }
}