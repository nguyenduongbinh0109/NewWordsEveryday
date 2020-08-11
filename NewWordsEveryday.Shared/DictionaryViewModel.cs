using System;
using System.Collections.Generic;
using System.Text;

namespace NewWordsEveryday.Shared
{
    public class DictionaryViewModel
    {
        public WordViewModel Word { get; set; }
        public List<string> Defination { get; set; }
        public List<string> Symonyms { get; set; }
        
    }
    public class WordViewModel 
    {
        public string Word { get; set; }
        public string PartsOfSpeech { get; set; }
    }
}
