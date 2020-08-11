using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewWordsEveryday.MVC.Models;
using NewWordsEveryday.Services.DictionaryService;

namespace NewWordsEveryday.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDictionaryService _dictionary;
        public HomeController(ILogger<HomeController> logger, IDictionaryService dictionary)
        {
            _logger = logger;
            _dictionary = dictionary;
        }

        private string ConvertString(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                StringBuilder sb = new StringBuilder(word);
                sb[0] = char.ToUpper(sb[0]);
                if ((sb[(sb.Length - 2)].Equals(':')))
                {
                    sb = sb.Remove(sb.Length - 2, 2);
                }
                return sb.ToString();

            }
            else return word;

        }
      

        public async Task<IActionResult> Index()
        {

            if(ModelState == null || ModelState.Count == 0)
            {
                var result = await _dictionary.GetDefinationOfTenWordsAsync();
                if (result.Count > 0)
                {
                    foreach(var word in result)
                    {
                        word.Word.Word = ConvertString(word.Word.Word);
                    }   
                    return View(result);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
           
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
