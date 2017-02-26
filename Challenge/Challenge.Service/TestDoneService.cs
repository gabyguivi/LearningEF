﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Data.Interfaces;
using Challenge.Model;
using Challenge.Service.Interfaces;

namespace Challenge.Service
{
    public class TestDoneService : IService<TestDone>
    {
        private IRepository<TestDone> testDoneRepository;
        
        public TestDoneService(IRepository<TestDone> repository)
        {
            this.testDoneRepository = repository;           
        }

        public IEnumerable<TestDone> GetAll()
        {
            return testDoneRepository.GetAll.ToList();
        }

        public TestDone GetTestDone(int id)
        {
            return testDoneRepository.GetById(id);
        }

        public void Insert(TestDone test)
        {
            testDoneRepository.Insert(test);
        }

        public void Update(TestDone test)
        {
            testDoneRepository.Update(test);
        }

        public void Delete(TestDone test)
        {
            testDoneRepository.Delete(test);
        }
    }
}