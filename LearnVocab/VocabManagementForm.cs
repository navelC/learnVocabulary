using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LearnVocab
{
    public partial class VocabManagementForm : Form
    {
        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<Vocab> vocabs;
        public VocabManagementForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dataPath = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\data.json";
            vocabs = JsonConvert.DeserializeObject<List<Vocab>>(System.IO.File.ReadAllText(dataPath));
            foreach(var vocab in vocabs)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.AutoSize = true;
                checkbox.Location = new System.Drawing.Point(3, 3);
                checkbox.Name = vocab.English;
                checkbox.Size = new System.Drawing.Size(80, 17);
                checkbox.TabIndex = 1;
                checkbox.Text = vocab.English;
                checkbox.UseVisualStyleBackColor = true;
                checkBoxes.Add(checkbox);
                this.flowLayoutPanel1.Controls.Add(checkbox);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddVocabForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> english = new List<string>();
            foreach(var item in checkBoxes)
            {
                if (item.Checked == true)
                {
                    english.Add(item.Text);
                }
            }
            if(english.Count < 8)
            {
                MessageBox.Show("hãy chọn ít nhất 8 từ vựng để học");
                return;
            }
            var s = vocabs.Select(c => c).Where(c => english.Contains(c.English)).ToList();
            new learnForm(s).ShowDialog();
        }
    }
}
