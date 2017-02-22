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
    public class UserService : IService<User>
    {
        private IRepository<User> userRepository;
        
        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;           
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll.ToList();
        }

        public User GetUser(int id)
        {
            return userRepository.GetById(id);
        }

        public User GetUser(string user, string pass)
        {
            return userRepository.GetAll.FirstOrDefault(u => u.UserName == user && u.Password == pass);
        }
        public void Insert(User user)
        {
            userRepository.Insert(user);
        }

        public void Update(User user)
        {
            userRepository.Update(user);
        }

        public void Delete(User user)
        {
            userRepository.Delete(user);
        }
    }
}
