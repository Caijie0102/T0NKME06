//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace T0NKME06.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HierarchyTemplates
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HierarchyTemplates()
        {
            this.ComponentDetails = new HashSet<ComponentDetails>();
        }
    
        public System.Guid HierarchyTemplateId { get; set; }
        public Nullable<System.Guid> AssetFamilyId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsPiping { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime SysStartTime { get; set; }
        public System.DateTime SysEndTime { get; set; }
        public Nullable<System.Guid> AuditEventItemId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComponentDetails> ComponentDetails { get; set; }
    }
}
