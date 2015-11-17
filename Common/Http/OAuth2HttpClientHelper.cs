using System;
namespace Common.Http
{
    public class OAuth2HttpClientHelper
    {
        public static TResult Get<TResult>(string requestUrl, string bearerToken, params object[] parameters)
        {
            return HttpClientHelper.Get<TResult>(requestUrl, "Bearer", bearerToken, parameters);
        }

        public static TResult Post<TResult, TInput>(Uri requestUrl, TInput entity, string bearerToken)
        {
            return HttpClientHelper.Post<TResult, TInput>(requestUrl, entity, "Bearer",
                bearerToken);
        }
    }
}
