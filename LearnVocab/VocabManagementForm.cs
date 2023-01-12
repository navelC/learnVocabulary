using LearnVocab.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            vocabs = DataProvider.getInstance().vocab;
            var vocab1 = vocabs.Where(x => x.Category == "No");
            if(vocab1.Count() > 0)
            {
                foreach (var vocab in vocab1)
                {
                    if(vocab.TestID == null)
                    {
                        CheckBox checkbox = new CheckBox();
                        checkbox.AutoSize = true;
                        checkbox.Location = new System.Drawing.Point(3, 3);
                        checkbox.Name = vocab.English;
                        checkbox.Size = new System.Drawing.Size(80, 17);
                        checkbox.TabIndex = 1;
                        checkbox.Tag = vocab.ID;
                        checkbox.Text = vocab.English;
                        checkbox.UseVisualStyleBackColor = true;
                        ToolTip tt = new ToolTip();
                        tt.SetToolTip(checkbox, vocab.Vietnamese);
                        checkBoxes.Add(checkbox);
                        this.flowLayoutPanel1.Controls.Add(checkbox);
                    }
                    else
                    {
                        Label checkbox = new Label();
                        checkbox.DoubleClick += label_DoubleClick;
                        checkbox.AutoSize = true;
                        checkbox.Location = new System.Drawing.Point(3, 3);
                        checkbox.Name = vocab.English;
                        checkbox.Size = new System.Drawing.Size(80, 17);
                        checkbox.TabIndex = 1;
                        checkbox.Tag = vocab.ID;
                        checkbox.Text = vocab.English;
                        ToolTip tt = new ToolTip();
                        tt.SetToolTip(checkbox, vocab.Vietnamese);
                        this.flowLayoutPanel1.Controls.Add(checkbox);
                    }
                    
                }
            }
            else
            {
                Label label = new Label();
                label.Text = "rỗng";
                this.flowLayoutPanel1.Controls.Add(label);
            }
            var cate = DataProvider.getInstance().category.Where(x => x.Name != "No");
            if(cate.Count() > 0)
            {
                foreach (var category in cate)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.DoubleClick += GroupBox_DoubleClick;
                    var vcab = vocabs.Where(c => c.Category == category.Name);
                    int count = 0;
                    int x = groupBox.Location.X + 20;
                    int y = groupBox.Location.Y + 20;
                    foreach (var vc in vcab)
                    {
                        if (count == 3) break;
                        count++;
                        Label label = new Label();
                        label.Location = new System.Drawing.Point(x,  + y);
                        label.AutoSize = true;
                        label.Tag = vc.ID;
                        label.DoubleClick += label_DoubleClick;
                        label.Text = vc.English;
                        ToolTip tt = new ToolTip();
                        tt.SetToolTip(label, vc.Vietnamese);
                        groupBox.Controls.Add(label);
                        y += 20;
                    }
                    groupBox.Size = new System.Drawing.Size(169, 100);
                    groupBox.TabIndex = 0;
                    groupBox.TabStop = false;
                    groupBox.Text = category.Name;
                    this.flowLayoutPanel2.Controls.Add(groupBox);
                }
            }
            else
            {
                Label label = new Label();
                label.Text = "rỗng";
                this.flowLayoutPanel2.Controls.Add(label);
            }

        }
        private void label_DoubleClick(object sender, EventArgs e)
        {
            int id = Int32.Parse(((Label)sender).Tag.ToString());
            new VocabForm(vocabs.Where(x => x.ID == id).FirstOrDefault()).Show();
        }
        private void GroupBox_DoubleClick(object sender, EventArgs e)
        {
            var cate = ((GroupBox)sender).Text.ToString();
            new GroupForm(cate).Show();
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
            List<int> english = new List<int>();
            foreach(var item in checkBoxes)
            {
                if (item.Checked == true)
                {
                    english.Add(Int32.Parse(item.Tag.ToString()));
                }
            }
            if(english.Count < 8)
            {
                MessageBox.Show("hãy chọn ít nhất 8 từ vựng để học");
                return;
            }
            var s = vocabs.Where(c => english.Contains(c.ID)).ToList();
            new learnForm(s).ShowDialog();
        }
    }
    public partial class GroupForm : Form
    {
        FlowLayoutPanel flowLayoutPanel1;
        List<CheckBox> ckboxs = new List<CheckBox>();
        List<Vocab> vocabs;
        private string cate;
        public GroupForm(string category)
        {
            InitializeComponent();
            this.cate = category;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = cate;
            vocabs = DataProvider.getInstance().vocab;
            var vocab1 = vocabs.Where(x => x.Category == cate);
            foreach (var vocab in vocab1)
            {
                string text = $"{vocab.English} ({vocab.ESubs.PartOfSpeech}): {vocab.Vietnamese}";
                if (vocab.TestID == null)
                {
                    CheckBox checkbox = new CheckBox();
                    ckboxs.Add(checkbox);
                    checkbox.AutoSize = true;
                    checkbox.Location = new System.Drawing.Point(3, 3);
                    checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    checkbox.Name = vocab.English;
                    checkbox.TabIndex = 1;
                    checkbox.Text = text;
                    checkbox.Tag = vocab.ID;
                    checkbox.UseVisualStyleBackColor = true;
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(checkbox, vocab.Vietnamese);
                    this.flowLayoutPanel1.Controls.Add(checkbox);
                }
                else
                {
                    Label checkbox = new Label();
                    checkbox.AutoSize = true;
                    checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    checkbox.Location = new System.Drawing.Point(3, 3);
                    checkbox.Name = vocab.English;
                    checkbox.TabIndex = 1;
                    checkbox.Text = text;
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(checkbox, vocab.Vietnamese);
                    this.flowLayoutPanel1.Controls.Add(checkbox);
                }
            }
        }
        private void ChooseAllBtn_Click(object sender, EventArgs e)
        {
            foreach(CheckBox checkBox in ckboxs)
            {
                checkBox.Checked = true;
            }
        }
        private void UnChooseAllBtn_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in ckboxs)
            {
                checkBox.Checked = false;
            }
        }
        private void LearnBtn_Click(object sender, EventArgs e)
        {
            List<int> english = new List<int>();
            foreach (var item in ckboxs)
            {
                if (item.Checked == true)
                {
                    english.Add(Int32.Parse(item.Tag.ToString()));
                }
            }
            if (english.Count < 8)
            {
                MessageBox.Show("hãy chọn ít nhất 8 từ vựng để học");
                return;
            }
            var s = vocabs.Where(c => english.Contains(c.ID)).ToList();
            new learnForm(s).ShowDialog();
        }
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(582, 390);
            flowLayoutPanel1.TabIndex = 1;
            ///
            Button btn = new Button();
            btn.Click += ChooseAllBtn_Click;
            btn.Location = new System.Drawing.Point(20, 10);
            btn.Text = "chọn tất cả";
            btn.Size = new System.Drawing.Size(120, 30);
            btn.TabIndex = 2;
            // 
            Button btn1 = new Button();
            btn1.Click += UnChooseAllBtn_Click;
            btn1.Location = new System.Drawing.Point(170, 10);
            btn1.Text = "bỏ chọn tất cả";
            btn1.Size = new System.Drawing.Size(120, 30);
            btn1.TabIndex =3;
            //
            Button btn2 = new Button();
            btn2.Click += LearnBtn_Click;
            btn2.Location = new System.Drawing.Point(320, 10);
            btn2.Text = "học từ vựng";
            btn2.Size = new System.Drawing.Size(120, 30);
            btn2.TabIndex = 4;
            // GroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 450);
            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(btn);
            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
            this.Name = "GroupForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
