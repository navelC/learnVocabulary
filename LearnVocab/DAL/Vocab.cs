using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab
{
    public class Vocab
    {
        public int ID { get; set; }
        public string Vietnamese {get; set; }
        public string English { get; set; }
        public List<Phonetic> Phonetics {get; set; }
        public ESub ESubs { get; set; }
        public int NumberOfTest { get; set; }
        public int Passed { get; set; }
        public int? TestID { get; set; }
        public string Category { get; set; }
        public Vocab(string vSub, List<Phonetic> phonetics, ESub eSubs)
        {
            NumberOfTest = 0;
            Passed = 0;
            Vietnamese = vSub;
            Phonetics = phonetics;
            ESubs = eSubs;
        }
        public Vocab() {
            NumberOfTest = 0;
            Passed = 0;
        }
        public Vocab(List<Phonetic> phonetics, ESub eSubs)
        {
            Passed = 0;
            NumberOfTest = 0;
            Phonetics = phonetics;
            ESubs = eSubs;
        }
    }
    public class Phonetic
    {
        public string Text { get; set; }
        public string Audio { get; set; }
        public string Region { get; set; }
        public Phonetic(string text, string audio, string region)
        {
            Region = region;
            Text = text;
            Audio = audio;
        }
        public Phonetic() { }
    }
    public class ESub
    {
        public string PartOfSpeech { get; set; }
        public string Text { get; set; }
        public string Example { get; set; }
        public ESub(string partOfSpeech, string text, string ex)
        {
            PartOfSpeech = partOfSpeech;
            Text = text;
            Example = ex;
        }
        public ESub(string partOfSpeech, string text)
        {
            PartOfSpeech = partOfSpeech;
            Text = text;
        }
        public ESub() { }
    }
}
