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
    
    public partial class NodeInputMetadata
    {
        public System.Guid NodeInputId { get; set; }
        public int DataType { get; set; }
        public string DefaultValue { get; set; }
        public string Identifier { get; set; }
        public string Label { get; set; }
        public System.Guid ModelNodeId { get; set; }
        public System.Guid NodeId { get; set; }
        public bool Required { get; set; }
        public int Source { get; set; }
        public System.Guid SourceLookupId { get; set; }
        public string SourceMapping { get; set; }
        public System.Guid UnitOfMeasureId { get; set; }
        public string GroupName { get; set; }
        public int RiskCategoryType { get; set; }
    
        public virtual ModelNodeMetadata ModelNodeMetadata { get; set; }
    }
}
