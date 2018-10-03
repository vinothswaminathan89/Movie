using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace Movie.Helper
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            message.Content = new StringContent(String.Format("Error : {0}", actionExecutedContext.Exception.Message));
            actionExecutedContext.Response = message;
        }
    }
}