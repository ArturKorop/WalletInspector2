using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WalletInspector2.WebUI.Startup))]
namespace WalletInspector2.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
