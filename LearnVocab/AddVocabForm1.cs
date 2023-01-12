using Newtonsoft.Json;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LearnVocab.DAL;
using System.Collections;

namespace LearnVocab
{
    public partial class AddVocabForm1 : Form
    {
        Vocab vocab;
        Dictionary<string, MediaFoundationReader> dic;
        public AddVocabForm1(Vocab vocab, Dictionary<string, MediaFoundationReader> dic)
        {
            InitializeComponent();
            this.vocab = vocab;
            this.dic = dic;
            loadCategory();   
        }
        private void loadCategory()
        {
            var cate = DataProvider.getInstance().category;
            comboBox1.DataSource = cate;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Path = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\";
            foreach (KeyValuePair<string, MediaFoundationReader> entry in dic)
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(entry.Value))
                {
                    WaveFileWriter.CreateWaveFile($"{Path}{vocab.English}-{entry.Key}.mp3", pcm);
                    vocab.Phonetics.Where(c => c.Region == entry.Key).Single().Audio = $"{Path}{vocab.English}-{entry.Key}.mp3";
                }
            }
            vocab.Category = comboBox1.Text;
            if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.True)
                MessageBox.Show("lưu thành công");
            else if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.Exist)
                MessageBox.Show("từ vựng đã tồn tại");
            else if (DataProvider.getInstance().AddVocab(vocab) == (int)VocabEnum.False)
                MessageBox.Show("lưu thất bại");
        }
    }
}
