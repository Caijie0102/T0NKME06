using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace T0NKME06.Models
{
    public class T0NKME08model
    {
        [Display(Name = "組織Id")]
        public string OrganisationId { get; set; }
        [Display(Name = "組織名稱")]
        public string Organisation { get; set; }
        [Display(Name = "廠處Id")]
        public string SitId { get; set; }
        [Display(Name = "廠處名稱")]
        public string Sit { get; set; }
        [Display(Name = "單元Id")]
        public string UnitId { get; set; }
        [Display(Name = "單元名稱")]
        public string Unit { get; set; }
        [Display(Name = "系統Id")]
        public string SystemId { get; set; }
        [Display(Name = "系統名稱")]
        public string System { get; set; }
        [Display(Name = "資產Id")]
        public string AssetId { get; set; }
        [Display(Name = "資產名稱")]
        public string Asset { get; set; }


        [Display(Name = "風險分析批次ID")]
        public string OrgTreeNodeModelRunId { get; set; }
        [Display(Name = "檢測計劃ID")]
        public string InspectionPlanId { get; set; }
        [Display(Name = "風險分析人員")]
        public string CreatedBy32 { get; set; }
        [Display(Name = "風險分析產生日")]
        public string CreationDate32 { get; set; }
        [Display(Name = "風險分析最後修改人")]
        public string LastModifiedBy32 { get; set; }
        [Display(Name = "風險分析最後修改日")]
        public string LastModifiedDate32 { get; set; }
        [Display(Name = "風險分析名稱")]
        public string Name { get; set; }
        [Display(Name = "風險分析情境")]
        public string ScenarioType { get; set; }
        [Display(Name = "風險分析日期")]
        public string AnalysisDate { get; set; }
        [Display(Name = "批量風險分析ID")]
        public string MassCriticalityRunId { get; set; }
        [Display(Name = "檢測計劃人員")]
        public string CreatedBy09 { get; set; }
        [Display(Name = "檢測計劃產生日期")]
        public string CreationDate09 { get; set; }
        [Display(Name = "檢測計劃狀態")]
        public string InspectionPlanStatus { get; set; }
        [Display(Name = "檢測計劃人員")]
        public string CreatedBy38 { get; set; }
        [Display(Name = "檢測計劃日期")]
        public string CreationDate38 { get; set; }
        [Display(Name = "檢測計劃最後修改人")]
        public string LastModifiedBy38 { get; set; }
        [Display(Name = "檢測計劃最後修改日")]
        public string LastModifiedDate38 { get; set; }
        [Display(Name = "檢測內容")]
        public string TaskExtent { get; set; }
        [Display(Name = "檢測頻率")]
        public string TaskFrequency { get; set; }
        [Display(Name = "檢測頻率單位")]
        public string TaskInterval { get; set; }
        [Display(Name = "檢測方法")]
        public string TaskName { get; set; }
        [Display(Name = "損壞機制")]
        public string TaskReference { get; set; }
        [Display(Name = "檢測優先順序")]
        public string MitigationType { get; set; }
        [Display(Name = "檢測到期日")]
        public string DueDate { get; set; }
        [Display(Name = "自訂檢測到期日註記")]
        public string IsStaticDueDate { get; set; }
        [Display(Name = "上次檢測日")]
        public string HistoricInspectionDate { get; set; }
        [Display(Name = "主要檢測方法ID")]
        public string MasterInspectionMethodId { get; set; }
        [Display(Name = "調整後上次檢測日")]
        public string ManualLastInspectionDate { get; set; }
        [Display(Name = "檢測位置")]
        public string LOCATION { get; set; }
        [Display(Name = "可執行時機")]
        public string AVAILABILITY { get; set; }
        [Display(Name = "建議穿透保溫數量")]
        public string NoBarrPenetrationsRdgs { get; set; }
        [Display(Name = "建議滯留區數量")]
        public string NoDeadLegRdgs { get; set; }
        [Display(Name = "建議保溫損壞數量")]
        public string NoDmgdInsdRdgs { get; set; }
        [Display(Name = "建議彎頭數量")]
        public string NoElbowRdgs { get; set; }
        [Display(Name = "建議沖蝕區數量")]
        public string NoErosionZoneRdgs { get; set; }
        [Display(Name = "建議水平低點數量")]
        public string NoHorizLowPtRdgs { get; set; }
        [Display(Name = "建議保溫端點數量")]
        public string NoInsdTerminatorRdgs { get; set; }
        [Display(Name = "建議水平長管數量")]
        public string NoLongHorizRdgs { get; set; }
        [Display(Name = "建議漸縮管數量")]
        public string NoReducerRdgs { get; set; }
        [Display(Name = "建議土壤-空氣界面數量")]
        public string NoSoilToAirIntfRdgs { get; set; }

        [Display(Name = "建議三通管數量")]
        public string NoTeeRdgs { get; set; }

        [Display(Name = "建議垂直管數量")]
        public string NoVertRunRdgs { get; set; }

    }
}