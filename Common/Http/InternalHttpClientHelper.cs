using Common.Config;
using System;

namespace Common.Http
{
    public class InternalHttpClientHelper
    {
        public static TResult Get<TResult>(string requestUrl, params object[] parameters)
        {
            return HttpClientHelper.Get<TResult>(requestUrl, "Internal", CommonConfiguration.Config.InternalSecret,
                parameters);
        }

        public static TResult Post<TResult, TInput>(Uri requestUrl, TInput entity)
        {
            return HttpClientHelper.Post<TResult, TInput>(requestUrl, entity, "Internal",
                CommonConfiguration.Config.InternalSecret);
        }
    }

}
