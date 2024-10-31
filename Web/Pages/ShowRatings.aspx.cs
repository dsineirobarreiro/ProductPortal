using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Model;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.UserDTO;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.ValoracionServiceNS;
using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaMad.Web.Pages
{
    public partial class ShowRatings : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get User Identifier passed as parameter in the request from
             * the previous page
             */
            string username = Request.Params.Get("username");
            UserName.Text = username;
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
            IValoracionService valService = iocManager.Resolve<IValoracionService>();
            IUserService userService = iocManager.Resolve<IUserService>();

            UserDTO user = userService.FindByUsername(username);

            lblTotal.Text += user.numValoraciones.ToString();
            lblAverage.Text += user.score.ToString();

            /* Get Accounts Info */
            ValoracionBlock valBlock =
                valService.FindValoracionByVendor(user.userID, startIndex, count);

            this.gvProducts.DataSource = valBlock.Valoraciones;
            this.gvProducts.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "~/Pages/ShowRatings.aspx" + "?username=" + username +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (valBlock.ExistMoreValoraciones)
            {
                String url =
                    "~/Pages/ShowRatings.aspx" + "?username=" + username +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}