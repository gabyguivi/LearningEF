using Challenge.Model;
using Challenge.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Challenge.WebApi.Util
{
     
    public class SessionToken
    {
        public DateTime? startDateBlock { get; set; }
        public int amountRequestMinute { get;
            set;            
        }

        public DateTime? startDateOfPeriod { get; set; }
        public string token { get; set; }
        public bool isBlocked
        {
            get
            {
                if (startDateBlock.HasValue)
                {
                    double minutes = (DateTime.Now - startDateBlock.Value).TotalMinutes;
                    return (minutes <= double.Parse(ConfigurationManager.AppSettings["MinutesBlock"]));
                }
                return false;
            }
        }
        public bool isRateLimit
        {
            get
            {
                return (amountRequestMinute >= 60);                
            }
        }
        public void AddAmount()
        {
            double minutes = (DateTime.Now - startDateOfPeriod.Value).TotalMinutes;
            if (minutes > int.Parse(ConfigurationManager.AppSettings["MinutePeriod"]))
            {
                startDateOfPeriod = DateTime.Now;
                amountRequestMinute = 1;
            }
            else { amountRequestMinute = amountRequestMinute + 1; }
        } 
    }
}