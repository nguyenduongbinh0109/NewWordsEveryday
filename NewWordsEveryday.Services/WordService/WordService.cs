using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWordsEveryday.Services.WordService
{
    public interface IWordService
    {
        Task<List<string>> generateTenWordsAsync();
    }
    public class WordService:IWordService
    {
        public async Task<List<string>> generateTenWordsAsync()
        {
            List<string> result = new List<string>();
            try
            {
                string[] lines = await File.ReadAllLinesAsync(Directory.GetCurrentDirectory() + "/wordList.txt");
                if(lines.Count() > 10)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        Random random = new Random();
                        result.Add(lines[random.Next(0, 2999)]);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}
