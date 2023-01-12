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
    public partial class VocabForm : Form
    {
        List<MediaFoundationReader> mfs = new List<MediaFoundationReader>();
        public VocabForm(Vocab vocab)
        {
            InitializeComponent();
            this.Text = vocab.English;
            this.AutoSize = true;
            load(vocab);
        }
        private void load(Vocab vocab)
        {
            lblEn.Text = vocab.English+": "+vocab.Vietnamese;
            lblType.Text = vocab.ESubs.PartOfSpeech;
            int x = lblType.Location.X + 10;
            int y = lblType.Location.Y + lblType.Size.Height + 10;
            int phonsCount = 0;
            if (vocab.Phonetics != null)
                foreach (Phonetic p in vocab.Phonetics)
                {
                    if (p.Audio != null && p.Audio != "")
                    {
                        Label label1 = new Label();
                        label1.Location = new System.Drawing.Point(x, y);
                        label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label1.AutoSize = true;
                        label1.Text = p.Region;
                        x += 30;
                        this.Controls.Add(label1);

                        PictureBox pic = new PictureBox();
                        pic.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        pic.Size = new System.Drawing.Size(25, 25);
                        pic.Cursor = Cursors.Hand;
                        pic.Image = global::LearnVocab.Properties.Resources.Speaker_Icon_svg;
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        pic.Tag = phonsCount;
                        var mf = new MediaFoundationReader(p.Audio);
                        mfs.Add(mf);
                        phonsCount++;
                        pic.Click += PlaySound;
                        this.Controls.Add(pic);

                        Label label = new Label();
                        label.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label.AutoSize = true;
                        label.Text = p.Text;
                        x += label.Size.Width;
                        this.Controls.Add(label);
                    }
            }
            lblDefine.Text = vocab.ESubs.Text;

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
    }
}
