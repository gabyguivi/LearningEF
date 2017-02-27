using Challenge.Model;
using Challenge.Service;
using Challenge.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Challenge.WebApi.Util
{
    public class AuthAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private IService<Application> service;
        private ApplicationService Service
        { get { return ((ApplicationService)service); } }
        
        public AuthAttribute(IService<Application> service)
        {
            this.service = service;            
        }
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authInfo = actionContext.Request.Headers.Authorization.Parameter;
                string decodedInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo));
                string app_id = decodedInfo.Split(':')[0];
                string app_secret = decodedInfo.Split(':')[1];
                if (string.IsNullOrEmpty(app_id) || string.IsNullOrEmpty(app_secret))
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    Application app = Service.GetApplication(app_id);
                    if (app == null)
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        if (app_secret != CryptoHelper.DecryptText(app.secret))
                        {
                            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                        }
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
    public class TokenAuthAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}