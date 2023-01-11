using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LearnVocab
{
    public partial class DefinationForm : Form
    {
        string dataPath = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\data.json";
        static HttpClientHandler handle = new HttpClientHandler();
        static HttpClient client;
        Vocab vocab;
        string english;
        public DefinationForm(string english)
        {
            InitializeComponent();
            handle.Proxy = null;
            handle.UseProxy = false;
            client = new HttpClient(handle);
            this.english = english;
            load();
        }
        private async void load()
        {
            this.panel1.Controls.Clear();
            string vsub = await GetVSub(english);
            dynamic vocab = await GetESub(english);
            vocab.Vietnamese = vsub;
            vocab.English = english;
            if (vocab != null)
            {
                int x = 3;
                int y = 61;
                foreach(var z in vocab.Phonetics)
                {
                    if( z.Audio != "")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        pic.Size = new System.Drawing.Size(25, 25);
                        pic.Cursor = Cursors.Hand;
                        pic.Image = global::LearnVocab.Properties.Resources.Speaker_Icon_svg;
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        pic.Tag = z.Audio;
                        pic.Click += PlaySound;
                        this.panel1.Controls.Add(pic);


                        Label label = new Label();
                        label.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label.AutoSize = true;
                        label.Text = z.Text;
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
                foreach (var es in vocab.ESubs)
                {
                    y += 40;
                    Label label = new Label();
                    label.Location = new System.Drawing.Point(x, y);
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.AutoSize = true;
                    label.Text = es.PartOfSpeech;
                    this.panel1.Controls.Add(label);
                    int count = 1;
                    foreach (var def in es.Definitions)
                    {
                        y += 40;
                        Label label3 = new Label();
                        label3.Location = new System.Drawing.Point(x + 20, y);
                        label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label3.AutoSize = true;
                        label3.Text = count.ToString();
                        this.panel1.Controls.Add(label3);

                        RadioButton label4 = new RadioButton();
                        if (check)
                        {
                            label4.Checked = true;
                            check = false;
                        }
                        label4.CheckAlign = ContentAlignment.MiddleRight;
                        label4.Location = new System.Drawing.Point(x + 40, y);
                        label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label4.AutoSize = true;
                        label4.MaximumSize = new Size(this.panel1.Width - x -40, 0);
                        label4.Text = def.Text;
                        this.panel1.Controls.Add(label4);
                        if (def.Example != null && def.Example != "")
                        {
                            y += label4.Height;
                            y += 20;
                            Label label5 = new Label();
                            label5.Location = new System.Drawing.Point(x + 40, y);
                            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            label5.AutoSize = true;
                            label5.MaximumSize = new Size(this.panel1.Width - x - 40, 0);
                            label5.Text = def.Example;
                            this.panel1.Controls.Add(label5);
                            y += label5.Height;
                        }
                        count++;

                    }
                }
            }
            labelVSub.Text = vocab.Vietnamese;
            this.panel1.Controls.Add(labelVSub);
            saveBtn.Visible = true;

        }
        private void PlaySound(object sender, EventArgs e)
        {
            var url = ((PictureBox)sender).Tag.ToString();
            using (var mf = new MediaFoundationReader(url))
            using (var wo = new WasapiOut())
            {
                wo.Init(mf);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
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
            List<Phonetic> phonetics = new List<Phonetic>();
            List<Object> phons = new List<Object>();
            foreach (var x in json[0].phonetics)
            {
                try
                {
                    var phon = new
                    {
                        text = x.text,
                        audio = x.audio,
                    };
                    phons.Add(phon);
                    //Phonetic phon = new Phonetic(x.text.ToString(), x.audio.ToString());
                    //phonetics.Add(phon);
                }
                catch
                {
                    continue;
                }
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
                        string ex = null;
                        if(def.example != null)
                            ex = def.example.ToString();
                        //Definition definition;
                        dynamic definition = new { definition = def.definition.ToString()};
                        if (!(ex is null))
                            definition.ex = ex;
                        defs.Add(definition);
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
            new AddVocabForm1(vocab).ShowDialog();
            saveBtn.Visible = false;
        }
    }
}
