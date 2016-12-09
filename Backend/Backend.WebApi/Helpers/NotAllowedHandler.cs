using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Backend.WebApi.Helpers
{
    public class NotAllowedHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.ExceptionContext.Exception.GetType() == typeof(NotAllowedException))
                context.Result = new StatusCodeResult(HttpStatusCode.Forbidden, context.Request);

            return Task.FromResult(0);
        }
    }
}