using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnVocab.DAL
{
    internal class DataProvider
    {
        string filePath = "..\\..\\..\\..\\learnvocab\\LearnVocab\\Resources\\data.json";
        public dynamic data;
        private DataProvider() { }
        public DataProvider getInstance()
        {
            if (data is null) { data = getData(); }
            return data;
        }
        public dynamic getData()
        {
            return JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filePath));
        }
        public bool saveData(object obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);
                System.IO.File.WriteAllText(filePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
