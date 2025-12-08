using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;

namespace Logic.Http
{
    internal class HttpConnectionService : IHttpConnectionService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpConnectionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
        {
            var client = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(httpConnectionData.ClientName);

            if (httpConnectionData.Timeout.HasValue)
            {
                client.Timeout = httpConnectionData.Timeout.Value;
            }

            return client;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, HttpClient? client, CancellationToken cancellationToken)
        {
            if (client == null) client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            return response;
        }
    }
}
