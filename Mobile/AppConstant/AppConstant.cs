using System;

namespace AppConstant
{
    public class AppConstant
    {
        public const string Endpoint = "http://192.168.43.114:8800";
        //
        public const string APIThongBao = "/odata/ThongBaoChung";
        public const string APIUserInfo = "/common/common";
        public const string APINhanKhau = "/odata/NhanKhau";
        public const string APIPhieuThu = "/odata/PhieuThu";
        public const string APIPhieuThuExport = "/ExportEX/ExportFile";
        public const string APIGetImage = "/fileupload/download?key=";
        public const string APIInsertOrUpdateNhanKhau = "api/nhankhau/insertorupdatenhankhau";
        public const string APIDeleteNhanKhau = "api/nhankhau/deletenhankhau";
        public const string APIUploadImage = "api/img";
        public const string APIUpdateHoGiaDinhAvatar = "api/nhankhau/updateavatar";
    }
}
