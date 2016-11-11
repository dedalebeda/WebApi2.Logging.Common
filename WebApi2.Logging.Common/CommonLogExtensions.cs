using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApi2.Logging.Common
{
    public static class CommonLogFactoryExtensions
    {
        public static void UseCommonLogging(this HttpConfiguration config)
        {
            config.Services.Replace(typeof(ITraceWriter), new CommonLogger());
            config.Filters.Add(new LoggingFilterAttribute());
        }
    }
}
