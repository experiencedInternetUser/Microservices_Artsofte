using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Logic.Trace;

namespace Logic.Http
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpConnectionService _httpConnectionService;
        private readonly IEnumerable<ITraceWriter> _traceWriters;

        public HttpRequestService(IHttpConnectionService httpConnectionService, IEnumerable<ITraceWriter> traceWriters)
        {
            _httpConnectionService = httpConnectionService;
            _traceWriters = traceWriters;
        }

        public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default)
        {
            var client = _httpConnectionService.CreateHttpClient(connectionData);

            // build URI with query params
            var uriBuilder = new UriBuilder(requestData.Uri);
            if (requestData.QueryParameterList?.Any() == true)
            {
                var q = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var kv in requestData.QueryParameterList) q[kv.Key] = kv.Value;
                uriBuilder.Query = q.ToString()!;
            }

            var httpRequest = new HttpRequestMessage(requestData.Method, uriBuilder.Uri);

            // add trace headers
            foreach (var tw in _traceWriters)
            {
                var v = tw.GetValue();
                if (!string.IsNullOrEmpty(v) && !httpRequest.Headers.Contains(tw.Name))
                    httpRequest.Headers.Add(tw.Name, v);
            }

            // add custom headers
            if (requestData.HeaderDictionary != null)
            {
                foreach (var kv in requestData.HeaderDictionary)
                {
                    if (!httpRequest.Headers.Contains(kv.Key))
                        httpRequest.Headers.Add(kv.Key, kv.Value);
                }
            }

            // content
            if (requestData.Body != null && requestData.Method != HttpMethod.Get && requestData.Method != HttpMethod.Head)
            {
                httpRequest.Content = PrepareContent(requestData.Body, requestData.ContentType);
            }

            var responseMessage = await _httpConnectionService.SendRequestAsync(httpRequest, client, connectionData.CancellationToken);

            var response = new HttpResponse<TResponse>
            {
                StatusCode = responseMessage.StatusCode,
                RawResponse = responseMessage
            };

            if (responseMessage.Content != null)
            {
                var str = await responseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(str) && response.IsSuccessStatusCode)
                {
                    try
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var body = JsonSerializer.Deserialize<TResponse>(str, options);
                        response = response with { Body = body };
                    }
                    catch
                    {
                        response = response with { Body = default };
                    }
                }
            }

            return response;
        }

        private static HttpContent PrepareContent(object body, ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.ApplicationJson:
                    {
                        string serialized;
                        if (body is string s) serialized = s;
                        else serialized = JsonSerializer.Serialize(body);
                        return new StringContent(serialized, Encoding.UTF8, MediaTypeNames.Application.Json);
                    }
                case ContentType.XWwwFormUrlEncoded:
                    {
                        if (body is IEnumerable<KeyValuePair<string, string>> kvs) return new FormUrlEncodedContent(kvs);
                        throw new ArgumentException("Body must be IEnumerable<KeyValuePair<string,string>> for form content");
                    }
                case ContentType.Binary:
                    {
                        if (body is byte[] bytes) return new ByteArrayContent(bytes);
                        throw new ArgumentException("Body must be byte[] for binary content");
                    }
                default:
                    var str = body.ToString() ?? string.Empty;
                    return new StringContent(str, Encoding.UTF8, MediaTypeNames.Text.Plain);
            }
        }
    }
}
