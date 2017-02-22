using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Data.Interfaces;
using TestExam.Model;
using TestExam.Service.Interfaces;

namespace TestExam.Service
{
    public class TestExamService : IService<TestExam.Model.TestExam>
    {
        private IRepository<TestExam.Model.TestExam> testExamRepository;
        
        public TestExamService(IRepository<TestExam.Model.TestExam> repository)
        {
            this.testExamRepository = repository;           
        }

        public IEnumerable<TestExam.Model.TestExam> GetAll()
        {
            return testExamRepository.GetAll.ToList();
        }

        public TestExam.Model.TestExam GetTestExam(int id)
        {
            return testExamRepository.GetById(id);
        }

        public void Insert(TestExam.Model.TestExam test)
        {
            testExamRepository.Insert(test);
        }

        public void Update(TestExam.Model.TestExam test)
        {
            testExamRepository.Update(test);
        }

        public void Delete(TestExam.Model.TestExam test)
        {
            testExamRepository.Delete(test);
        }
    }
}
