using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G02Apis.Models
{
    public class NhatKyDTO
    {
        public int NhatKyId { get; set; }
        public int? VanBanId { get; set; }
        public string SoVanBan { get; set; }
        public string HanhDong { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}