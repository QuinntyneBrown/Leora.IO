using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Common.Config;

namespace Common.Attributes
{
    public class InternalAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public InternalAuthenticationAttribute()
        {

        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Internal")
            {
                request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //if header equals internal secret then authenticate

            var authorizationValue = authorization.Parameter;
            var internalSecret = CommonConfiguration.Config.InternalSecret;

            if (authorizationValue == internalSecret)
            {
                // create internal identity
            }

            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return true; }
        }

    }
}
