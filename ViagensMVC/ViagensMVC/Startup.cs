using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ViagensMVC.Startup))]

namespace ViagensMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
