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
    
    public partial class S_Uers_Atuthority
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public S_Uers_Atuthority()
        {
            this.S_Page = new HashSet<S_Page>();
        }
    
        public int SystemID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> AuthorityID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
    
        public virtual S_Authority S_Authority { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<S_Page> S_Page { get; set; }
        public virtual S_Users S_Users { get; set; }
    }
}
