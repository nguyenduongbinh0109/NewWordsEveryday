using System;
using System.ComponentModel.Design;

namespace Testing
{
    class Program
    {
        private readonly IDictionaryService _dictionary;
        public Program(IDictionaryService dictionary)
        {
            _dictionary = dictionary;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
