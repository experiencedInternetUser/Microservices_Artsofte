using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using User.Api.Trace;

namespace User.Api.Middleware
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private const string TraceHeader = "TraceId";

        public TraceMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, ITraceReader traceReader)
        {
            if (context.Request.Headers.TryGetValue(TraceHeader, out var v))
            {
                traceReader.WriteValue(v.ToString());
            }
            else
            {
                traceReader.WriteValue(null);
            }

            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(TraceHeader))
                {
                    context.Response.Headers.Add(TraceHeader, traceReader.GetValue());
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
