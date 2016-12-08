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
    public class NotFondHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.ExceptionContext.Exception.GetType() == typeof(NotFoundException))
                context.Result = new StatusCodeResult(HttpStatusCode.NotFound, context.Request);

            return Task.FromResult(0); // Googled it.... havent the faintest what it means... Should dive deeper into async/await/Task at some point...
        }
    }
}