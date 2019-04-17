﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Application
{
    public class WxBaseReponse
    {
        [JsonProperty("errcode")]
        public int? ErrCode { get; set; }
        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}
