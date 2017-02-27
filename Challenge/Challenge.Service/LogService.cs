using System;
using System.Linq;
using System.Collections.Generic;
using Challenge.Data.Interfaces;
using Challenge.Model;
using Challenge.Service.Interfaces;

namespace Challenge.Service
{
    public class LogService : IService<Log>
    {
        private IRepository<Log> logRepository;
        
        public LogService(IRepository<Log> repository)
        {
            logRepository = repository;           
        }
        public IEnumerable<Log> GetAll()
        {
            return logRepository.GetAll;
        }
        public Log GetLog(int id)
        {
            //I not use find method because it was returning null and a didn't have the time to solve this
            return logRepository.GetAll.FirstOrDefault(l=>l.log_id==id);
        }
        public void Insert(Log log)
        {
            logRepository.Insert(log);
        }
        public void Update(Log log)
        {
            logRepository.Update(log);
        }
        public void Delete(Log log)
        {
            logRepository.Delete(log);
        }
    }
}
