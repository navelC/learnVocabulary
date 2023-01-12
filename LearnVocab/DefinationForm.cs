using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LearnVocab
{
    public partial class DefinationForm : Form
    {
        static HttpClientHandler handle = new HttpClientHandler();
        static HttpClient client;
        List<MediaFoundationReader> mfs = new List<MediaFoundationReader>();
        string english;
        string define;
        string type;
        List<Phonetic> phonetics = new List<Phonetic>();
        Dictionary<string, MediaFoundationReader> dic = new Dictionary<string, MediaFoundationReader>();
        public DefinationForm()
        {
            InitializeComponent();
            this.labelEn.Visible = false;
            this.saveBtn.Visible = false;
            this.labelVSub.Visible = false;
            client = new HttpClient(handle);
        }
        private async void load(string english)
        {
            dic.Clear();
            mfs.Clear();
            phonetics.Clear();
            saveBtn.Enabled = false;
            labelEn.Text = english;
            this.english = english;
            this.panel1.Controls.Clear();
            string vsub = await GetVSub(english);
            dynamic vocab = new ExpandoObject();
            var en = await GetESub(english);
            vocab.phons = en.GetType().GetProperty("phons").GetValue(en, null);
            vocab.esubs = en.GetType().GetProperty("esubs").GetValue(en, null);
            this.labelEn.Visible = true;
            this.saveBtn.Visible = true;
            this.labelVSub.Visible = true;
            vocab.Vietnamese = vsub;
            vocab.English = english;
            if (vocab != null)
            {
                int x = 3;
                int y = 61;
                int phonsCount = 0;
                foreach(var z in vocab.phons)
                {
                    string pattern = @"\.\w+";
                    var matches = Regex.Matches(z.audio.ToString(), pattern);
                    string ex = "";
                    foreach (Match m in matches)
                    {
                        ex = m.Value;
                    }
                    if (z.audio != "" && ex == ".mp3")
                    {
                        Label label1 = new Label();
                        label1.Location = new System.Drawing.Point(x, y);
                        label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label1.AutoSize = true;
                        label1.Text = z.region;
                        x += 30;
                        this.panel1.Controls.Add(label1);

                        PictureBox pic = new PictureBox();
                        pic.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        pic.Size = new System.Drawing.Size(25, 25);
                        pic.Cursor = Cursors.Hand;
                        pic.Image = global::LearnVocab.Properties.Resources.Speaker_Icon_svg;
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        pic.Tag = phonsCount;
                        var mf = new MediaFoundationReader(z.audio.ToString());
                        mfs.Add(mf);
                        dic.Add(z.region.ToString(), mf);
                        phonsCount++;
                        pic.Click += PlaySound;
                        this.panel1.Controls.Add(pic);

                        Label label = new Label();
                        label.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label.AutoSize = true;
                        label.Text = z.text;
                        x += label.Size.Width;
                        this.panel1.Controls.Add(label);
                    }
                }
                y += 40;
                x = 3;
                Label label2 = new Label();
                label2.Text = "";
                label2.BorderStyle = BorderStyle.Fixed3D;
                label2.AutoSize = false;
                label2.Location = new System.Drawing.Point(x, y);
                label2.Height = 2;
                label2.Width = panel1.Width-3;
                this.panel1.Controls.Add(label2);
                bool check = true;
                foreach (var es in vocab.esubs)
                {
                    y += 40;
                    Label label = new Label();
                    label.Location = new System.Drawing.Point(x, y);
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.AutoSize = true;
                    label.Text = es.PartOfSpeech;
                    this.panel1.Controls.Add(label);
                    int count = 1;
                    foreach (var def in es.defs)
                    {
                        y += 40;
                        Label label3 = new Label();
                        label3.Location = new System.Drawing.Point(x + 20, y);
                        label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label3.AutoSize = true;
                        label3.Text = count.ToString();
                        this.panel1.Controls.Add(label3);

                        RadioButton label4 = new RadioButton();
                        label4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
                        label4.Location = new System.Drawing.Point(x + 40, y);
                        label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label4.AutoSize = true;
                        label4.MaximumSize = new Size(this.panel1.Width - x -80, 0);
                        label4.Text = def.text;
                        label4.Tag = es.PartOfSpeech;
                        if (check)
                        {
                            define = def.text;
                            type = es.PartOfSpeech.ToString();
                            label4.Checked = true;
                            check = false;
                        }
                        label4.CheckedChanged += RadioChange;
                        this.panel1.Controls.Add(label4);
                        if (def.ex != null && def.ex != "")
                        {
                            y += label4.Height;
                            y += 20;
                            Label label5 = new Label();
                            label5.Location = new System.Drawing.Point(x + 40, y);
                            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            label5.AutoSize = true;
                            label5.MaximumSize = new Size(this.panel1.Width - x - 40, 0);
                            label5.Text = def.ex;
                            this.panel1.Controls.Add(label5);
                            y += label5.Height;
                        }
                        count++;

                    }
                }
            }
            labelVSub.Text = vocab.Vietnamese;
            this.panel1.Controls.Add(labelVSub);
            saveBtn.Enabled = true;

        }
        private void RadioChange(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                define = ((RadioButton)sender).Text;
                type = ((RadioButton)sender).Tag.ToString();
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
        private async Task<Object> GetESub(string english)
        {
            string url = String.Format
            ("https://api.dictionaryapi.dev/api/v2/entries/en/"+english);
            var response = await client.GetAsync(url);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(result);
            //List<Phonetic> phonetics = new List<Phonetic>();
            List<Object> phons = new List<Object>();
            string pattern = @"\w+(?=\.mp3)";
            foreach (var x in json[0].phonetics)
            {
                try
                {
                    var phon = new
                    {
                        region = Regex.Match(x.audio.ToString(), pattern).Value,
                        text = x.text,
                        audio = x.audio,
                    };
                    if(phon.audio.ToString() != "" && phon.text.ToString() != "")
                    {
                        phons.Add(phon);
                        phonetics.Add(new Phonetic(phon.text.ToString(), phon.audio.ToString(), phon.region.ToString()));
                    }
                }
                catch
                {
                    continue;
                }
                    
                    //Phonetic phon = new Phonetic(x.text.ToString(), x.audio.ToString());
                    //phonetics.Add(phon);
               
            }
            //List<ESub> esubs = new List<ESub>();
            List<Object> esubs = new List<Object>();
            foreach (var mean in json[0].meanings)
            {
                string PartOfSpeech = mean.partOfSpeech;
                List<Object> defs = new List<Object>();
                //List<Definition> defs = new List<Definition>();
                foreach (var def in mean.definitions)
                {
                    try
                    {
                        if(def.example != null)
                        {
                            dynamic definition = new { text = def.definition.ToString(), ex = def.example.ToString() };
                            defs.Add(definition);

                        }
                        else
                        {
                            dynamic definition = new { text = def.definition.ToString(), ex = ""};
                            defs.Add(definition);

                        }
                        //Definition definition;

                        //if (ex is null)
                        //definition = new Definition(def.definition.ToString());
                        //else definition = new Definition(def.definition.ToString(), ex);
                        //defs.Add(definition);
                    }
                    catch
                    {
                        continue;
                    }
                }
                //ESub esub = new ESub(PartOfSpeech, defs);
                var esub = new
                {
                    PartOfSpeech,
                    defs,
                };
                esubs.Add(esub);
            }
            //Vocab vocab = new Vocab(phonetics, esubs);
            var vocab = new {phons, esubs};
            return vocab;
        }
        private async Task<string> GetVSub(string text)
        {
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             "en", "vi", Uri.EscapeUriString(text));
            string result = await client.GetStringAsync(url);

            return result.Split('\"')[1];

        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            Vocab vocab = new Vocab(phonetics, new ESub(type, define));
            vocab.English = english;
            vocab.Vietnamese = labelVSub.Text;
            new AddVocabForm1(vocab, dic).ShowDialog();
            //saveBtn.Visible = false;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                button1_Click(null, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                load(textBox1.Text);
            }
            else
                MessageBox.Show("bạn chưa nhập từ vào");
        }
    }
}
