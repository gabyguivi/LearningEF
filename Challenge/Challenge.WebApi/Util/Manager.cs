using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Challenge.WebApi.Util
{
    public class Manager
    {
        public static int sessiontime { get; set; }
        public static MemoryCache CacheApps = MemoryCache.Default;
        public static void RegisterApp(string app_id)
        {
            SessionToken s = new SessionToken();
            s.amountRequestMinute = 1;
            s.startDateBlock = null;
            s.startDateOfPeriod = DateTime.Now;
            CacheApps.Add(app_id, s, new DateTimeOffset(DateTime.Now.AddMinutes(sessiontime)));
        }
        /// <summary>
        /// returns if an app is in the cache or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ExistsApp(string key)
        {
            return CacheApps.GetCacheItem(key) != null;
        }

        /// <summary>
        /// returns if an app is in the cache or not with token and key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ExistsApp(string key, string token)
        {
            SessionToken sToken = (SessionToken)CacheApps.GetCacheItem(key).Value;
            return (sToken != null && sToken.token == token);
        }

        /// <summary>
        /// Generates and returns the token for the app sended as parameter
        /// </summary>
        /// <param name="app_id"></param>
        /// <returns></returns>
        public static string RegisterToken(string app_id)
        {
            string token = CryptoHelper.EncryptText(app_id + "=" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            //if is null we have to make it again
            if (!ExistsApp(app_id))
            {
                RegisterApp(app_id);
            }
            SessionToken sToken = GetByApp(app_id);
            sToken.token = token;
            sToken.AddAmount();
            CacheApps.Remove(app_id);
            CacheApps.Add(app_id, sToken, new DateTimeOffset(DateTime.Now.AddMinutes(sessiontime)));//renew the session time
            return token;
        }
        /// <summary>
        /// add 1 to the amount of request made in the period by the application
        /// </summary>
        /// <param name="app_id"></param>        
        public static void AddAmount(string app_id)
        {
            SessionToken sToken = GetByApp(app_id);
            sToken.AddAmount();
            if (sToken.isRateLimit)
            {
                sToken.amountRequestMinute = 1;
                sToken.startDateBlock = DateTime.Now;
            }
            CacheApps.Remove(app_id);
            CacheApps.Add(app_id, sToken, new DateTimeOffset(DateTime.Now.AddMinutes(sessiontime)));//renew the session time            

        }

        public static SessionToken GetByToken(string token)
        {
            string tokenReal = CryptoHelper.DecryptText(token);
            string app_id = tokenReal.Split('=')[0];
            return GetByApp(app_id);
        }
        public static SessionToken GetByApp(string app_id)
        {
            return ((SessionToken)CacheApps.GetCacheItem(app_id).Value);
        }

        public static void RenewBlockTime(string app_id)
        {
            SessionToken sToken = GetByApp(app_id);
            sToken.amountRequestMinute = 1;
            sToken.startDateBlock = DateTime.Now;
            CacheApps.Remove(app_id);
            CacheApps.Add(app_id, sToken, new DateTimeOffset(DateTime.Now.AddMinutes(sessiontime)));//renew the session time            
        }
    }
}