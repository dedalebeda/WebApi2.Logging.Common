# WebApi2.Logging.Common
Common.Logging implementation for WebApi2 tracing

## Using
Configure the underlying Commong.Logging Factory Adapter as usual.

Then to utilize Common.Logging in Owin, use extension methods:

```C#
using System.Web.Http;
using WebApi2.Logging.Common;

    public class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.UseCommonLogging();
        }
    }
```