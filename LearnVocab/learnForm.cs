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
    public partial class learnForm : Form
    {
        List<Vocab> vocabs;
        public learnForm(List<Vocab> vc)
        {
            InitializeComponent();
            vocabs = vc;
            foreach (Vocab v in vocabs)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.Text = v.English + ": " + v.Vietnamese;
                this.flowLayoutPanel1.Controls.Add(label);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new QuizzForm(vocabs).ShowDialog();
        }
    }
}
