using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnVocab
{
    public partial class QuizzResultForm : Form
    {
        public QuizzResultForm(string res)
        {
            InitializeComponent();
            label1.Text = res;
        }
    }
}
