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
    public class AnswerService : IService<Answer>
    {
        private IRepository<Answer> answerRepository;
        
        public AnswerService(IRepository<Answer> repository)
        {
            this.answerRepository = repository;           
        }

        public IEnumerable<Answer> GetAll()
        {
            return answerRepository.GetAll.ToList();
        }

        public Answer GetAnswer(int id)
        {
            return answerRepository.GetById(id);
        }

        public void Insert(Answer answer)
        {
            answerRepository.Insert(answer);
        }

        public void Update(Answer answer)
        {
            answerRepository.Update(answer);
        }

        public void Delete(Answer answer)
        {
            answerRepository.Delete(answer);
        }
    }
}
