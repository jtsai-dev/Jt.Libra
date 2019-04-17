using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Application
{
    public class WxUserInfoResponse : WxBaseReponse
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public int Gender { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("headimgurl")]
        public string Avatar { get; set; }
        [JsonProperty("privilege")]
        public List<string> Privilege { get; set; }
        [JsonProperty("unionid")]
        public string Unionid { get; set; }
    }
}
