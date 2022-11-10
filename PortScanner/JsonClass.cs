using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PortScanner
{
    public struct JsonObject
    {
        
        [JsonProperty("BLD")]
        public string BLD { get; set; }

        [JsonProperty("UBG")]
        public string UBG { get; set; }

        [JsonProperty("BIL")]
        public string BIL { get; set; }

        [JsonProperty("PRO")]
        public string PRO { get; set; }

        [JsonProperty("NIT")]
        public string NIT { get; set; }

        [JsonProperty("KET")]
        public string KET { get; set; }

        [JsonProperty("GLU")]
        public string GLU { get; set; }

        [JsonProperty("pH")]
        public string PH { get; set; }

        [JsonProperty("SG")]
        public string SG { get; set; }

        [JsonProperty("LEU")]
        public string LEU { get; set; }
    }
}
