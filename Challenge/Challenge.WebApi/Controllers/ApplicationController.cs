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
using System.Text;
using System.Web.Http.Cors;

namespace Challenge.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:9448", headers: "*", methods: "*")]
    public class ApplicationController : ApiController
    {
        private IService<Application> service;
        private IService<SessionTime> serviceSessionTime;
        private IService<Log> serviceLog;
        private ApplicationService Service
        { get { return ((ApplicationService)service); } }

        private SessionTimeService ServiceSessionTime
        { get { return ((SessionTimeService)serviceSessionTime); } }
        private LogService ServiceLog
        { get { return ((LogService)serviceLog); } }

        public ApplicationController(IService<Application> service, 
            IService<SessionTime> serviceSessionTime, IService<Log> serviceLog)
        {
            this.service = service;
            this.serviceSessionTime = serviceSessionTime;
            if (Manager.sessiontime == 0)
            {
                Manager.sessiontime = ServiceSessionTime.GetAll().First().minutes;
            }
            this.serviceLog = serviceLog;
        }

        [Route("api/application/register")]
        [HttpPost]
        public JObject register(JObject displayname)
        {
            JToken display;
            JObject result;
            displayname.TryGetValue("display_name", out display);
            if (display == null || string.IsNullOrEmpty(display.ToString()) || display.ToString().Length > 25)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            //1. generate the strins to be returned to the client
            string app_id = display.ToString() + DateTime.Now.ToString("yyyMMdd");
            string secret = display.ToString() + DateTime.Now.ToString("HHmmss");
            Application app = Service.GetApplicationByDisplayName(display.ToString());
            if (app != null)
            {
                //Check if already exists on cache or is blocked
                if (Manager.ExistsApp(app.application_id))
                {
                    //if is blocked we have to reset the start block time
                    if (Manager.GetByApp(app.application_id).isBlocked)
                    {
                        Manager.RenewBlockTime(app.application_id);
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                    }
                    else
                    {
                        Manager.AddAmount(app.application_id);
                    }
                    result = new JObject();
                    result.Add("Error", JToken.FromObject("the application already exists"));
                    return result;
                }
                Manager.RegisterApp(app.application_id);//register in Memory cache the app
                result = new JObject();
                result.Add("Error", JToken.FromObject("the application already exists"));
                return result;
            }
            app = new Application()
            {
                application_id = CryptoHelper.EncryptText(app_id),
                secret = CryptoHelper.EncryptText(secret),
                display_name = display.ToString()
            };                        
            Service.Insert(app);
            Manager.RegisterApp(app.application_id);//register in Memory cache the app
            result = JObject.FromObject(app);
            result.Remove("secret");
            result.Remove("application_id");
            result.Add("application_secret", JToken.FromObject(secret));
            result.Add("application_id", JToken.FromObject(app_id));
            return result;
        }

        [Route("api/application/auth")]
        [HttpPost]
        [AppFilter]
        public JObject auth()
        {           
            string decodedInfo = Request.Headers.Authorization.Parameter;
            string app_id = decodedInfo.Split(':')[0];
            string secret = decodedInfo.Split(':')[1];
            //Check if app exists
            Application app = Service.GetApplication(CryptoHelper.EncryptText(app_id));
            if (app == null || app.secret != CryptoHelper.EncryptText(secret))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            string token = Manager.RegisterToken(app_id);            
            //Check if is in rate-limit because is not blocked
            if (Manager.GetByApp(app_id).isRateLimit)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            JObject result = new JObject();
            result.Add("access_token", JToken.FromObject(token));
            return result;
        }

        [Route("api/application/log")]
        [HttpPost]
        [TokenAuth]
        public JObject log(JObject message)
        {
            string decodedInfo = Request.Headers.Authorization.Parameter;
            //Get app_id from token                    
            string app_id = CryptoHelper.DecryptText(decodedInfo).Split('=')[0].Split(':')[0];                       
            
            Application app = Service.GetApplication(CryptoHelper.EncryptText(app_id));
            SessionToken sToken = Manager.GetByApp(app_id);
            //If is blocked the request is invalid
            if (sToken.isBlocked)
            {
                Manager.RenewBlockTime(app_id);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            Log log = message.ToObject<Log>();
            //check if the request is ok
            if (log.application_id == null || string.IsNullOrEmpty(log.application_id.ToString())
                || log.logger==null ||  string.IsNullOrEmpty(log.logger.ToString())
                ||log.message==null || string.IsNullOrEmpty(log.message.ToString())
                ||log.message==null || string.IsNullOrEmpty(log.message.ToString()))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            log.application_id = CryptoHelper.EncryptText(app_id);
            Manager.AddAmount(app_id);
            serviceLog.Insert(log);
            JObject result= new JObject();
            result.Add("success", JToken.FromObject((log.log_id>0).ToString().ToLower()));
            return result;
        }
    }
}
