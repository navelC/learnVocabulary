using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab
{
    public class Vocab
    {
        public string Vietnamese {get; set; }
        public string English { get; set; }
        public List<Phonetic> Phonetics {get; set; }
        public ESub ESubs { get; set; }
        public int NumberOfTest { get; set; }
        public int Passed { get; set; }
        public string Category { get; set; }
        public Vocab(string vSub, List<Phonetic> phonetics, ESub eSubs)
        {
            NumberOfTest = 0;
            Passed = 0;
            Vietnamese = vSub;
            Phonetics = phonetics;
            ESubs = eSubs;
        }
        public Vocab() {}
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
        public Phonetic(string text, string audio)
        {
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
