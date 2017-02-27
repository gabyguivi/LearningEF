using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Challenge.Model;
using Challenge.Service;
using Challenge.Service.Interfaces;
using Challenge.WebApi.Util;
using Newtonsoft.Json.Linq;


namespace Challenge.WebApi.Controllers
{
    public class ApplicationController : ApiController
    {
        private IService<Application> service;
        private IService<SessionTime> serviceSessionTime;
        private ApplicationService Service
        { get { return ((ApplicationService)service); } }

        private SessionTimeService ServiceSessionTime
        { get { return ((SessionTimeService)serviceSessionTime); } }

        public ApplicationController(IService<Application> service,IService<SessionTime> serviceSessionTime)
        {
            this.service = service;
            this.serviceSessionTime = serviceSessionTime;
            if (Manager.sessiontime == 0)
            {
                Manager.sessiontime = ServiceSessionTime.GetAll().First().minutes;
            }
        }
        
        [Route("api/application/register")]
        [HttpPost]
        public JObject register(JObject displayname)
        {
            JToken display;
            displayname.TryGetValue("display_name", out display);
            if (display == null || string.IsNullOrEmpty(display.ToString()) || display.ToString().Length > 25)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            //1. generate the strins to be returned to the client
            string app_id = display.ToString() + DateTime.Now.ToString("yyyMMdd");
            string secret = display.ToString() + DateTime.Now.ToString("HHmmss");
            Application app = new Application()
            {
                application_id = CryptoHelper.EncryptText(app_id),
                secret = CryptoHelper.EncryptText(secret),
                display_name = display.ToString()
            };
            //Check if already exists or is blocked
            if (Manager.ExistsApp(app.application_id))
            {
                Manager.AddAmount(app.application_id);
                //if is blocked we have to reset the start block time
                if (Manager.GetByApp(app.application_id).isBlocked)
                {
                    Manager.RegisterBlockTime(app.application_id);
                }
                return JObject.Parse("{'Error':'the application already exists'}");
            }
            if (Service.GetApplication(CryptoHelper.EncryptText(app_id)) != null)
            {
                return JObject.Parse("{'Error':'the application already exists'}");
            }
            Service.Insert(app);
            Manager.RegisterApp(app.application_id);//register in Memory cache the app
            JObject result = JObject.FromObject(app);
            result.Remove("secret");
            result.Remove("application_id");
            result.Add("application_secret", JToken.FromObject(app_id));
            result.Add("application_id", JToken.FromObject(secret));
            return result;
        }      
    }
}
