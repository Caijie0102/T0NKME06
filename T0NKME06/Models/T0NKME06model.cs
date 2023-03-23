using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace T0NKME06.Models
{
    public class T0NKME06model
    {
         

        public string ModelNodeId { get; set; }
        public string NodeInputId { get; set; }
        public string NodeOutputId { get; set; }
        public string NodeLabel { get; set; }

        public string GroupName { get; set; }

        public string Label { get; set; }
        public string Value { get; set; }
        public string DisplayValue { get; set; }
        public string ProbabilityLabel { get; set; }
        public string ConsequenceLabel { get; set; }
        public string ComponentDetailsId { get; set; }

        public string ProbabilityLabel25 { get; set; }
        public string ConsequenceLabel25 { get; set; }
        public string DisplayValue25 { get; set; }
        public string Value25 { get; set; }




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
        [Display(Name = "風險分析執行日")]
        public string CreationDate { get; set; }
        [Display(Name = "建檔人員")]
        public string CreatedBy { get; set; }
        [Display(Name = "風險分析最後修改日")]
        public string LastModifiedDate { get; set; }
        [Display(Name = "風險分析名稱")]
        public string Name { get; set; }
        [Display(Name = "風險分析情境")]
        public string ScenarioType { get; set; }
        [Display(Name = "風險分析日期")]
        public string AnalysisDate { get; set; }
        [Display(Name = "風險等級")]
        public string OverallRisk { get; set; }
        [Display(Name = "風險分析模型名稱")]
        public string ModelName { get; set; }

        [Display(Name = "夾套註記")]
        public string JacketedFlag { get; set; }

        [Display(Name = "進入內部可能性註記")]
        public string IntEntryPossFlag { get; set; }

        [Display(Name = "注入點註記")]
        public string InjectionPointFlag { get; set; }

        [Display(Name = "混合管註記")]
        public string MixedBoreFlag { get; set; }

        [Display(Name = "小管徑註記")]
        public string SmallBoreFlag { get; set; }

        [Display(Name = "穿透保溫數量")]
        public string NoBarrPenetrations { get; set; }

        [Display(Name = "保溫損壞數量")]
        public string NoDmgdInsd { get; set; }

        [Display(Name = "滯留區數量")]
        public string NoDeadLegs { get; set; }

        [Display(Name = "彎頭數量")]
        public string NoElbows { get; set; }

        [Display(Name = "沖蝕區數量")]
        public string NoErosionZones { get; set; }

        [Display(Name = "水平低點數量")]
        public string NoHorizLowPts { get; set; }

        [Display(Name = "保溫端點數量")]
        public string NoInsdTerminators { get; set; }


        [Display(Name = "水平長管數量")]
        public string NoLongHorizRuns { get; set; }

        [Display(Name = "漸縮管數量")]
        public string NoReducers { get; set; }

        [Display(Name = "土壤-空氣界面數量")]
        public string NoSoilToAirIntfs { get; set; }

        [Display(Name = "三通管數量")]
        public string NoTees { get; set; }

        [Display(Name = "垂直管數量")]
        public string NoVertRuns { get; set; }

        [Display(Name = "偵測時間")]
        public string DetectionTime { get; set; }

        [Display(Name = "隔離時間")]
        public string IsolationTime { get; set; }

        [Display(Name = "防溢堤面積")]
        public double DikeArea { get; set; }

        [Display(Name = "防溢堤註記")]
        public string DikedFlag { get; set; }

        [Display(Name = "內容物重量")]
        public double Inventory { get; set; }

        [Display(Name = "操作壓力")]
        public double OperatingPressure { get; set; }

        [Display(Name = "操作溫度")]
        public double OperatingTemp { get; set; }

        [Display(Name = "代表厚度")]
        public double RepThick { get; set; }

        [Display(Name = "代表性流體")]
        public string RepFluid { get; set; }

        [Display(Name = "生產損失")]
        public string ProductionLoss { get; set; }

        [Display(Name = "毒性濃度")]
        public string PercentToxic { get; set; }

        [Display(Name = "毒性流體")]
        public string ToxicFluid { get; set; }

        [Display(Name = "毒性流體註記")]
        public string ToxicMixtureFlag { get; set; }


        [Display(Name = "環境裂痕檢測有效性")]
        public string EnvCrckgInspConf { get; set; }

        [Display(Name = "環境裂痕上次檢測日")]
        public string EnvCrckgLastInspDate { get; set; }

        [Display(Name = "環境裂痕檢測次數")]
        public string EnvCrckgNoOfInsp { get; set; }

        [Display(Name = "環境裂痕使用日期")]
        public string EnvCrckgServDate { get; set; }

        [Display(Name = "環境裂痕機制")]
        public string EnvCrckgMech { get; set; }



        [Display(Name = "地區濕度")]
        public string Humidity { get; set; }

        [Display(Name = "外部濕潤註記")]
        public string ExtWettingFlag { get; set; }

        [Display(Name = "外部腐蝕率來源")]
        public string ExtCorrosionOption { get; set; }

        [Display(Name = "外部預期腐蝕率")]
        public double ExtExpecedCorrRate { get; set; }

        [Display(Name = "外部量測腐蝕率")]
        public double ExtMeasuredCorrRate { get; set; }

        [Display(Name = "外部計算腐蝕率")]
        public double ExtCalcCorrRate { get; set; }
        public string ExtCalcCorrRate1 { get; set; }

        [Display(Name = "外部開始使用日")]
        public string ExtServDate { get; set; }

        [Display(Name = "操作溫度")]
        public double OperatingTemperature { get; set; }

        [Display(Name = "CUI註記")]
        public string CUIFlag { get; set; }

        [Display(Name = "外部檢測有效性")]
        public string ExtInspConf { get; set; }

        [Display(Name = "外部上次檢測日")]
        public string ExtLastInspDate { get; set; }


        [Display(Name = "外部檢測次數")]
        public string ExtNoOfInsp { get; set; }


        [Display(Name = "外部塗層")]
        public string ExtCoating { get; set; }

        [Display(Name = "保溫註記")]
        public string InsulatedFlag { get; set; }

        [Display(Name = "保溫狀況")]
        public string InsulationCondition { get; set; }

        [Display(Name = "保溫材料")]
        public string InsulationType { get; set; }

        [Display(Name = "自燃溫度")]
        public double AIT { get; set; }

        [Display(Name = "沸點")]
        public double BoilingPoint { get; set; }

        [Display(Name = "流體類型")]
        public string FluidType { get; set; }

        [Display(Name = "毒性沸點")]
        public double ToxicBP { get; set; }

        [Display(Name = "內部腐蝕率類型")]
        public string IntCorrosionType { get; set; }

        [Display(Name = "內部腐蝕率來源")]
        public string IntCorrosionOption { get; set; }

        [Display(Name = "內部預期腐蝕率")]
        public double IntExpectedCorrRate { get; set; }


        [Display(Name = "內部長期腐蝕率")]
        public double IntLTCorrRate { get; set; }

        [Display(Name = "內部短期腐蝕率")]
        public double IntSTCorrRate { get; set; }

        [Display(Name = "組件型式")]
        public string CompType { get; set; }

        [Display(Name = "設計壓力")]
        public double DesignPressure { get; set; }

        [Display(Name = "設計溫度")]
        public double DesignTemp { get; set; }

        [Display(Name = "直徑")]
        public double Diameter { get; set; }

        [Display(Name = "內部開始使用日")]
        public string IntServDate { get; set; }

        [Display(Name = "覆蓋管線外徑")]
        public double ODOverride { get; set; }

        [Display(Name = "覆蓋管線外徑註記")]
        public string ODOverrideFlag { get; set; }

        //[Display(Name = "風險分析日期")]
        //public string AnalysisDate { get; set; }

        [Display(Name = "內部檢測有效性")]
        public string IntInpsConf { get; set; }

        [Display(Name = "內部上次檢測日")]
        public string IntLastInspDate { get; set; }

        [Display(Name = "內部檢測次數")]
        public string IntNoOfInps { get; set; }

        [Display(Name = "建造規範")]
        public string ConstCode { get; set; }

        [Display(Name = "銲接係數")]
        public string JointEfficiency  { get; set; }

        [Display(Name = "自訂容許應力")]
        public double OverideAllowableStress { get; set; }

        [Display(Name = "自訂容許應力註記")]
        public string OverideAllowableStressFlag { get; set; }

        [Display(Name = "自訂估計最小厚度註記")]
        public string EstMinThicknessFlag { get; set; }

        [Display(Name = "估計最小厚度")]
        public double EstMinThickness { get; set; }

        [Display(Name = "內部估計剩餘壁厚")]
        public double IntEstWallRemain { get; set; }

        [Display(Name = "估計半壽命")]
        public string EstHalfLife { get; set; }

        [Display(Name = "其他損壞機制1")]
        public string ODM1 { get; set; }

        [Display(Name = "其他損壞機制1等級")]
        public string ODM1Potential { get; set; }

        [Display(Name = "其他損壞機制1可能性")]
        public string ODM1Probability { get; set; }

        [Display(Name = "其他損壞機制1說明")]
        public string ODM1Comment { get; set; }

        [Display(Name = "其他損壞機制2")]
        public string ODM2 { get; set; }

        [Display(Name = "其他損壞機制2等級")]
        public string ODM2Potential { get; set; }

        [Display(Name = "其他損壞機制2可能性")]
        public string ODM2Probability { get; set; }

        [Display(Name = "其他損壞機制2說明")]
        public string ODM2Comment { get; set; }

        [Display(Name = "其他損壞機制3")]
        public string ODM3 { get; set; }

        [Display(Name = "其他損壞機制3等級")]
        public string ODM3Potential { get; set; }

        [Display(Name = "其他損壞機制3可能性")]
        public string ODM3Probability { get; set; }

        [Display(Name = "其他損壞機制3說明")]
        public string ODM3Comment { get; set; }

        [Display(Name = "其他損壞機制4")]
        public string ODM4 { get; set; }

        [Display(Name = "其他損壞機制4等級")]
        public string ODM4Potential { get; set; }

        [Display(Name = "其他損壞機制4可能性")]
        public string ODM4Probability { get; set; }

        [Display(Name = "其他損壞機制4說明")]
        public string ODM4Comment { get; set; }

        [Display(Name = "其他損壞機制5")]
        public string ODM5 { get; set; }

        [Display(Name = "其他損壞機制5等級")]
        public string ODM5Potential { get; set; }

        [Display(Name = "其他損壞機制5可能性")]
        public string ODM5Probability { get; set; }

        [Display(Name = "其他損壞機制5說明")]
        public string ODM5Comment { get; set; }





    }
}