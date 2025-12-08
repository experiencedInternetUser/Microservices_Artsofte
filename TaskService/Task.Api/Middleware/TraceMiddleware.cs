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

            // make TraceId available in response headers too for convenience
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(TraceHeader))
                {
                    context.Response.Headers.Add(TraceHeader, traceReader.GetValue());
                }
                return System.Threading.Tasks.Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
