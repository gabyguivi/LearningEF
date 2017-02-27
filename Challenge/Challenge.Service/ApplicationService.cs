using System;
using System.Linq;
using System.Collections.Generic;
using Challenge.Data.Interfaces;
using Challenge.Model;
using Challenge.Service.Interfaces;

namespace Challenge.Service
{
    public class ApplicationService : IService<Application>
    {
        private IRepository<Application> applicationRepository;
        
        public ApplicationService(IRepository<Application> repository)
        {
            this.applicationRepository = repository;           
        }

        public IEnumerable<Application> GetAll()
        {
            return applicationRepository.GetAll;
        }

        public Application GetApplication(string id)
        {
            
            //I not use find method because it was returning null and a didn't have the time to solve this
            return applicationRepository.GetAll.FirstOrDefault(a=>a.application_id==id);
        }

        public void Insert(Application application)
        {
            applicationRepository.Insert(application);
        }

        public void Update(Application application)
        {
            applicationRepository.Update(application);
        }

        public void Delete(Application application)
        {
            applicationRepository.Delete(application);
        }
    }
}
