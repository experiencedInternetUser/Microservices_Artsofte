using System.Threading.Tasks;
using Logic.Trace;
using Microsoft.AspNetCore.Http;

namespace Api.Middleware
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private const string TraceHeader = "TraceId";

        public TraceMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, ITraceReader traceReader)
        {
            if (context.Request.Headers.TryGetValue(TraceHeader, out var vals))
            {
                traceReader.WriteValue(vals.ToString());
            }
            else
            {
                traceReader.WriteValue(null);
            }

            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(TraceHeader))
                {
                    context.Response.Headers[TraceHeader] = traceReader.GetValue();
                }
                else
                {
                    context.Response.Headers[TraceHeader] = traceReader.GetValue();
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
