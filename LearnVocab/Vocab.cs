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
        public List<ESub> ESubs { get; set; }
        public int NumberOfTest { get; set; }
        public int Passed { get; set; }
        public Vocab(string vSub, List<Phonetic> phonetics, List<ESub> eSubs)
        {
            NumberOfTest = 0;
            Passed = 0;
            Vietnamese = vSub;
            Phonetics = phonetics;
            ESubs = eSubs;
        }
        public Vocab() {}
        public Vocab(List<Phonetic> phonetics, List<ESub> eSubs)
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
        public List<Definition> Definitions { get; set; }
        public ESub(string partOfSpeech, List<Definition> definitions)
        {
            PartOfSpeech = partOfSpeech;
            Definitions = definitions;
        }
        public ESub() { }
    }
    public class Definition
    {
        public string Text { get; set; }
        public string Example { get; set; }
        public Definition(string text, string example)
        {
            Text = text;
            Example = example;
        }
        public Definition(string text)
        {
            Text = text;
        }
        public Definition() { }
    }
}
