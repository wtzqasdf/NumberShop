using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NumberShop.Models.Tables
{
    public class Review
    {
        public string MerchID { get; set; }
        [JsonIgnore]
        public string Account { get; set; }
        [NotMapped]
        [JsonProperty("account")]
        public string AccountText { get {
                return Account.Substring(0, Account.Length / 2).PadRight(Account.Length, '*');
            } }
        public int Score { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public DateTime PostDate { get; set; }
        [NotMapped]
        [JsonProperty("postdate")]
        public string PostDateText { get { return PostDate.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
