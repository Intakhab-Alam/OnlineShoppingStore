using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineShoppingStore.Startup))]
namespace OnlineShoppingStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
