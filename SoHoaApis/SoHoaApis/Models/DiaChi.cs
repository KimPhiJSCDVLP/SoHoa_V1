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
    
    public partial class DiaChi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiaChi()
        {
            this.S_CoQuan = new HashSet<S_CoQuan>();
        }
    
        public int DiaChiID { get; set; }
        public Nullable<int> TinhID { get; set; }
        public Nullable<int> HuyenID { get; set; }
        public Nullable<int> XaPhuongID { get; set; }
        public string DiaChiChiTiet { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<S_CoQuan> S_CoQuan { get; set; }
    }
}
