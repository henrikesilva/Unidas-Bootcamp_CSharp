using System;
using System.IO;
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
            app.Use((context, next) =>
            {
                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
                });
            });
        

            string getTime()
            {
                return DateTime.Now.Millisecond.ToString();
            }
        }
    }
}
