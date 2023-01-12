using LearnVocab.DAL;
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
            loadCategory();
        }
        private void loadCategory()
        {
            var cate = DataProvider.getInstance().category;
            comboBox2.DataSource = cate;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( entxtbox.Text == "" || vntxtbox.Text == "")
            {
                MessageBox.Show("phải điền đẩy đủ các trường");
                return;
            }
            ESub ESubs = new ESub();
            ESubs.PartOfSpeech = comboBox1.Text;
            var vocab = new Vocab
            {
                English = entxtbox.Text,
                Vietnamese = vntxtbox.Text,
                ESubs = ESubs,
                Category = comboBox2.Text,

            };
            if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.True)
                MessageBox.Show("lưu thành công");
            else if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.Exist)
                MessageBox.Show("từ vựng đã tồn tại");
            else if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.False)
                MessageBox.Show("lưu thất bại");
        }
    }
}
