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
    
    public partial class InspectionPlanTaskMitigationColumns
    {
        public System.Guid InspectionPlanTaskMitigationColumnId { get; set; }
        public System.Guid InspectionPlanTaskId { get; set; }
        public System.Guid MitigationColumnId { get; set; }
        public string CellText { get; set; }
    
        public virtual InspectionPlanTasks InspectionPlanTasks { get; set; }
        public virtual MitigationColumns MitigationColumns { get; set; }
    }
}
