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
    public class QuestionService : IService<Question>
    {
        private IRepository<Question> questionRepository;
        
        public QuestionService(IRepository<Question> repository)
        {
            this.questionRepository = repository;           
        }

        public IEnumerable<Question> GetAll()
        {
            return questionRepository.GetAll.ToList();
        }

        public Question GetQuestion(int id)
        {
            return questionRepository.GetById(id);
        }

        public void Insert(Question question)
        {
            questionRepository.Insert(question);
        }

        public void Update(Question question)
        {
            questionRepository.Update(question);
        }

        public void Delete(Question question)
        {
            questionRepository.Delete(question);
        }
    }
}
