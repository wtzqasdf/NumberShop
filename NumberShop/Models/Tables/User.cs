using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumberShop.Models.Tables
{
    public class User
    {
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("identity")]
        public string Identity { get; set; }
        [JsonProperty("permission")]
        public int Permission { get; set; }
        [JsonIgnore]
        public DateTime RegDate { get; set; }
        [NotMapped]
        [JsonProperty("regdate")]
        public string RegDateText { get { return RegDate.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
