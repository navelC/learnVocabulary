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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new DefinationForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new VocabManagementForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new TestForm().ShowDialog();
        }
    }
}
