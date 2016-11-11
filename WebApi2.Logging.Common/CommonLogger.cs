using Common.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;

namespace WebApi2.Logging.Common
{
    public class CommonLogger : ITraceWriter
    {
        private static readonly ILog Logger = LogManager.GetLogger<CommonLogger>();

        /// <summary>
        /// Implementation of TraceWriter to trace the logs.
        /// </summary>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    category += " Action Parameters: " + traceAction.Target.ToString();
                }
                var record = new TraceRecord(request, category, level);
                traceAction?.Invoke(record);
                Log(record);
            }
        }

        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.AppendLine(record.Message);

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.AppendLine("Method: " + record.Request.Method);

                if (record.Request.RequestUri != null)
                    message.AppendLine("URL: " + record.Request.RequestUri);

                const string TokenKey = "Token";
                if (record.Request.Headers?.Contains(TokenKey) == true)
                {
                    string token = record.Request.Headers.GetValues(TokenKey)?.FirstOrDefault();
                    if (token != null)
                        message.AppendLine($"{TokenKey}: {token}");
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.AppendFormat(" {0} {1}", record.Operator, record.Operation);

            switch (record.Level)
            {
                case TraceLevel.Debug:
                    Logger.Debug(message.ToString(), record.Exception);
                    break;
                case TraceLevel.Info:
                    Logger.Info(message.ToString(), record.Exception);
                    break;
                case TraceLevel.Warn:
                    Logger.Warn(message.ToString(), record.Exception);
                    break;
                case TraceLevel.Error:
                    Logger.Error(message.ToString(), record.Exception);
                    break;
                case TraceLevel.Fatal:
                    Logger.Fatal(message.ToString(), record.Exception);
                    break;
                default:
                    Logger.Fatal("Unknown TraceLevel: " + record.Level.ToString());
                    Logger.Fatal(message.ToString(), record.Exception);
                    break;
            }
        }
    }
}
