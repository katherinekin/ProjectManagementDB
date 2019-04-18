using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using DataLibrary.BusinessLogic;

namespace ProjManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // look if any security information exists for this request

            if (HttpContext.Current.User != null)
            {

                // see if this user is authenticated, any authenticated cookie (ticket) exists for this user

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    // see if the authentication is done using FormsAuthentication

                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {

                        // Get the roles stored for this request from the ticket

                        // get the identity of the user

                        FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                        //Get the form authentication ticket of the user

                        FormsAuthenticationTicket ticket = identity.Ticket;

                        //Get the roles stored as UserData into ticket

                        string[] roles;

                        if (IsManager(int.Parse(HttpContext.Current.User.Identity.Name)))
                        {
                            roles = new string[1]{"Manager"};
                        }
                        else
                        {
                            roles = new string[1]{"Employee"};
                        }

                        //Create general prrincipal and assign it to current request

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                    }
                }
            }
        }

        public bool IsManager(int id)
        {
            var managerList = LoginProcessor.LoadManagers();

            foreach (int managerID in managerList)
            {
                if (id == managerID)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
