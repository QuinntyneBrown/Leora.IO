using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Common.Http
{
    public class HttpClientHelper
    {
        public static HttpClient CreateClient(string authorizationScheme = null, string authorizationValue = null)
        {
            var httpClient = new HttpClient();

            if (authorizationScheme != null && authorizationValue != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationValue);
            }

            return httpClient;
        }

        public static TResult Post<TResult, TInput>(Uri requestUrl, TInput entity, string authorizationScheme = null, string authorizationValue = null)
        {
            return PostAsync<TResult, TInput>(requestUrl, entity, authorizationScheme, authorizationValue).Result;
        }

        public static TResult Get<TResult>(string requestUrl, string authorizationScheme = null, string authorizationValue = null, params object[] parameters)
        {
            var absoluteUrl = GetAbsoluteUrl(requestUrl, false, parameters);

            return SendRequest<TResult>(new Uri(absoluteUrl),authorizationScheme,authorizationValue);
        }

        public static Task<TResult> PostAsync<TResult, TInput>(Uri requestUrl, TInput entity, string authorizationScheme = null, string authorizationValue = null)
        {
            return SendRequestAsync<TResult, TInput>(requestUrl, HttpMethod.Post, entity,authorizationScheme,authorizationValue);
        }

        public static Task<TResult> GetAsync<TResult>(string requestUrl, params object[] parameters)
        {
            var absoluteUrl = GetAbsoluteUrl(requestUrl, false, parameters);

            return SendRequestAsync<TResult>(new Uri(absoluteUrl));
        }

        private static string GetAbsoluteUrl(string requestUrl, bool urlEncodeParams, params object[] parameters)
        {
            string absoluteRequestUrl = requestUrl;

            if (urlEncodeParams)
            {
                var ps = parameters.Select(p => ((p != null) && p is string ? HttpUtility.UrlEncode(p as string) : p)).ToArray();

                return string.Format(absoluteRequestUrl, ps);
            }
            else
                return string.Format(absoluteRequestUrl, parameters);
        }

        public static TResult SendRequest<TResult, TInput>(Uri requestUrl, HttpMethod method, TInput entity = default(TInput), string authorizationScheme = null, string authorizationValue = null)
        {
            HttpResponseMessage responseMessage = null;
            var httpClient = CreateClient(authorizationScheme, authorizationValue);
            if (method == HttpMethod.Post)
            {
                responseMessage = httpClient.PostAsJsonAsync(requestUrl.ToString(), entity).Result;
            }
            else
            {
                responseMessage = httpClient.GetAsync(requestUrl).Result;
            }
            ProcessResponseException(responseMessage);
            return responseMessage.Content.ReadAsAsync<TResult>().Result;
        }

        public static Task<TResult> SendRequestAsync<TResult, TInput>(Uri requestUrl, HttpMethod method, TInput entity = default(TInput), string authorizationScheme = null, string authorizationValue = null)
        {
            HttpResponseMessage responseMessage = null;
            HttpClient client = CreateClient();
            if (method == HttpMethod.Post)
            {
                responseMessage = client.PostAsJsonAsync(requestUrl.ToString(), entity).Result;
            }
            else
            {
                responseMessage = client.GetAsync(requestUrl).Result;
            }
            ProcessResponseException(responseMessage);
            return responseMessage.Content.ReadAsAsync<TResult>();
        }

        public static TResult SendRequest<TResult>(Uri requestUrl, string authorizationScheme = null, string authorizationValue = null)
        {            
            return SendRequestAsync<TResult>(requestUrl,authorizationScheme,authorizationValue).Result;
        }

        public static Task<TResult> SendRequestAsync<TResult>(Uri requestUrl, string authorizationScheme = null, string authorizationValue = null)
        {
            HttpResponseMessage responseMessage = null;

            HttpClient client = CreateClient(authorizationScheme,authorizationValue);

            responseMessage = client.GetAsync(requestUrl).Result;

            ProcessResponseException(responseMessage);

            return responseMessage.Content.ReadAsAsync<TResult>();
        }
            

        public static void ProcessResponseException(HttpResponseMessage httpResponseMessage)
        {
            
        }
    }
}
