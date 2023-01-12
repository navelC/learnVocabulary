using LearnVocab.DAL;
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
    public partial class learnForm : Form
    {
        List<MediaFoundationReader> mfs = new List<MediaFoundationReader>();
        List<Vocab> vocabs;
        Test test; 
        public learnForm(List<Vocab> vc, string result = null, List<string> input = null)
        {
            InitializeComponent();
            this.Text = "Kết quả";
            bool check = true;
            vocabs = vc;
            if (result == null)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
                label1.Text = "số câu đúng của bạn là: " + result;
                button1.Visible = false;
                button1.Enabled = false;
                button2.Visible = false;
                button2.Enabled = false;
                if (vocabs[0].TestID == null)
                    check = false;
                else
                {
                    test = DataProvider.getInstance().test.Where(x => x.ID == vocabs[0].TestID).FirstOrDefault();
                    test.TestTime = DateTime.Now;
                    test.numberOfTest += 1;
                }
            }
            int count = 1;
            int phonsCount = 0;
            for (int i = 0; i < vocabs.Count; i++)
            {
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.AutoSize = true;
                panel.FlowDirection = FlowDirection.LeftToRight;
                panel.MaximumSize = new System.Drawing.Size(435, 40);
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                string text = $"{count}. {vocabs[i].English} ({vocabs[i].ESubs.PartOfSpeech}): {vocabs[i].Vietnamese}";
                if (result != null)
                {
                    if (test.numberOfTest == 3)
                    {
                        vocabs[i].TestID = null;
                    }
                    if (input[i] != vocabs[i].English)
                    {
                        if (check)
                        {
                            vocabs[i].NumberOfTest += 1;
                        }
                            label.ForeColor = Color.Red;
                        text += " <-- " + input[i];
                    }
                    else
                    {
                        if (check)
                        {
                            vocabs[i].NumberOfTest += 1;
                            vocabs[i].Passed += 1;
                            if (test.numberOfTest == 3)
                                vocabs[i].Passed = vocabs[i].NumberOfTest;
                        }
                        label.ForeColor = Color.Green;
                    }
                    DataProvider.getInstance().Save();
                }
                label.Text = text;
                count++;
                if (vocabs[i].Phonetics != null) 
                    foreach (Phonetic p in vocabs[i].Phonetics) {
                        if (p.Audio != null && p.Audio != "")
                        {
                            Label label1 = new Label();
                            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            label1.AutoSize = true;
                            label1.Padding = new Padding(0, 5, 0, 2);
                            label1.Text = p.Region;
                            panel.Controls.Add(label1);

                            PictureBox pic = new PictureBox();
                            pic.Size = new System.Drawing.Size(25, 25);
                            pic.Cursor = Cursors.Hand;
                            pic.Image = global::LearnVocab.Properties.Resources.Speaker_Icon_svg;
                            pic.SizeMode = PictureBoxSizeMode.Zoom;
                            pic.Tag = phonsCount;
                            phonsCount++;
                            var mf = new MediaFoundationReader(p.Audio);
                            mfs.Add(mf);
                            pic.Click += PlaySound;
                            panel.Controls.Add(pic);

                            Label label2 = new Label();
                            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            label2.AutoSize = true;
                            label2.Padding = new Padding(0, 5, 0, 2);
                            label2.Text = p.Text;
                            panel.Controls.Add(label2);
                        }
                    }
                this.flowLayoutPanel1.Controls.Add(label);
                this.flowLayoutPanel1.Controls.Add(panel);
                if (vocabs[i].ESubs.Text != null)
                {
                    Label label2 = new Label();
                    label2.AutoSize = true;
                    label2.Padding = new Padding(10, 5, 0, 5);
                    label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label2.Text = vocabs[i].ESubs.Text;
                    this.flowLayoutPanel1.Controls.Add(label2);
                }
                this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(435, 1009);
                this.flowLayoutPanel1.AutoSize = true;
                this.AutoSize = true;
            }
        }
        private void PlaySound(object sender, EventArgs e)
        {
            var index = Int32.Parse(((PictureBox)sender).Tag.ToString());
            using (var wo = new WasapiOut())
            {
                mfs[index].Seek(0, SeekOrigin.Begin);
                wo.Init(mfs[index]);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(10);
                }
                wo.Dispose();
                mfs[index].Dispose();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new QuizzForm(vocabs).Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DataProvider.getInstance().AddTest(vocabs))
            {
                MessageBox.Show("lưu test thành công");
            }
            else
            {
                MessageBox.Show("lưu test thất bại");
            }
        }
    }
}
