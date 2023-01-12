using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnVocab
{
    public partial class QuizzForm2 : Form
    {
        List<Vocab> vocabs;
        int count;
        int current = 0;
        MediaFoundationReader mf;
        List<string> input = new List<string>();
        public QuizzForm2(List<Vocab> vc)
        {
            Shuffle(vc);
            vocabs = vc;
            count = vocabs.Count;
            InitializeComponent();
            this.pictureBox1.Click += PlaySound;
            this.Show();
            load();
        }
        private static Random rng = new Random();

        private void load()
        {
            labelCount.Text = current + 1 + "/" + count;
            if (vocabs.Count == current + 1) button1.Text = "Finish";

            if (vocabs[current].Phonetics != null)
            {
                mf = new MediaFoundationReader(vocabs[current].Phonetics.FirstOrDefault().Audio);
                
                this.pictureBox1.Visible = true;
                labelVn.Visible = false;
                Sound();
            }
            else
            {
                this.pictureBox1.Visible = false;
                labelVn.Visible = true;
                labelVn.Text = vocabs[current].Vietnamese;
            }
            
        }
        public static void Shuffle(List<Vocab> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Vocab value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (input.Count <= current)
            {
                string test = maskedTextBox1.Text.Trim();
                input.Add(test);

            }
            else
            {
                input[current] = maskedTextBox1.Text;
            }
            current++;
            maskedTextBox1.Clear();
            maskedTextBox1.Focus();
            if (current == count)
            {
                int correct = 0;
                for (int i = 0; i < count; i++)
                {
                    if (input[i] == vocabs[i].English) correct++;
                }
                new learnForm(vocabs, correct + "/" + count, input).Show();
                this.Dispose();
                return;

            }
            load();

        }
        private void Sound()
        {
            using (var wo = new WasapiOut())
            {
                mf.Seek(0, SeekOrigin.Begin);
                wo.Init(mf);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(10);
                }
                wo.Dispose();
                mf.Dispose();

            }
        }
        private void PlaySound(object sender, EventArgs e)
        {
            Sound();

        }
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            //if (input.Count <= current)
            //{
            //    string test = maskedTextBox1.Text;
            //    input.Add(maskedTextBox1.Text);

            //}
            //else
            //{
            //    input[current] = maskedTextBox1.Text;
            //}
        }
    }
}
