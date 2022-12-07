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

namespace LearnVocab
{
    public partial class AddVocabForm : Form
    {
        public AddVocabForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( entxtbox.Text == "" || entxtbox.Text == "")
            {
                MessageBox.Show("phải điền đẩy đủ các trường");
                return;
            }
            string dataPath = "\\learnvocab\\LearnVocab\\Resources\\data.json";
            var obj = JsonConvert.DeserializeObject<List<Vocab>>(System.IO.File.ReadAllText(dataPath));
            var vocab = new Vocab
            {
                English = entxtbox.Text,
                Vietnamese = vntxtbox.Text,
            };
            if (obj == null)
            {
                List<Vocab> list = new List<Vocab>();
                list.Add(vocab);
                string json = JsonConvert.SerializeObject(list.ToArray());
                System.IO.File.WriteAllText("\\learnvocab\\LearnVocab\\Resources\\data.json", json);
            }
            else
            {
                obj.Add(vocab);
                string json = JsonConvert.SerializeObject(obj.ToArray());
                System.IO.File.WriteAllText("\\learnvocab\\LearnVocab\\Resources\\data.json", json);
            }
            MessageBox.Show("thêm thành công");
        }
    }
}
