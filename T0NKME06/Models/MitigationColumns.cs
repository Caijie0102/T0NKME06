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
    
    public partial class MitigationColumns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MitigationColumns()
        {
            this.InspectionPlanTaskMitigationColumns = new HashSet<InspectionPlanTaskMitigationColumns>();
        }
    
        public System.Guid MitigationColumnId { get; set; }
        public int ColumnIndex { get; set; }
        public string HeaderText { get; set; }
        public bool IsDisplayed { get; set; }
        public System.Guid MitigationsId { get; set; }
        public Nullable<System.Guid> FilterTagMitigationFilterTagId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InspectionPlanTaskMitigationColumns> InspectionPlanTaskMitigationColumns { get; set; }
    }
}
