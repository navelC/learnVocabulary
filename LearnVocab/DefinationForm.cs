using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LearnVocab
{
    public partial class DefinationForm : Form
    {
        static HttpClientHandler handle = new HttpClientHandler();
        static HttpClient client;
        public DefinationForm()
        {
            InitializeComponent();
            handle.Proxy = null;
            handle.UseProxy = false;
            client = new HttpClient(handle);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("nguồn không được để trống");
            }
            string vsub = await GetVSub();
            Vocab esub = await GetESub();
            esub.VSub = vsub;
            if (esub != null)
            {
                int x = 3;
                int y = 41;
                foreach(var z in esub.Phonetics)
                {
                    if( z.Audio != "")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Location = new System.Drawing.Point(x, y);
                        x += 30;
                        pic.Size = new System.Drawing.Size(25, 25);
                        pic.Image = global::LearnVocab.Properties.Resources.Speaker_Icon_svg;
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        this.panel1.Controls.Add(pic);
                    }
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
            labelVSub.Text = esub.VSub;
        }
        private async Task<Vocab> GetESub()
        {
            string url = String.Format
            ("https://api.dictionaryapi.dev/api/v2/entries/en/"+textBox1.Text);
            var response = await client.GetAsync(url);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(result);
            List<Phonetic> phonetics = new List<Phonetic>();
            foreach (var x in json[0].phonetics)
            {
                try
                {
                    Phonetic phon = new Phonetic(x.text.ToString(), x.audio.ToString());
                    phonetics.Add(phon);
                }
                catch
                {
                    continue;
                }
            }
            List<ESub> esubs = new List<ESub>();
            foreach (var mean in json[0].meanings)
            {
                string PartOfSpeech = mean.partOfSpeech;
                List<Definition> defs = new List<Definition>();
                foreach (var def in mean.definitions)
                {
                    try
                    {
                        string ex = def.example.ToString();
                        Definition definition;
                        if (ex is null) definition = new Definition(def.definition.ToString());
                        else definition = new Definition(def.definition.ToString(), ex);
                        defs.Add(definition);
                    }
                    catch
                    {
                        continue;
                    }
                }
                ESub esub = new ESub(PartOfSpeech, defs);
                esubs.Add(esub);
            }
            Vocab vocab = new Vocab(phonetics, esubs);
            return vocab;
        }
        private async Task<string> GetVSub()
        {
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             "en", "vi", Uri.EscapeUriString(textBox1.Text));
            string result = await client.GetStringAsync(url);

            return result.Split('\"')[1];

        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                button1_Click(null, null);
        }
    }
}
