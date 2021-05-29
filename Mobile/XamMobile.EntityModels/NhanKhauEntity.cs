using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace XamMobile.EntityModels
{
    public class NhanKhauEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string GioiTinhStr
        {
            get
            {
                return GioiTinh == true ? "Nam" : "Nữ";
            }
        }
        [JsonProperty("NhanKhauId")]
        public int NhanKhauId { get; set; }

        private string _hoTen;
        [JsonProperty("HoTen")]
        public string HoTen
        {
            get
            {
                return _hoTen;
            }
            set
            {
                _hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }
        [JsonProperty("NgaySinh")]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        [JsonProperty("GioiTinh")]
        public Nullable<bool> GioiTinh { get; set; }
        [JsonProperty("NguyenQuan")]
        public string NguyenQuan { get; set; }
        [JsonProperty("SoCMT")]
        public string SoCMT { get; set; }
        [JsonProperty("NgayCap")]
        public Nullable<System.DateTime> NgayCap { get; set; }
        [JsonProperty("NoiCap")]
        public string NoiCap { get; set; }

        private string _quanHeVoiChuHo;
        [JsonProperty("QuanHeVoiChuHo")]
        public string QuanHeVoiChuHo
        {
            get
            {
                return _quanHeVoiChuHo;
            }
            set
            {
                _quanHeVoiChuHo = value;
                OnPropertyChanged(nameof(QuanHeVoiChuHo));
            }
        }
        [JsonProperty("HoGiaDinhId")]
        public Nullable<int> HoGiaDinhId { get; set; }
        [JsonProperty("SoDienThoai")]
        public string SoDienThoai { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
