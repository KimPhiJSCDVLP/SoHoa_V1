//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoHoaApis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class S_ComputerFile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public S_ComputerFile()
        {
            this.S_VanBan = new HashSet<S_VanBan>();
        }
    
        public int ComputerFileID { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiCapNhat { get; set; }
        public Nullable<int> HoSoID { get; set; }
        public Nullable<int> PageNumber { get; set; }
        public Nullable<int> SheetNumber { get; set; }
        public string Size { get; set; }
        public string FolderPath { get; set; }
        public string ClientUrl { get; set; }
        public Nullable<byte> Status { get; set; }
    
        public virtual S_HoSo S_HoSo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<S_VanBan> S_VanBan { get; set; }
    }
}
