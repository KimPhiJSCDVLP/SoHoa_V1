using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamMobile.EntityModels
{
    public class PhieuThuEntity
    {
        [JsonProperty("PhieuThuId")]
        public int PhieuThuId { get; set; }
        [JsonProperty("PhongId")]
        public Nullable<int> PhongId { get; set; }
        [JsonProperty("TieuDe")]
        public string TieuDe { get; set; }
        [JsonProperty("TrangThai")]
        public Nullable<int> TrangThai { get; set; }
        [JsonProperty("HanThanhToan")]
        public Nullable<System.DateTime> HanThanhToan { get; set; }
        [JsonProperty("Thang")]
        public Nullable<int> Thang { get; set; }
        [JsonProperty("Nam")]
        public Nullable<int> Nam { get; set; }

        public string HanThanhToanFormat
        {
            get
            {
                return $"Hạn thanh toán: {HanThanhToan?.ToString("dd - MMM - yyyy")}";
            }
        }

        private string GetTrangThai()
        {
            if(TrangThai == 1)
            {
                return "Chưa đóng";
            }
            else if(TrangThai == 2)
            {
                return "Quá hạn";
            }
            else if(TrangThai == 3)
            {
                return "Đã đóng";
            }
            return "";
        }
        public string TenPhieuThu
        {
            get
            {
                return $"{TieuDe} - {Thang}/{Nam} - {GetTrangThai()}";
            }
        }
    }

}
