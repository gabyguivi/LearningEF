using System;
using System.Linq;
using System.Collections.Generic;
using Challenge.Data.Interfaces;
using Challenge.Model;
using Challenge.Service.Interfaces;

namespace Challenge.Service
{
    public class SessionTimeService : IService<SessionTime>
    {
        private IRepository<SessionTime> sessionTimeRepository;
        
        public SessionTimeService(IRepository<SessionTime> repository)
        {
            this.sessionTimeRepository = repository;           
        }

        public IEnumerable<SessionTime> GetAll()
        {
            return sessionTimeRepository.GetAll;
        }

        public SessionTime GetSessionTime()
        {
            return sessionTimeRepository.GetAll.FirstOrDefault();
        }

        public void Insert(SessionTime sessiontime)
        {
            sessionTimeRepository.Insert(sessiontime);
        }

        public void Update(SessionTime sessiontime)
        {
            sessionTimeRepository.Update(sessiontime);
        }

        public void Delete(SessionTime sessiontime)
        {
            sessionTimeRepository.Delete(sessiontime);
        }
    }
}
