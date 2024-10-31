using Ninject;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.ProductoDAO;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.UserDAO;
using PracticaMad.Model.ValoracionDAO;
using PracticaMad.Model.ValoracionServiceNS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.IoC;
using PracticaMad.Web.HTTP.Util.IoC;
using PracticaMad.Web.HTTP.Session;

namespace PracticaMad.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager IoCManager = new IoCManagerNinject();
            IoCManager.Configure();

            Application["managerIoC"] = IoCManager;

            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
 
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ((IKernel)Application["kernelIoC"]).Dispose();
        }
    }
}