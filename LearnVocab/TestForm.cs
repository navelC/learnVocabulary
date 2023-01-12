using LearnVocab.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnVocab
{
    public partial class TestForm : Form
    {
        List<Test> test = new List<Test>();
        public TestForm()
        {
            InitializeComponent();
            test = DataProvider.getInstance().test;

        }
        private void LoadData()
        {
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var item in test)
            {
                GroupBox groupBox1 = new GroupBox();
                groupBox1.Location = new System.Drawing.Point(3, 3);
                groupBox1.Name = "groupBox1";
                groupBox1.Size = new System.Drawing.Size(250, 118);
                groupBox1.TabIndex = 0;
                groupBox1.TabStop = false;
                groupBox1.Text = item.InitTime.Date.ToString();
                this.flowLayoutPanel1.Controls.Add(groupBox1);
                double days = (item.TestTime.Year == 1) ? Math.Round((DateTime.Now - item.InitTime).TotalDays) : Math.Round((DateTime.Now - item.TestTime).TotalDays);
                if ((item.numberOfTest == 0 && days < 1) || (item.numberOfTest == 1 && days < 3) || (item.numberOfTest == 2 && days < 7))
                {
                    Label label = new Label();
                    label.Location = new System.Drawing.Point(groupBox1.Location.X + 30, groupBox1.Location.Y + 30);
                    label.AutoSize = true;
                    if (item.numberOfTest == 0) days = 1 - days;
                    else if (item.numberOfTest == 1) days = 3 - days;
                    else if (item.numberOfTest == 2) days = 7 - days;
                    label.Text = $"lần kiểm tra tiếp theo: {days} ngày tới";
                    groupBox1.Controls.Add(label);
                }
                else
                {
                    Button btn = new Button();
                    btn.Location = new System.Drawing.Point(groupBox1.Location.X + 30, groupBox1.Location.Y + 30);
                    btn.Text = "Làm bài";
                    btn.Tag = item.ID;
                    btn.Size = new Size(80, 30);
                    btn.Click += btn_Click;
                    groupBox1.Controls.Add(btn);
                }

            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            var vocab = DataProvider.getInstance().vocab;
            int id = Int32.Parse(((Button)sender).Tag.ToString());
            if(test.Where(x => x.ID == id).FirstOrDefault().numberOfTest == 1) new QuizzForm2(vocab.Where(x => x.TestID == id).ToList()).Show();
            else new QuizzForm(vocab.Where(x => x.TestID == id).ToList());
        }
        private void TestForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test.OrderBy(x => x.InitTime);
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test.OrderByDescending(x => x.InitTime);
            LoadData();
        }
    }
}
