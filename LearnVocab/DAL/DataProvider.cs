using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace LearnVocab.DAL
{
    internal class DataProvider
    {
        static string filePath = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\data.json";
        private static DataProvider instance;
        public dynamic data;
        public List<Category> category;
        public List<Vocab> vocab;
        public List<Test> test;
        private DataProvider() { 
            data = getData();
            category = JsonConvert.DeserializeObject<List<Category>>(data["category"].ToString());
            vocab = JsonConvert.DeserializeObject<List<Vocab>>(data["vocab"].ToString());
            test = JsonConvert.DeserializeObject<List<Test>>(data["test"].ToString());
        }
        public static DataProvider getInstance()
        {
            if (instance is null) {
                instance = new DataProvider();
            }
            return instance;
        }
        private dynamic getData()
        {
            return JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filePath));
        }
        public bool AddTest(List<Vocab> vc)
        {
            try
            {
                int id = 0;
                if (this.test.Count > 0) id = this.test.Max(x => x.ID) + 1;
                foreach(Vocab v in vc)
                {
                    v.TestID = id;
                }
                test.Add(new Test(id));
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int AddCategory(Category cate)
        {
            try
            {
                this.category.Add(cate);
                return Save();
            }
            catch
            {
                return (int)VocabEnum.False;
            }
        }
        public int CheckVocabExist(Vocab vocab)
        {
            var check = this.vocab.Where(x => x.English == vocab.English).FirstOrDefault();
            if (check != null)
            {
                if (check.Vietnamese == vocab.Vietnamese)
                    return (int)VocabEnum.Exist;
                else
                    return (int)VocabEnum.ExistHalf;
            }
            return (int)VocabEnum.False;
        }
        public int AddVocab(Vocab vocab,bool check = true)
        {
            try
            {
                if (check && (CheckVocabExist(vocab) != (int)VocabEnum.False))
                {
                    if (CheckVocabExist(vocab) == (int)VocabEnum.ExistHalf && MessageBox.Show($"{vocab.English} đã tồn tại với nghĩa {vocab.Vietnamese}. Bạn có muốn tiếp tục lưu không?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return AddVocab(vocab, false);
                    return (CheckVocabExist(vocab));
                }
                if (!(category.Where(x => x.Name == vocab.Category).Count() > 0)) {
                    if (AddCategory(new Category(vocab.Category)) != (int)VocabEnum.True) return (int)VocabEnum.False;
                }
                int id = 0;
                if (this.vocab.Count > 0) id = this.vocab.Max(x => x.ID)+1;
                vocab.ID = id;
                this.vocab.Add(vocab);
                return Save();
            }
            catch
            {
                return (int)VocabEnum.False;
            }
        }
        public int Save()
        {
            try
            {
                var obj = new { vocab, category, test};
                string json = JsonConvert.SerializeObject(obj);
                System.IO.File.WriteAllText(filePath, json);
                return (int)VocabEnum.True;
            }
            catch
            {
                return (int)VocabEnum.False;
            }
        }
    }
}
