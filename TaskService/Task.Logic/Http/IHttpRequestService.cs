using System.Net.Http;
using System.Threading.Tasks;

namespace Logic.Http
{
    public enum ContentType
    {
        Unknown = 0,
        ApplicationJson = 1,
        XWwwFormUrlEncoded = 2,
        Binary = 3,
        ApplicationXml = 4,
        MultipartFormData = 5,
        TextPlain = 6
    }

    public record HttpRequestData
    {
        public HttpMethod Method { get; init; } = HttpMethod.Get;
        public System.Uri Uri { get; init; } = null!;
        public object? Body { get; init; }
        public ContentType ContentType { get; init; } = ContentType.ApplicationJson;
        public System.Collections.Generic.IDictionary<string, string> HeaderDictionary { get; init; } = new System.Collections.Generic.Dictionary<string, string>();
        public System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, string>> QueryParameterList { get; init; } = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
    }

    public record BaseHttpResponse
    {
        public System.Net.HttpStatusCode StatusCode { get; init; }
        public HttpResponseMessage? RawResponse { get; init; }
        public bool IsSuccessStatusCode => ((int)StatusCode >= 200 && (int)StatusCode <= 299);
    }

    public record HttpResponse<TResponse> : BaseHttpResponse
    {
        public TResponse? Body { get; init; }
    }

    public interface IHttpRequestService
    {
        Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
    }
}
