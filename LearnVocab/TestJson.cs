using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab
{
    internal class TestJson
    {
        string filePath = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\test.json";
        public TestJson()
        {
            dynamic obj = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filePath));
            List<Cate> a = JsonConvert.DeserializeObject<List<Cate>>(obj["category"].ToString());
            List<Voca> b = JsonConvert.DeserializeObject<List<Voca>>(obj["vocab"].ToString());
            b.Add(new Voca { english = "kind", vietnamese = "loại" });
            var ob = new { vocab = b, category = a};
            string json = JsonConvert.SerializeObject(ob);
            System.IO.File.WriteAllText(filePath, json);
        }
    }
    class Voca
    {
        public string vietnamese { get; set; }
        public string english { get; set; }
    }
    class Cate
    {
        public string Name { get; set; }
    }
}
