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
    
    public partial class RunnableModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RunnableModel()
        {
            this.ModelNodeMetadata = new HashSet<ModelNodeMetadata>();
            this.OrgTreeNodeModelRuns = new HashSet<OrgTreeNodeModelRuns>();
        }
    
        public string Description { get; set; }
        public System.Guid ModelId { get; set; }
        public string ModelName { get; set; }
        public string PublishedBy { get; set; }
        public System.DateTime PublishedDate { get; set; }
        public int Status { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.Guid> CustomerId { get; set; }
        public Nullable<System.Guid> DisplayMethodId { get; set; }
        public Nullable<System.Guid> IndustryId { get; set; }
        public Nullable<System.Guid> IntendedTargetId { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
        public Nullable<System.Guid> AssessmentMethodId { get; set; }
        public string ValidatedBy { get; set; }
        public System.DateTime ValidatedDate { get; set; }
        public Nullable<System.Guid> RiskMatrixId { get; set; }
        public Nullable<System.Guid> EquipmentClassId { get; set; }
        public Nullable<System.Guid> EquipmentComponentId { get; set; }
        public Nullable<System.Guid> EquipmentPartId { get; set; }
        public Nullable<System.Guid> EquipmentSubUnitId { get; set; }
        public string Discriminator { get; set; }
        public bool RiskMatrixFrlEnabled { get; set; }
        public Nullable<System.Guid> EquipmentClassTypeId { get; set; }
        public Nullable<System.Guid> EquipmentLibraryId { get; set; }
        public string LastModifiedBy { get; set; }
        public double MaxInspectionFrequencyForFrl { get; set; }
        public double MaxRemainingLifeForFrl { get; set; }
        public bool UseFrlMostConservativeValue { get; set; }
        public string SourceModel { get; set; }
        public Nullable<System.Guid> SourceModelId { get; set; }
        public bool UseFrlFrequencySourceDetection { get; set; }
        public int RunOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModelNodeMetadata> ModelNodeMetadata { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrgTreeNodeModelRuns> OrgTreeNodeModelRuns { get; set; }
    }
}
