using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NewWordsEveryday.Shared
{
    public class ThirdPartyAPIResponeViewModel
    {

        [JsonPropertyName("meta")]
        public Meta meta { get; set; }
        [JsonPropertyName("fl")]
        public string fl { get; set; }
        [JsonPropertyName("shortdef")]
        public List<string> shortdef { get; set; }
       
    }
    public class Meta
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("syns")]
        public List<List<string>> syns { get; set; }
    }

}
