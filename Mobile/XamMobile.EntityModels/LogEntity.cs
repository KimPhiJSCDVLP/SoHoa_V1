using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamMobile.EntityModels
{
    public class LogEntity
    {
        [JsonProperty("NhatKyId")]
        public int NhatKyId { get; set; }
        [JsonProperty("VanBanId")]
        public Nullable<int> VanBanId { get; set; }
        [JsonProperty("SoVanBan")]
        public string SoVanBan { get; set; }
        [JsonProperty("HanhDong")]
        public string HanhDong { get; set; }
        [JsonProperty("CreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [JsonProperty("UpdatedDate")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        public string CreatedDateFormat
        {
            get
            {
                return CreatedDate?.ToString("dd - MMM - yyyy HH:mm:ss");
            }
        }
    }
}
