using Newtonsoft.Json;
using NewWordsEveryday.Services.WordService;
using NewWordsEveryday.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewWordsEveryday.Services.DictionaryService
{
    public interface IDictionaryService
    {
        Task<List<DictionaryViewModel>> GetDefinationOfTenWordsAsync();
    }
    public class DictionaryService: IDictionaryService
    {
        private readonly IWordService _word;
        private readonly string key = "9a673235-4bc8-48bc-b2e2-29cb69a6b29d";
        public DictionaryService(IWordService word)
        {
            _word = word;
        }

        public async Task<List<DictionaryViewModel>> GetDefinationOfTenWordsAsync()
        {
            List<DictionaryViewModel> result = new List<DictionaryViewModel>();
            try
            {
                List<string> tenRandomWords = await _word.generateTenWordsAsync();
                if(tenRandomWords.Count == 10)
                {
                    var respones = await GetDefinationFromThirdPartyAsync(tenRandomWords);
                    if (respones.Count == 10)
                    {
                        foreach (var word in respones)
                        {
                            if(word.meta.syns != null && word.meta.syns.Count > 0)
                            {
                                result.Add(new DictionaryViewModel()
                                {
                                    Word = new WordViewModel()
                                    {
                                        Word = word.meta.id,
                                        PartsOfSpeech = word.fl
                                    },
                                    Defination = word.shortdef,
                                    Symonyms = word.meta.syns[0]
                                });
                            }
                            else
                            {
                                result.Add(new DictionaryViewModel()
                                {
                                    Word = new WordViewModel()
                                    {
                                        Word = word.meta.id,
                                        PartsOfSpeech = word.fl
                                    },
                                    Defination = word.shortdef
                                });
                            }
                            
                        }
                    }

                }
                


            }
            catch(Exception ex)
            {

            }
            return result;
        }

        private async Task<List<ThirdPartyAPIResponeViewModel>> GetDefinationFromThirdPartyAsync(List<string> tenRandomWords)
        {
            List<ThirdPartyAPIResponeViewModel> result = new List<ThirdPartyAPIResponeViewModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://dictionaryapi.com/api/v3/references/sd3/json/");
                foreach(var word in tenRandomWords)
                {
                    var respone = await client.GetAsync(word + "?key=" + key);
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var converters =  JsonConvert.DeserializeObject<List<ThirdPartyAPIResponeViewModel>>(content);
                        result.Add(converters[0]);
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
