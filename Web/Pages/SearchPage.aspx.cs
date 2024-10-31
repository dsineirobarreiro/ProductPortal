using PracticaMad.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaMad.Web.Pages
{
    public partial class SearchPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                string prodName = searchText.Text;

                String url =
                    String.Format("./ShowProds.aspx?search={0}", prodName);

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}