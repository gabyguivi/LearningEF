using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Data.Interfaces;
using Challenge.Model;
using Challenge.Service.Interfaces;

namespace Challenge.Service
{
    public class TestExamService : IService<Challenge.Model.TestExam>
    {
        private IRepository<Challenge.Model.TestExam> testExamRepository;
        
        public TestExamService(IRepository<Challenge.Model.TestExam> repository)
        {
            this.testExamRepository = repository;           
        }

        public IEnumerable<Challenge.Model.TestExam> GetAll()
        {
            return testExamRepository.GetAll;
        }

        public Challenge.Model.TestExam GetTestExam(int id)
        {
            return testExamRepository.GetById(id);
        }

        public void Insert(Challenge.Model.TestExam test)
        {
            testExamRepository.Insert(test);
        }

        public void Update(Challenge.Model.TestExam test)
        {
            testExamRepository.Update(test);
        }

        public void Delete(Challenge.Model.TestExam test)
        {
            testExamRepository.Delete(test);
        }
    }
}
