using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Chilindo.CustomerAPI.Handlers
{
    public class APIKeyHandler : DelegatingHandler
    {

        private const string REQUEST_HEADER_API_KEY = "X-WEBAPI-KEY";
        private const string REQUEST_HEADER_API_KEY_VALUE = "SPA";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isValidKey = false;
            IEnumerable<string> APIKeyValue;
            if (request.Headers.TryGetValues(REQUEST_HEADER_API_KEY, out APIKeyValue))
            {
                if (APIKeyValue.FirstOrDefault().Equals(REQUEST_HEADER_API_KEY_VALUE))
                {
                    isValidKey = true;
                }
                else
                {
                    isValidKey = false;
                }
            }

            if (isValidKey)
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                var res = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(res);
                return tsc.Task;
            }

        }
    }
}