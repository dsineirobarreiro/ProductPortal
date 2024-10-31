using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Model;
using PracticaMad.Model.ProductoDTO;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.UserDTO;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PracticaMad.Web.Pages
{
    public partial class CommentProduct : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProdName.Text = Request.Params.Get("prodName");
            UserName.Text = Request.Params.Get("username");
        }

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                String text = txtComment.Text;

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService prodService = iocManager.Resolve<IProductService>();
                ProductoDTO prod = prodService.Find(Convert.ToInt64(Request.Params.Get("id")));
                char[] separators = { ';', ',', ' ' };
                String[] tags = (TagText.Text).Split(separators, StringSplitOptions.RemoveEmptyEntries);
                HashSet<String> final_tags = new HashSet<string>();
                foreach (String tag in tags) {
                    final_tags.Add(tag);
                }
                prodService.CommentProduct(SessionManager.GetUserSession(Context).UserProfileId, prod.prodId, text, final_tags);
            }
        }
    }
}