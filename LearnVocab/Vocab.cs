using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnVocab
{
    internal class Vocab
    {
        public string VSub {get; set; }
        public List<Phonetic> Phonetics {get; set; }
        public List<ESub> ESubs { get; set; }
        public Vocab(string vSub, List<Phonetic> phonetics, List<ESub> eSubs)
        {
            VSub = vSub;
            Phonetics = phonetics;
            ESubs = eSubs;
        }
        public Vocab(List<Phonetic> phonetics, List<ESub> eSubs)
        {
            Phonetics = phonetics;
            ESubs = eSubs;
        }
    }
    class Phonetic
    {
        public string Text { get; set; }
        public string Audio { get; set; }
        public Phonetic(string text, string audio)
        {
            Text = text;
            Audio = audio;
        }
    }
    class ESub
    {
        public string PartOfSpeech { get; set; }
        public List<Definition> Definitions { get; set; }
        public ESub(string partOfSpeech, List<Definition> definitions)
        {
            PartOfSpeech = partOfSpeech;
            Definitions = definitions;
        }
    }
    class Definition
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
    }
}
