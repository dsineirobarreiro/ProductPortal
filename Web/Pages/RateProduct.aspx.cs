using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.HTTP.Session;
using PracticaMad.Model.ProductoDTO;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.UserDTO;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.ValoracionServiceNS;
using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaMad.Web.Pages
{
    public partial class RateProduct : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName.Text = Request.Params.Get("username");
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                String text = txtComment.Text;
                Double rating = Convert.ToDouble(RatingList.SelectedValue);

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IValoracionService valService = iocManager.Resolve<IValoracionService>();


                valService.AddValoracion(SessionManager.GetUserSession(Context).UserProfileId, Convert.ToInt64(Request.Params.Get("id")), rating, text);
            }
        }
    }
}