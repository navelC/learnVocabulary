﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab
{
    public class Category
    {
        public string Name { get; set; }
        public Category(string name)
        {
            Name = name;
        }
    }
}
