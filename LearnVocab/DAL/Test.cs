using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab.DAL
{
    public class Test
    {
        public DateTime InitTime;
        public DateTime TestTime;
        public int ID;
        public int numberOfTest;
        public Test(int id)
        {
            ID = id;
            numberOfTest = 0;
            InitTime = DateTime.Now;
        }
    }
   
}
