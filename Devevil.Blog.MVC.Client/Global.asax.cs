using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Devevil.Blog.MVC.Client.Controllers;
using Devevil.Blog.Nhibernate.DAL;

namespace Devevil.Blog.MVC.Client
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            try
            {
                //configura Nhibernate
                SessionManager.Instance.Configure();
                //Confgurazione log4net
                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception ex)
            {
                //Log errore?

            }
        }

        protected void Application_Error()
        {
            //RouteData routeData = new RouteData();
            //routeData.Values.Add("controller", "Error");
            //routeData.Values.Add("action", "Index");

            //IController controller = new ErrorController();
            //controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}