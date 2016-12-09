using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Backend.WebApi.Helpers
{
    public class VersionCheckFilter : IActionFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false; // For some reason....?
            }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            KeyValuePair<string, IEnumerable<string>> headers = actionContext.Request.Headers.FirstOrDefault(x => x.Key == "X-Version");
            var response = new HttpResponseMessage((HttpStatusCode) 418);

            if (headers.Key != null)
            {
                if (headers.Value.FirstOrDefault()  == "42")
                {
                    // Set normal response from request chain.
                    response = await continuation();
                }
            }

            return response;
        }
    }
}