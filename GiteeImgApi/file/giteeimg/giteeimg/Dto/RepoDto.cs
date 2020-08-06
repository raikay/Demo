using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace giteeimg.Dto
{
    class RepoDto
    {
    }


    public class SaveReposParam
    {
        public string access_token { get; set; }
        public string name { get; set; }
        public bool has_issues { get; set; }
        public bool has_wiki { get; set; }
        public bool can_comment { get; set; }
        public bool auto_init { get; set; }
        [JsonProperty(PropertyName = "private")]
        public bool privateType { get; set; }
    }

}
