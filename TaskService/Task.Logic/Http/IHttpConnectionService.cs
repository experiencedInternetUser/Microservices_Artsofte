using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Http
{
    public readonly record struct HttpConnectionData
    {
        public System.TimeSpan? Timeout { get; init; }
        public CancellationToken CancellationToken { get; init; }
        public string? ClientName { get; init; }
    }

    public interface IHttpConnectionService
    {
        HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);
        Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, HttpClient? client, CancellationToken cancellationToken);
    }
}
