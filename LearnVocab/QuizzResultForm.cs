using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace LearnVocab
{
    public partial class QuizzResultForm : Form
    {
        public QuizzResultForm(List<string> input, List<Vocab> vocab, string res)
        {
            InitializeComponent();
            label1.Text = "số câu đúng của bạn là: "+res;
        }

    }
}
