using PhoneBookNamespace.DAL.DbInitialization;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PhoneBookNamespace
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<PhonebookContext>(new DatabaseInitialization());
            var context = new PhonebookContext();
            context.Database.Initialize(true);

            Database.SetInitializer<PhonebookContext>(new DatabaseInitialization());

            DatabaseInitialization InitDatabase = new DatabaseInitialization();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
