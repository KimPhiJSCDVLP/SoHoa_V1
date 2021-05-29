using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamMobile.EntityModels
{
    public class UserInfo : INotifyPropertyChanged
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("FullName")]
        public string FullName { get; set; }
        [JsonProperty("HoGiaDinhId")]
        public int HoGiaDinhId { get; set; }
        [JsonProperty("ToaNhaId")]
        public int ToaNhaId { get; set; }
        [JsonProperty("UserId")]
        public int UserId { get; set; }
        [JsonProperty("PhongId")]
        public int PhongId { get; set; }
        [JsonProperty("LoaiTaiKhoanId")]
        public int LoaiTaiKhoanId { get; set; }
        private string _anhDaiDien;
        [JsonProperty("AnhDaiDien")]
        public string AnhDaiDien {
            get
            {
                return _anhDaiDien;
            }
            set
            {
                _anhDaiDien = value;
                OnPropertyChanged(nameof(AnhDaiDien));
            }
        }
        [JsonProperty("TenPhong")]
        public string TenPhong { get; set; }
        [JsonProperty("DienTich")]
        public string DienTich { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("TenHoGiaDinh")]
        public string TenHoGiaDinh { get; set; }
        [JsonProperty("SDT")]
        public string SDT { get; set; }
        [JsonProperty("TenToaNha")]
        public string TenToaNha { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
