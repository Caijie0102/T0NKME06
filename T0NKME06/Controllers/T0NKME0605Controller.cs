using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;
using T0NKME06.Models;
using T0NKME06.Extensions;

namespace T0NKME06.Controllers
{
    public class T0NKME0605Controller : ApiController
    {
        // GET: T0NKME0605
        public IHttpActionResult Get()
        {
            List<T0NKME06model> DataList_T0NKME06model = new List<T0NKME06model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();

            try
            {
                using (Entities2 db = new Entities2())
                {
                    var getAllComponent = from otmr in db.OrgTreeNodeModelRuns  //32
                                          join otmri in db.OrgTreeNodeModelRunInputs on otmr.OrgTreeNodeModelRunId equals otmri.OrgTreeNodeModelRunId  //24
                                          join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId //25
                                          join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId
                                          select new
                                          {
                                              OrgTreeNodeModelRunId = otmr.OrgTreeNodeModelRunId,
                                              CreationDate = otmr.CreationDate,
                                              CreatedBy = otmr.CreatedBy,
                                              LastModifiedDate = otmr.LastModifiedDate,
                                              Name = otmr.Name,
                                              ScenarioType = otmr.ScenarioType,
                                              AnalysisDate = otmr.AnalysisDate,

                                              //36
                                              OverallRisk= (from dim in db.DimModelRunOverallRisk
                                                            where otmr.OrgTreeNodeModelRunId.ToString() == dim.OrgTreeNodeModelRunId.ToString()
                                                            select dim.OverallRisk).FirstOrDefault(),
                                              //37
                                              ModelName= (from rm in db.RunnableModel
                                                          where otmr.ModelId.ToString() == rm.ModelId.ToString()
                                                          select rm.ModelName).FirstOrDefault(),

                                              OrgTreeNodeModelRunId32 = otmr.OrgTreeNodeModelRunId.ToString(),
                                              NodeInputId = otmri.NodeInputId.ToString(),  //24
                                              NodeOutputId = otmro.NodeOutputId.ToString(),  //25

                                              ModelNodeId2429 = otmri.ModelNodeId.ToString(),
                                              ModelNodeId2529 = otmro.ModelNodeId.ToString(),


                                              NodeInputId31= (from nim in db.NodeInputMetadata
                                                              where otmri.NodeInputId.ToString() == nim.NodeInputId.ToString()
                                                              select nim.NodeInputId).FirstOrDefault(),


                                              NodeOutputId30 = (from nom in db.NodeOutputMetadata
                                                               where otmro.NodeOutputId.ToString() == nom.NodeOutputId.ToString()
                                                               select nom.NodeOutputId).FirstOrDefault(),

                                            
                                              Value24 = otmri.Value,
                                              Value25 = otmro.Value,
                                    
                                              OrgTreeNodeId32 = otmr.OrgTreeNodeId.ToString(), //otmr與otn連接的
                                              ParentId = otn.ParentId.ToString(),   //otn的

                                              ExtCalcCorrRate= (from nomd in db.NodeOutputMetadata
                                                                join otmro in db.OrgTreeNodeModelRunOutputs on nomd.NodeOutputId equals otmro.NodeOutputId
                                                                join mnm in db.ModelNodeMetadata on otmro.ModelNodeId equals mnm.ModelNodeId
                                                                where nomd.GroupName.ToString() == "Corrosion Information" && nomd.Label.ToString() == "Calculated Corrosion Rate" && mnm.NodeLabel== "EXTERNAL CORROSION"
                                                                select otmro.Value).FirstOrDefault(),
                                                                //29 30
                                              //ComponentDetailsId = cd.ComponentDetailsId.ToString(),

                                              //OrgTreeNodeModelRunId= otmr.OrgTreeNodeModelRunInputs.ToString(),說是不能轉換成字串

                                              // ModelId= otmr.ModelId.ToString(),


                                          };
                    //return Ok(getAllComponent.Take(100).ToList());

                    

                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var W0NKME26 = db.RunnableLookupRowDisplayValues.ToList();
                    var W0NKME27 = db.RiskMatrixProbabilityVectors.ToList();
                    var W0NKME28 = db.RiskMatrixConsequenceVectors.ToList();
             

                    var fake = getAllComponent
                  //.AsEnumerable()
                  .Take(100);
                    //.ToList(); // 将 LINQ 对象转换为列表对象

                    Parallel.ForEach(fake, item =>
                    {
                        //在這個迴圈中，程式碼使用Where方法從"W0NKME03"集合中過濾出元件（Components），以便獲取該元件的詳細資料，例如組織、位置、系統、設備、元件ID等。使用Select方法從過濾後的元件集合中，選擇需要的屬性來建立一個匿名物件，然後使用FirstOrDefault方法來取得第一個符合條件的元素，如果沒有符合條件的元素，則返回null。
                        var getComponent = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == item.OrgTreeNodeId32.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();
                        if (getComponent != null)  //"x"則是指向"W0NKME03"集合的當前元素。
                        {
                            var getAssetitem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == getComponent.ParentId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();
                            if (getAssetitem != null)
                            {
                                var getSystemitem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == getAssetitem.ParentId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name }).FirstOrDefault();
                                if (getSystemitem != null)
                                {
                                    var getUnititem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == getSystemitem.ParentId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name }).FirstOrDefault();
                                    if (getUnititem != null)
                                    {
                                        var getSititem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == getUnititem.ParentId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name }).FirstOrDefault();
                                        if (getSititem != null)
                                        {
                                            var getOrganisationitem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == getSititem.ParentId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name }).FirstOrDefault();
                                            if (getOrganisationitem != null)
                                            {
                                                lock (lockMe)
                                                {
                                                    DataList_T0NKME06model.Add(new T0NKME06model()
                                                    {

                                                        OrganisationId = getOrganisationitem.OrgTreeNodeId.ToString(),
                                                        Organisation = getOrganisationitem.Name,
                                                        SitId = getSititem.OrgTreeNodeId.ToString(),
                                                        Sit = getSititem.Name,
                                                        UnitId = getUnititem.OrgTreeNodeId.ToString(),
                                                        Unit = getUnititem.Name,
                                                        SystemId = getSystemitem.OrgTreeNodeId.ToString(),
                                                        System = getSystemitem.Name,
                                                        AssetId = getAssetitem.OrgTreeNodeId.ToString(),
                                                        Asset = getAssetitem.Name,

                                                        ModelNodeId = item.ModelNodeId2429,
                                                        NodeInputId = item.NodeInputId,
                                                        NodeOutputId = item.NodeOutputId,


                                                        Value = item.Value24,
                                                        Value25 = item.Value25,

                                                        OrgTreeNodeModelRunId = item.OrgTreeNodeModelRunId.ToString(),
                                                        CreationDate = item.CreationDate.ToString(),
                                                        CreatedBy = item.CreatedBy,
                                                        LastModifiedDate = item.LastModifiedDate.ToString(),
                                                        Name = item.Name,
                                                        ScenarioType = item.ScenarioType.ToString(),
                                                        AnalysisDate = item.AnalysisDate.ToString(),

                                                        //36
                                                        OverallRisk = item.OverallRisk,
                                                        //37
                                                        ModelName = item.ModelName,

                                                        //ExtCalcCorrRate1=item.ExtCalcCorrRate




                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });
                    string JacketedFlag = "", IntEntryPossFlag = "", InjectionPointFlag = " ", MixedBoreFlag = " ", SmallBoreFlag = " ", NoBarrPenetrations = " ", NoDmgdInsd = " ", NoDeadLegs = " ", NoElbows = " ", NoErosionZones = " ", NoHorizLowPts = " ", NoInsdTerminators = " ", NoLongHorizRuns = " ", NoReducers = " ", NoSoilToAirIntfs = " ", NoTees = " ", NoVertRuns = " ",
                    DetectionTime = " ", IsolationTime = " ", DikedFlag = " ", RepFluid = " ", ProductionLoss = " ", PercentToxic = " ", ToxicFluid = " ", ToxicMixtureFlag = " ",
                    EnvCrckgInspConf = " ", EnvCrckgLastInspDate = " ", EnvCrckgNoOfInsp = " ", EnvCrckgServDate = " ", EnvCrckgMech = " ",
                    Humidity = " ", ExtWettingFlag = " ", ExtCorrosionOption = " ", ExtServDate = " ",  CUIFlag = " ", ExtInspConf = " ", ExtLastInspDate = " ", ExtNoOfInsp = " ", ExtCoating = " ", InsulatedFlag = " ", InsulationCondition = " ", InsulationType = " ",
                     FluidType = " ", 
                    IntCorrosionType = " ", IntCorrosionOption = " ", CompType = " ", IntServDate = " ", ODOverrideFlag = " ", AnalysisDate = " ", IntInpsConf = " ", IntLastInspDate = " ", IntNoOfInps = " ", ConstCode = " ", JointEfficiency = " ", OverideAllowableStressFlag = " ", EstMinThicknessFlag = " ",
                    ODM1 = " ", ODM2 = " ", ODM3 = " ", ODM4 = " ", ODM5 = " ", ODM1Potential = " ", ODM2Potential = " ", ODM3Potential = " ", ODM4Potential = " ", ODM5Potential = " ", ODM1Probability = " ", ODM2Probability = " ", ODM3Probability = " ", ODM4Probability = " ", ODM5Probability = " ", ODM1Comment = " ", ODM2Comment = " ", ODM3Comment = " ", ODM4Comment = " ", ODM5Comment = " ", EstHalfLife = " ";

                    double DikeArea = 0, Inventory = 0, OperatingPressure = 0, OperatingTemp = 0, RepThick = 0, ExtExpecedCorrRate = 0, ExtMeasuredCorrRate = 0, IntExpectedCorrRate = 0, IntLTCorrRate = 0, IntSTCorrRate = 0, DesignPressure = 0, DesignTemp = 0, Diameter = 0, ODOverride = 0, OverideAllowableStress = 0, ExtCalcCorrRate = 0, EstMinThickness = 0, IntEstWallRemain = 0, OperatingTemperature = 0,
                        AIT = 0, BoilingPoint = 0, ToxicBP = 0;
                    //Double DikeArea;

                    foreach (var need24 in DataList_T0NKME06model)
                    {   //31
                        var get31 = db.NodeInputMetadata.Where(x => x.NodeInputId.ToString() == need24.NodeInputId.ToString()).Select(x => new { x.GroupName, x.Label }).ToList();
                        var get30 = db.NodeOutputMetadata.Where(x => x.NodeOutputId.ToString() == need24.NodeOutputId.ToString()).Select(x => new { x.GroupName, x.Label }).ToList();

                        var DisplayValue = W0NKME26.Where(x => x.LookupRowId.ToString() == need24.Value.ToString()).Select(x => new { x.DisplayValue }).FirstOrDefault();
                        var ProbabilityLabel = W0NKME27.Where(x => x.Index.ToString() == need24.Value.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        var ConsequenceLabel = W0NKME28.Where(x => x.Index.ToString() == need24.Value.ToString()).Select(x => new { x.Label }).FirstOrDefault();


                        var DisplayValue25 = W0NKME26.Where(x => x.LookupRowId.ToString() == need24.Value25.ToString()).Select(x => new { x.DisplayValue }).FirstOrDefault();
                        var ProbabilityLabel25 = W0NKME27.Where(x => !need24.Value25.IsEmpty() && x.Index.ToString() == need24.Value25.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        var ConsequenceLabel25 = W0NKME28.Where(x => !need24.Value25.IsEmpty() && x.Index.ToString() == need24.Value25.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        //!x.GroupName.IsEmpty() &&
                        switch (db.ModelNodeMetadata.Where(x => x.ModelNodeId.ToString() == need24.ModelNodeId.ToString()).FirstOrDefault().NodeLabel) //24==29
                        {


                            case "ADDITIONAL INFORMATION":
                                var getJacketedFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString().Contains("Jacket")).FirstOrDefault();//其實應該要24
                                if (getJacketedFlag != null && DisplayValue != null)
                                {
                                    JacketedFlag = DisplayValue.DisplayValue;
                                }
                                else if (getJacketedFlag != null && ProbabilityLabel != null)
                                {
                                    JacketedFlag = ProbabilityLabel.Label;
                                }
                                else if (getJacketedFlag != null && ConsequenceLabel != null)
                                {
                                    JacketedFlag = ConsequenceLabel.Label;
                                }
                                else if (getJacketedFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    JacketedFlag = need24.Value;
                                }
                                else
                                {
                                    var getIntEntryPossFlag = get31.Where(x => x.GroupName.ToString() == "Column Locations"
                                || x.GroupName.ToString() == "Exchanger Locations" || x.GroupName.ToString() == "Pipe Locations"
                                                                     || x.GroupName.ToString() == "Tank Locations" || x.GroupName.ToString() == "Vessel Locations"
                                                                     && x.Label.ToString() == "Internal Entry Possible").FirstOrDefault();//其實應該要24
                                    if (getIntEntryPossFlag != null && DisplayValue != null)
                                    {
                                        IntEntryPossFlag = DisplayValue.DisplayValue;
                                    }
                                    else if (getIntEntryPossFlag != null && ProbabilityLabel != null)
                                    {
                                        IntEntryPossFlag = ProbabilityLabel.Label;
                                    }
                                    else if (getIntEntryPossFlag != null && ConsequenceLabel != null)
                                    {
                                        IntEntryPossFlag = ConsequenceLabel.Label;
                                    }
                                    else if (getIntEntryPossFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        IntEntryPossFlag = need24.Value;
                                    }
                                    else
                                    {

                                        var getInjectionPointFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString() == "Injection Point?").FirstOrDefault();//其實應該要24
                                        if (getInjectionPointFlag != null && DisplayValue != null)
                                        {
                                            InjectionPointFlag = DisplayValue.DisplayValue;
                                        }
                                        else if (getInjectionPointFlag != null && ProbabilityLabel != null)
                                        {
                                            InjectionPointFlag = ProbabilityLabel.Label;
                                        }
                                        else if (getInjectionPointFlag != null && ConsequenceLabel != null)
                                        {
                                            InjectionPointFlag = ConsequenceLabel.Label;
                                        }
                                        else if (getInjectionPointFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            InjectionPointFlag = need24.Value;
                                        }
                                        else
                                        {
                                            var getMixedBoreFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString() == "Mixed Bore?").FirstOrDefault();//其實應該要24
                                            if (getMixedBoreFlag != null && DisplayValue != null)
                                            {
                                                MixedBoreFlag = DisplayValue.DisplayValue;
                                            }
                                            else if (getMixedBoreFlag != null && ProbabilityLabel != null)
                                            {
                                                MixedBoreFlag = ProbabilityLabel.Label;
                                            }
                                            else if (getMixedBoreFlag != null && ConsequenceLabel != null)
                                            {
                                                MixedBoreFlag = ConsequenceLabel.Label;
                                            }
                                            else if (getMixedBoreFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                MixedBoreFlag = need24.Value;
                                            }
                                            else
                                            {
                                                var getSmallBoreFlag = get31.Where(x => x.GroupName.ToString() == "Other Data"
                                                                                 && x.Label.ToString() == "Small Bore?").FirstOrDefault();
                                                if (getSmallBoreFlag != null && DisplayValue != null)
                                                {
                                                    SmallBoreFlag = DisplayValue.DisplayValue;
                                                }
                                                else if (getSmallBoreFlag != null && ProbabilityLabel != null)
                                                {
                                                    SmallBoreFlag = ProbabilityLabel.Label;
                                                }
                                                else if (getSmallBoreFlag != null && ConsequenceLabel != null)
                                                {
                                                    SmallBoreFlag = ConsequenceLabel.Label;
                                                }
                                                else if (getSmallBoreFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    SmallBoreFlag = need24.Value;
                                                }
                                                else
                                                {
                                                    var getNoBarrPenetrations = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                 && x.Label.ToString() == "Number of Barrier Penetrations").FirstOrDefault();//其實應該要24
                                                    if (getNoBarrPenetrations != null && DisplayValue != null)
                                                    {
                                                        NoBarrPenetrations = DisplayValue.DisplayValue;
                                                    }
                                                    else if (getNoBarrPenetrations != null && ProbabilityLabel != null)
                                                    {
                                                        NoBarrPenetrations = ProbabilityLabel.Label;
                                                    }
                                                    else if (getNoBarrPenetrations != null && ConsequenceLabel != null)
                                                    {
                                                        NoBarrPenetrations = ConsequenceLabel.Label;
                                                    }
                                                    else if (getNoBarrPenetrations != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                    {
                                                        NoBarrPenetrations = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getNoDmgdInsd = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                 && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                        if (getNoDmgdInsd != null && DisplayValue != null)
                                                        {
                                                            NoDmgdInsd = DisplayValue.DisplayValue;
                                                        }
                                                        else if (getNoDmgdInsd != null && ProbabilityLabel != null)
                                                        {
                                                            NoDmgdInsd = ProbabilityLabel.Label;
                                                        }
                                                        else if (getNoDmgdInsd != null && ConsequenceLabel != null)
                                                        {
                                                            NoDmgdInsd = ConsequenceLabel.Label;
                                                        }
                                                        else if (getNoDmgdInsd != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                        {
                                                            NoDmgdInsd = need24.Value;
                                                        }
                                                        else
                                                        {
                                                            var getNoDeadLegs = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                 && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                            if (getNoDeadLegs != null && DisplayValue != null)
                                                            {
                                                                NoDeadLegs = DisplayValue.DisplayValue;
                                                            }
                                                            else if (getNoDeadLegs != null && ProbabilityLabel != null)
                                                            {
                                                                NoDeadLegs = ProbabilityLabel.Label;
                                                            }
                                                            else if (getNoDeadLegs != null && ConsequenceLabel != null)
                                                            {
                                                                NoDeadLegs = ConsequenceLabel.Label;
                                                            }
                                                            else if (getNoDeadLegs != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                            {
                                                                NoDeadLegs = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getNoElbows = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                 && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                                if (getNoElbows != null && DisplayValue != null)
                                                                {
                                                                    NoElbows = DisplayValue.DisplayValue;
                                                                }
                                                                else if (getNoElbows != null && ProbabilityLabel != null)
                                                                {
                                                                    NoElbows = ProbabilityLabel.Label;
                                                                }
                                                                else if (getNoElbows != null && ConsequenceLabel != null)
                                                                {
                                                                    NoElbows = ConsequenceLabel.Label;
                                                                }
                                                                else if (getNoElbows != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                {
                                                                    NoElbows = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getNoErosionZones = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Erosion Zones").FirstOrDefault();//其實應該要24
                                                                    if (getNoErosionZones != null && DisplayValue != null)
                                                                    {
                                                                        NoErosionZones = DisplayValue.DisplayValue;
                                                                    }
                                                                    else if (getNoErosionZones != null && ProbabilityLabel != null)
                                                                    {
                                                                        NoErosionZones = ProbabilityLabel.Label;
                                                                    }
                                                                    else if (getNoErosionZones != null && ConsequenceLabel != null)
                                                                    {
                                                                        NoErosionZones = ConsequenceLabel.Label;
                                                                    }
                                                                    else if (getNoErosionZones != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                    {
                                                                        NoErosionZones = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getNoHorizLowPts = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Horizontal Low Points").FirstOrDefault();//其實應該要24
                                                                        if (getNoHorizLowPts != null && DisplayValue != null)
                                                                        {
                                                                            NoHorizLowPts = DisplayValue.DisplayValue;
                                                                        }
                                                                        else if (getNoHorizLowPts != null && ProbabilityLabel != null)
                                                                        {
                                                                            NoHorizLowPts = ProbabilityLabel.Label;
                                                                        }
                                                                        else if (getNoHorizLowPts != null && ConsequenceLabel != null)
                                                                        {
                                                                            NoHorizLowPts = ConsequenceLabel.Label;
                                                                        }
                                                                        else if (getNoHorizLowPts != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            NoHorizLowPts = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getNoInsdTerminators = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Insulation Terminations").FirstOrDefault();//其實應該要24
                                                                            if (getNoInsdTerminators != null && DisplayValue != null)
                                                                            {
                                                                                NoInsdTerminators = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getNoInsdTerminators != null && ProbabilityLabel != null)
                                                                            {
                                                                                NoInsdTerminators = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getNoInsdTerminators != null && ConsequenceLabel != null)
                                                                            {
                                                                                NoInsdTerminators = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getNoInsdTerminators != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                            {
                                                                                NoInsdTerminators = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getNoLongHorizRuns = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Long Horizontal Runs").FirstOrDefault();//其實應該要24
                                                                                if (getNoLongHorizRuns != null && DisplayValue != null)
                                                                                {
                                                                                    NoLongHorizRuns = DisplayValue.DisplayValue;
                                                                                }
                                                                                else if (getNoLongHorizRuns != null && ProbabilityLabel != null)
                                                                                {
                                                                                    NoLongHorizRuns = ProbabilityLabel.Label;
                                                                                }
                                                                                else if (getNoLongHorizRuns != null && ConsequenceLabel != null)
                                                                                {
                                                                                    NoLongHorizRuns = ConsequenceLabel.Label;
                                                                                }
                                                                                else if (getNoLongHorizRuns != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                {
                                                                                    NoLongHorizRuns = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getNoReducers = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Reducers").FirstOrDefault();//其實應該要24
                                                                                    if (getNoReducers != null && DisplayValue != null)
                                                                                    {
                                                                                        NoReducers = DisplayValue.DisplayValue;
                                                                                    }
                                                                                    else if (getNoReducers != null && ProbabilityLabel != null)
                                                                                    {
                                                                                        NoReducers = ProbabilityLabel.Label;
                                                                                    }
                                                                                    else if (getNoReducers != null && ConsequenceLabel != null)
                                                                                    {
                                                                                        NoReducers = ConsequenceLabel.Label;
                                                                                    }
                                                                                    else if (getNoReducers != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                    {
                                                                                        NoReducers = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getNoSoilToAirIntfs = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Soil-Air Interface").FirstOrDefault();//其實應該要24
                                                                                        if (getNoSoilToAirIntfs != null && DisplayValue != null)
                                                                                        {
                                                                                            NoSoilToAirIntfs = DisplayValue.DisplayValue;
                                                                                        }
                                                                                        else if (getNoSoilToAirIntfs != null && ProbabilityLabel != null)
                                                                                        {
                                                                                            NoSoilToAirIntfs = ProbabilityLabel.Label;
                                                                                        }
                                                                                        else if (getNoSoilToAirIntfs != null && ConsequenceLabel != null)
                                                                                        {
                                                                                            NoSoilToAirIntfs = ConsequenceLabel.Label;
                                                                                        }
                                                                                        else if (getNoSoilToAirIntfs != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                        {
                                                                                            NoSoilToAirIntfs = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getNoTees = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Tees").FirstOrDefault();//其實應該要24
                                                                                            if (getNoTees != null && need24.DisplayValue != null)
                                                                                            {
                                                                                                NoTees = DisplayValue.DisplayValue;
                                                                                            }
                                                                                            else if (getNoTees != null && ProbabilityLabel != null)
                                                                                            {
                                                                                                NoTees = ProbabilityLabel.Label;
                                                                                            }
                                                                                            else if (getNoTees != null && ConsequenceLabel != null)
                                                                                            {
                                                                                                NoTees = ConsequenceLabel.Label;
                                                                                            }
                                                                                            else if (getNoTees != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                            {
                                                                                                NoTees = need24.Value;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                var getNoVertRuns = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                     && x.Label.ToString() == "Number of Vertical Runs").FirstOrDefault();//其實應該要24
                                                                                                if (getNoVertRuns != null && DisplayValue != null)
                                                                                                {
                                                                                                    NoVertRuns = DisplayValue.DisplayValue;
                                                                                                }
                                                                                                else if (getNoVertRuns != null && ProbabilityLabel != null)
                                                                                                {
                                                                                                    NoVertRuns = ProbabilityLabel.Label;
                                                                                                }
                                                                                                else if (getNoVertRuns != null && ConsequenceLabel != null)
                                                                                                {
                                                                                                    NoVertRuns = ConsequenceLabel.Label;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    NoVertRuns = need24.Value;
                                                                                                }

                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }
                                                    }
                                                }



                                            }


                                        }
                                    }
                                }
                                break;

                            case "CONSEQUENCE":

                                var getDetectionTime = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation" && x.Label.ToString() == "Detection Time").FirstOrDefault();
                                if (getDetectionTime != null && need24.DisplayValue != null)
                                {
                                    DetectionTime = need24.DisplayValue;
                                }
                                else if (getDetectionTime != null && need24.ProbabilityLabel != null)
                                {
                                    DetectionTime = need24.ProbabilityLabel;
                                }
                                else if (getDetectionTime != null && need24.ConsequenceLabel != null)
                                {
                                    DetectionTime = need24.ConsequenceLabel;
                                }
                                else if (getDetectionTime != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    DetectionTime = need24.Value;
                                }
                                else
                                {
                                    var getIsolationTime = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Isolation Time").FirstOrDefault();
                                    if (getIsolationTime != null && need24.DisplayValue != null)
                                    {
                                        IsolationTime = need24.DisplayValue;
                                    }
                                    else if (getIsolationTime != null && need24.ProbabilityLabel != null)
                                    {
                                        IsolationTime = need24.ProbabilityLabel;
                                    }
                                    else if (getIsolationTime != null && need24.ConsequenceLabel != null)
                                    {
                                        IsolationTime = need24.ConsequenceLabel;
                                    }
                                    else if (getIsolationTime != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        IsolationTime = need24.Value;
                                    }
                                    else
                                    {

                                        var getDikeArea = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Dike Area").FirstOrDefault();
                                        if (getDikeArea != null && need24.DisplayValue != null)
                                        {//依備註7原則顯示資料x10.7639

                                            DikeArea = Convert.ToDouble(need24.DisplayValue) * 10.7639;
                                        }
                                        else if (getDikeArea != null && need24.ProbabilityLabel != null)
                                        {
                                            DikeArea = Convert.ToDouble(need24.ProbabilityLabel) * 10.7639;

                                        }
                                        else if (getDikeArea != null && need24.ConsequenceLabel != null)
                                        {
                                            DikeArea = Convert.ToDouble(need24.ConsequenceLabel) * 10.7639;

                                        }
                                        else if (getDikeArea != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            DikeArea = Convert.ToDouble(need24.Value) * 10.7639;

                                        }
                                        else
                                        {
                                            var getDikedFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Dike Area?").FirstOrDefault();
                                            if (getDikedFlag != null && need24.DisplayValue != null)
                                            {
                                                DikedFlag = need24.DisplayValue;
                                            }
                                            else if (getDikedFlag != null && need24.ProbabilityLabel != null)
                                            {
                                                DikedFlag = need24.ProbabilityLabel;
                                            }
                                            else if (getDikedFlag != null && need24.ConsequenceLabel != null)
                                            {
                                                DikedFlag = need24.ConsequenceLabel;
                                            }
                                            else if (getDikedFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                            {
                                                DikedFlag = need24.Value;
                                            }
                                            else
                                            {
                                                var getInventory = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Inventory").FirstOrDefault();
                                                if (getInventory != null && need24.DisplayValue != null)
                                                {
                                                    Inventory = Convert.ToDouble(need24.DisplayValue) * 0.45359237;
                                                }
                                                else if (getInventory != null && need24.ProbabilityLabel != null)
                                                {
                                                    Inventory = Convert.ToDouble(need24.ProbabilityLabel) * 0.45359237;

                                                }
                                                else if (getInventory != null && need24.ConsequenceLabel != null)
                                                {
                                                    Inventory = Convert.ToDouble(need24.ConsequenceLabel) * 0.45359237;

                                                }
                                                else if (getInventory != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                {
                                                    Inventory = Convert.ToDouble(need24.Value) * 0.45359237;

                                                }
                                                else
                                                {
                                                    var getOperatingPressure = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Operating Pressure").FirstOrDefault();
                                                    if (getOperatingPressure != null && need24.DisplayValue != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(need24.DisplayValue) * 0.0689476;
                                                    }
                                                    else if (getOperatingPressure != null && need24.ProbabilityLabel != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(need24.ProbabilityLabel) * 0.0689476;


                                                    }
                                                    else if (getOperatingPressure != null && need24.ConsequenceLabel != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(need24.ConsequenceLabel) * 0.0689476;

                                                    }
                                                    else if (getOperatingPressure != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(need24.Value) * 0.0689476;

                                                    }
                                                    else
                                                    {
                                                        var getOperatingTemp = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Operating Temperature").FirstOrDefault();
                                                        if (getOperatingTemp != null && need24.DisplayValue != null)
                                                        {
                                                            OperatingTemp = Convert.ToDouble(need24.DisplayValue);

                                                        }
                                                        else if (getOperatingTemp != null && need24.ProbabilityLabel != null)
                                                        { //(value - 32)x5/9
                                                            var OperatingTemp1 = Convert.ToDouble(need24.ProbabilityLabel);
                                                            OperatingTemp= ((OperatingTemp1-32)*5)/ 9;
                                                        }
                                                        else if (getOperatingTemp != null && need24.ConsequenceLabel != null)
                                                        {
                                                            var OperatingTemp1 = Convert.ToDouble(need24.ConsequenceLabel);
                                                            OperatingTemp = ((OperatingTemp1 - 32) * 5) / 9;

                                                        }
                                                        else if (getOperatingTemp != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            var OperatingTemp1 = Convert.ToDouble(need24.Value);
                                                            OperatingTemp = ((OperatingTemp1 - 32) * 5) / 9;

                                                        }
                                                        else
                                                        {
                                                            var getRepFluid = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                 && x.Label.ToString() == "Representative Fluid").FirstOrDefault();
                                                            if (getRepFluid != null && need24.DisplayValue != null)
                                                            {
                                                                RepFluid = need24.DisplayValue;
                                                            }
                                                            else if (getRepFluid != null && need24.ProbabilityLabel != null)
                                                            {
                                                                RepFluid = need24.ProbabilityLabel;
                                                            }
                                                            else if (getRepFluid != null && need24.ConsequenceLabel != null)
                                                            {
                                                                RepFluid = need24.ConsequenceLabel;
                                                            }
                                                            else if (getRepFluid != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                RepFluid = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getProductionLoss = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Production Loss").FirstOrDefault();
                                                                if (getProductionLoss != null && need24.DisplayValue != null)
                                                                {
                                                                    ProductionLoss = need24.DisplayValue;
                                                                }
                                                                else if (getProductionLoss != null && need24.ProbabilityLabel != null)
                                                                {
                                                                    ProductionLoss = need24.ProbabilityLabel;
                                                                }
                                                                else if (getProductionLoss != null && need24.ConsequenceLabel != null)
                                                                {
                                                                    ProductionLoss = need24.ConsequenceLabel;
                                                                }
                                                                else if (getProductionLoss != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                {
                                                                    ProductionLoss = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getPercentToxic = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Percent Toxic").FirstOrDefault();
                                                                    if (getPercentToxic != null && need24.DisplayValue != null)
                                                                    {
                                                                        PercentToxic = need24.DisplayValue;
                                                                    }
                                                                    else if (getPercentToxic != null && need24.ProbabilityLabel != null)
                                                                    {
                                                                        PercentToxic = need24.ProbabilityLabel;
                                                                    }
                                                                    else if (getPercentToxic != null && need24.ConsequenceLabel != null)
                                                                    {
                                                                        PercentToxic = need24.ConsequenceLabel;
                                                                    }
                                                                    else if (getPercentToxic != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                    {
                                                                        PercentToxic = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getToxicFluid = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Toxic Fluid").FirstOrDefault();
                                                                        if (getToxicFluid != null && need24.DisplayValue != null)
                                                                        {
                                                                            ToxicFluid = need24.DisplayValue;
                                                                        }
                                                                        else if (getToxicFluid != null && need24.ProbabilityLabel != null)
                                                                        {
                                                                            ToxicFluid = need24.ProbabilityLabel;
                                                                        }
                                                                        else if (getToxicFluid != null && need24.ConsequenceLabel != null)
                                                                        {
                                                                            ToxicFluid = need24.ConsequenceLabel;
                                                                        }
                                                                        else if (getToxicFluid != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                        {
                                                                            ToxicFluid = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getToxicMixtureFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Toxic Mixture?").FirstOrDefault();
                                                                            if (getToxicMixtureFlag != null && need24.DisplayValue != null)
                                                                            {
                                                                                ToxicMixtureFlag = need24.DisplayValue;
                                                                            }
                                                                            else if (getToxicMixtureFlag != null && need24.ProbabilityLabel != null)
                                                                            {
                                                                                ToxicMixtureFlag = need24.ProbabilityLabel;
                                                                            }
                                                                            else if (getToxicMixtureFlag != null && need24.ConsequenceLabel != null)
                                                                            {
                                                                                ToxicMixtureFlag = need24.ConsequenceLabel;
                                                                            }

                                                                            else
                                                                            {
                                                                                ToxicMixtureFlag = need24.Value;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;

                            case "ENVIRONMENTAL CRACKING":
                                var getEnvCrckgInspConf = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Inspection Information"
                                                                 && x.Label.ToString() == "Confidence").FirstOrDefault();
                                if (getEnvCrckgInspConf != null && need24.DisplayValue != null)
                                {
                                    EnvCrckgInspConf = need24.DisplayValue;
                                }
                                else if (getEnvCrckgInspConf != null && need24.ProbabilityLabel != null)
                                {
                                    EnvCrckgInspConf = need24.ProbabilityLabel;
                                }
                                else if (getEnvCrckgInspConf != null && need24.ConsequenceLabel != null)
                                {
                                    EnvCrckgInspConf = need24.ConsequenceLabel;
                                }
                                else if (getEnvCrckgInspConf != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    EnvCrckgInspConf = need24.Value;
                                }
                                else
                                {
                                    var getEnvCrckgLastInspDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Inspection Information"
                                                                 && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                    if (getEnvCrckgLastInspDate != null && need24.DisplayValue != null)
                                    {
                                        EnvCrckgLastInspDate = need24.DisplayValue;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && need24.ProbabilityLabel != null)
                                    {
                                        EnvCrckgLastInspDate = need24.ProbabilityLabel;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && need24.ConsequenceLabel != null)
                                    {
                                        EnvCrckgLastInspDate = need24.ConsequenceLabel;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        EnvCrckgLastInspDate = need24.Value;
                                    }
                                    else
                                    {

                                        var getEnvCrckgServDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Potential for Cracking"
                                                                         && x.Label.ToString() == "Environmental Cracking Date in Service").FirstOrDefault();//其實應該要24
                                        if (getEnvCrckgServDate != null && need24.DisplayValue != null)
                                        {
                                            EnvCrckgServDate = need24.DisplayValue;
                                        }
                                        else if (getEnvCrckgServDate != null && need24.ProbabilityLabel != null)
                                        {
                                            EnvCrckgServDate = need24.ProbabilityLabel;
                                        }
                                        else if (getEnvCrckgServDate != null && need24.ConsequenceLabel != null)
                                        {
                                            EnvCrckgServDate = need24.ConsequenceLabel;
                                        }
                                        else if (getEnvCrckgServDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            EnvCrckgServDate = need24.Value;
                                        }
                                        else
                                        {
                                            var getEnvCrckgMech = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Potential for Cracking"
                                                                         && x.Label.ToString().Contains("Environmental Cracking Mechanism")).FirstOrDefault();//其實應該要24
                                            if (getEnvCrckgMech != null && need24.DisplayValue != null)
                                            {
                                                EnvCrckgMech = need24.DisplayValue;
                                            }
                                            else if (getEnvCrckgMech != null && need24.ProbabilityLabel != null)
                                            {
                                                EnvCrckgMech = need24.ProbabilityLabel;
                                            }
                                            else if (getEnvCrckgMech != null && need24.ConsequenceLabel != null)
                                            {
                                                EnvCrckgMech = need24.ConsequenceLabel;
                                            }
                                            else
                                            {
                                                EnvCrckgMech = need24.Value;
                                            }



                                        }
                                    }
                                }
                                break;


                            case "EXTERNAL CORROSION":
                                var getHumidity = get31.Where(x => x.GroupName.ToString() == "Atmospheric Influence"
                                                                 && x.Label.ToString() == "Humidity").FirstOrDefault();
                                if (getHumidity != null && need24.DisplayValue != null)
                                {
                                    Humidity = need24.DisplayValue;
                                }
                                else if (getHumidity != null && need24.ProbabilityLabel != null)
                                {
                                    Humidity = need24.ProbabilityLabel;
                                }
                                else if (getHumidity != null && need24.ConsequenceLabel != null)
                                {
                                    Humidity = need24.ConsequenceLabel;
                                }
                                else if (getHumidity != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    Humidity = need24.Value;
                                }
                                else
                                {
                                    var getExtWettingFlag = get31.Where(x => x.GroupName.ToString() == "Atmospheric Influence"
                                                                         && x.Label.ToString() == "Near Cool Tower/Wetting?").FirstOrDefault();//其實應該要24
                                    if (getExtWettingFlag != null && need24.DisplayValue != null)
                                    {
                                        ExtWettingFlag = need24.DisplayValue;
                                    }
                                    else if (getExtWettingFlag != null && need24.ProbabilityLabel != null)
                                    {
                                        ExtWettingFlag = need24.ProbabilityLabel;
                                    }
                                    else if (getExtWettingFlag != null && need24.ConsequenceLabel != null)
                                    {
                                        ExtWettingFlag = need24.ConsequenceLabel;
                                    }
                                    else if (getExtWettingFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        ExtWettingFlag = need24.Value;
                                    }
                                    else
                                    {

                                        var getExtCorrosionOption = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Corrosion Option").FirstOrDefault();//其實應該要24
                                        if (getExtCorrosionOption != null && need24.DisplayValue != null)
                                        {
                                            ExtCorrosionOption = need24.DisplayValue;
                                        }
                                        else if (getExtCorrosionOption != null && need24.ProbabilityLabel != null)
                                        {
                                            ExtCorrosionOption = need24.ProbabilityLabel;
                                        }
                                        else if (getExtCorrosionOption != null && need24.ConsequenceLabel != null)
                                        {
                                            ExtCorrosionOption = need24.ConsequenceLabel;
                                        }
                                        else if (getExtCorrosionOption != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            ExtCorrosionOption = need24.Value;
                                        }
                                        else
                                        {
                                            var getExtExpecedCorrRate = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Expected").FirstOrDefault();//其實應該要24
                                            if (getExtExpecedCorrRate != null && need24.DisplayValue != null)
                                            {

                                                ExtExpecedCorrRate = Convert.ToDouble(need24.DisplayValue) * 25.4;
                                            }
                                            else if (getExtExpecedCorrRate != null && need24.ProbabilityLabel != null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                            }
                                            else if (getExtExpecedCorrRate != null && need24.ConsequenceLabel != null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                            }
                                            else if (getExtExpecedCorrRate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                            }
                                            else
                                            {
                                                var getExtMeasuredCorrRate = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Measured").FirstOrDefault();//其實應該要24
                                                if (getExtMeasuredCorrRate != null && need24.DisplayValue != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(need24.DisplayValue) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && need24.ProbabilityLabel != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && need24.ConsequenceLabel != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(need24.Value) * 25.4;
                                                }
                                                else
                                                {
                                                    var getExtServDate = get31.Where(x => x.GroupName.ToString() == "Design Information"
                                                                         && x.Label.ToString() == "External Date in Service").FirstOrDefault();//其實應該要24
                                                    if (getExtServDate != null && need24.DisplayValue != null)
                                                    {
                                                        ExtServDate = need24.DisplayValue;
                                                    }
                                                    else if (getExtServDate != null && need24.ProbabilityLabel != null)
                                                    {
                                                        ExtServDate = need24.ProbabilityLabel;
                                                    }
                                                    else if (getExtServDate != null && need24.ConsequenceLabel != null)
                                                    {
                                                        ExtServDate = need24.ConsequenceLabel;
                                                    }
                                                    else if (getExtServDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                    {
                                                        ExtServDate = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getOperatingTemperature = get31.Where(x => x.GroupName.ToString() == "Design Information"
                                                                         && x.Label.ToString() == "Operating Temperature").FirstOrDefault();//其實應該要24
                                                        if (getOperatingTemperature != null && need24.DisplayValue != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(need24.DisplayValue);
                                                            OperatingTemperature= ((OperatingTemperature1-32)*5)/ 9;
                                                        }
                                                        else if (getOperatingTemperature != null && need24.ProbabilityLabel != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(need24.ProbabilityLabel);
                                                            OperatingTemperature = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else if (getOperatingTemperature != null && need24.ConsequenceLabel != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(need24.ConsequenceLabel);
                                                            OperatingTemperature = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else if (getOperatingTemperature != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(need24.Value);
                                                            OperatingTemperature = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else
                                                        {
                                                            var getCUIFlag = get31.Where(x => x.GroupName.ToString() == "Design Information "
                                                                         && x.Label.ToString() == "Susceptible to Corrosion?").FirstOrDefault();//其實應該要24
                                                            if (getCUIFlag != null && need24.DisplayValue != null)
                                                            {
                                                                CUIFlag = need24.DisplayValue;
                                                            }
                                                            else if (getCUIFlag != null && need24.ProbabilityLabel != null)
                                                            {
                                                                CUIFlag = need24.ProbabilityLabel;
                                                            }
                                                            else if (getCUIFlag != null && need24.ConsequenceLabel != null)
                                                            {
                                                                CUIFlag = need24.ConsequenceLabel;
                                                            }
                                                            else if (getCUIFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                CUIFlag = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getExtInspConf = get31.Where(x => x.GroupName.ToString() == "External Inspection Information"
                                                                         && x.Label.ToString() == "Confidence").FirstOrDefault();//其實應該要24
                                                                if (getExtInspConf != null && need24.DisplayValue != null)
                                                                {
                                                                    ExtInspConf = need24.DisplayValue;
                                                                }
                                                                else if (getExtInspConf != null && need24.ProbabilityLabel != null)
                                                                {
                                                                    ExtInspConf = need24.ProbabilityLabel;
                                                                }
                                                                else if (getExtInspConf != null && need24.ConsequenceLabel != null)
                                                                {
                                                                    ExtInspConf = need24.ConsequenceLabel;
                                                                }
                                                                else if (getExtInspConf != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                {
                                                                    ExtInspConf = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getExtLastInspDate = get31.Where(x => x.GroupName.ToString() == "External Inspection Information"
                                                                         && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                                                    if (getExtLastInspDate != null && need24.DisplayValue != null)
                                                                    {
                                                                        ExtLastInspDate = need24.DisplayValue;
                                                                    }
                                                                    else if (getExtLastInspDate != null && need24.ProbabilityLabel != null)
                                                                    {
                                                                        ExtLastInspDate = need24.ProbabilityLabel;
                                                                    }
                                                                    else if (getExtLastInspDate != null && need24.ConsequenceLabel != null)
                                                                    {
                                                                        ExtLastInspDate = need24.ConsequenceLabel;
                                                                    }
                                                                    else if (getExtLastInspDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                    {
                                                                        ExtLastInspDate = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getExtNoOfInsp = get31.Where(x => x.GroupName.ToString() == "External Inspection Information"
                                                                         && x.Label.ToString() == "No. of Inspection").FirstOrDefault();//其實應該要24
                                                                        if (getExtNoOfInsp != null && need24.DisplayValue != null)
                                                                        {
                                                                            ExtNoOfInsp = need24.DisplayValue;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && need24.ProbabilityLabel != null)
                                                                        {
                                                                            ExtNoOfInsp = need24.ProbabilityLabel;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && need24.ConsequenceLabel != null)
                                                                        {
                                                                            ExtNoOfInsp = need24.ConsequenceLabel;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                        {
                                                                            ExtNoOfInsp = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getExtCoating = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Coating").FirstOrDefault();//其實應該要24
                                                                            if (getExtCoating != null && need24.DisplayValue != null)
                                                                            {
                                                                                ExtCoating = need24.DisplayValue;
                                                                            }
                                                                            else if (getExtCoating != null && need24.ProbabilityLabel != null)
                                                                            {
                                                                                ExtCoating = need24.ProbabilityLabel;
                                                                            }
                                                                            else if (getExtCoating != null && need24.ConsequenceLabel != null)
                                                                            {
                                                                                ExtCoating = need24.ConsequenceLabel;
                                                                            }
                                                                            else if (getExtCoating != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                            {
                                                                                ExtCoating = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getInsulatedFlag = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulated?").FirstOrDefault();//其實應該要24
                                                                                if (getInsulatedFlag != null && need24.DisplayValue != null)
                                                                                {
                                                                                    InsulatedFlag = need24.DisplayValue;
                                                                                }
                                                                                else if (getInsulatedFlag != null && need24.ProbabilityLabel != null)
                                                                                {
                                                                                    InsulatedFlag = need24.ProbabilityLabel;
                                                                                }
                                                                                else if (getInsulatedFlag != null && need24.ConsequenceLabel != null)
                                                                                {
                                                                                    InsulatedFlag = need24.ConsequenceLabel;
                                                                                }
                                                                                else if (getInsulatedFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                {
                                                                                    InsulatedFlag = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getInsulationCondition = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulation Condition").FirstOrDefault();//其實應該要24
                                                                                    if (getInsulationCondition != null && need24.DisplayValue != null)
                                                                                    {
                                                                                        InsulationCondition = need24.DisplayValue;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && need24.ProbabilityLabel != null)
                                                                                    {
                                                                                        InsulationCondition = need24.ProbabilityLabel;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && need24.ConsequenceLabel != null)
                                                                                    {
                                                                                        InsulationCondition = need24.ConsequenceLabel;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                    {
                                                                                        InsulationCondition = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getInsulationType = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulation Type").FirstOrDefault();//其實應該要24
                                                                                        if (getInsulationType != null && need24.DisplayValue != null)
                                                                                        {
                                                                                            InsulationType = need24.DisplayValue;
                                                                                        }
                                                                                        else if (getInsulationType != null && need24.ProbabilityLabel != null)
                                                                                        {
                                                                                            InsulationType = need24.ProbabilityLabel;
                                                                                        }
                                                                                        else if (getInsulationType != null && need24.ConsequenceLabel != null)
                                                                                        {
                                                                                            InsulationType = need24.ConsequenceLabel;
                                                                                        }
                                                                                        else if (getInsulationType != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                        {
                                                                                            InsulationType = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        { //!!!!超級奇怪
                                                                                            var getExtCalcCorrRate = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                                            && x.Label.ToString() == "Calculated Corrosion Rate").FirstOrDefault();//其實應該要24
                                                                                            if (getExtCalcCorrRate != null && need24.DisplayValue25 == need24.Value25)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(need24.DisplayValue25) * 25.4;

                                                                                            }
                                                                                            else if (getExtCalcCorrRate != null && need24.ProbabilityLabel25 == need24.Value25)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(need24.ProbabilityLabel25) * 25.4;

                                                                                            }
                                                                                            else if (getExtCalcCorrRate != null && need24.ConsequenceLabel25 == need24.Value25)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(need24.ConsequenceLabel25) * 25.4;

                                                                                            }
                                                                                            else
                                                                                            {

                                                                                                ExtCalcCorrRate = need24.ExtCalcCorrRate * 25.4;
                                                                                                //ExtCalcCorrRate = Convert.ToDouble(need24.Value25) * 25.4;

                                                                                            }
                                                                                            //用30的不是31的 var ExtCalcCorrRate = getAllComponent.Where(x => x.NodeLabel.ToString() == "EXTERNAL CORROSION" && x.GroupName.ToString() == "Corrosion Information"
                                                                                            //&& x.Label.ToString() == "Corrosion Option").Select(x => new { x.Value }).FirstOrDefault();
                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }
                                                    }
                                                }



                                            }


                                        }
                                    }
                                }
                                break;



                            case "FLUID DATA":
                                var getAIT = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                && x.Label.ToString() == "AIT").FirstOrDefault();
                                if (getAIT != null && need24.DisplayValue != null)
                                {
                                    var AIT1 = Convert.ToDouble(need24.DisplayValue);
                                    AIT = ((AIT1 - 32) * 5) / 9;
                                }
                                else if (getAIT != null && need24.ProbabilityLabel != null)
                                {
                                    var AIT1 = Convert.ToDouble(need24.ProbabilityLabel);
                                    AIT = ((AIT1 - 32) * 5) / 9;

                                   
                                }
                                else if (getAIT != null && need24.ConsequenceLabel != null)
                                {
                                    var AIT1 = Convert.ToDouble(need24.ConsequenceLabel);
                                    AIT = ((AIT1 - 32) * 5) / 9;

                                   
                                }
                                else if (getAIT != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    var AIT1 = Convert.ToDouble(need24.Value);
                                    AIT = ((AIT1 - 32) * 5) / 9;

                                    
                                }
                                else
                                {
                                    var getBoilingPoint = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                        && x.Label.ToString() == "Boiling Point").FirstOrDefault();//其實應該要24
                                    if (getBoilingPoint != null && need24.DisplayValue != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(need24.DisplayValue);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;

                                       
                                    }
                                    else if (getBoilingPoint != null && need24.ProbabilityLabel != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(need24.ProbabilityLabel);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;

                                        
                                    }
                                    else if (getBoilingPoint != null && need24.ConsequenceLabel != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(need24.ConsequenceLabel);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;

                                        
                                    }
                                    else if (getBoilingPoint != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(need24.Value);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;

                                        
                                    }
                                    else
                                    {

                                        var getFluidType = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                        && x.Label.ToString() == "Fluid Type").FirstOrDefault();//其實應該要24
                                        if (getFluidType != null && need24.DisplayValue != null)
                                        {
                                            FluidType = need24.DisplayValue;
                                        }
                                        else if (getFluidType != null && need24.ProbabilityLabel != null)
                                        {
                                            FluidType = need24.ProbabilityLabel;
                                        }
                                        else if (getFluidType != null && need24.ConsequenceLabel != null)
                                        {
                                            FluidType = need24.ConsequenceLabel;
                                        }
                                        else if (getFluidType != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            FluidType = need24.Value;
                                        }
                                        else
                                        {
                                            var getToxicBP = get31.Where(x => x.GroupName.ToString() == "Toxic Fluid Data"
                                                                        && x.Label.ToString() == "Boiling Point").FirstOrDefault();//其實應該要24
                                            if (getToxicBP != null && need24.DisplayValue != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(need24.DisplayValue);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;

                                                
                                            }
                                            else if (getToxicBP != null && need24.ProbabilityLabel != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(need24.ProbabilityLabel);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;

                                                
                                            }
                                            else if (getToxicBP != null && need24.ConsequenceLabel != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(need24.ConsequenceLabel);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;

                                                
                                            }
                                            else
                                            {
                                                //var ToxicBP1 = Convert.ToDouble(need24.Value);
                                                ToxicBP = ((ToxicBP - 32) * 5) / 9;

                                                
                                            }



                                        }
                                    }
                                }
                                break;







                            case "INTERNAL CORROSION":
                                var getIntCorrosionType = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                && x.Label.ToString() == "Corrosion Type").FirstOrDefault();
                                if (getIntCorrosionType != null && need24.DisplayValue != null)
                                {
                                    IntCorrosionType = need24.DisplayValue;
                                }
                                else if (getIntCorrosionType != null && need24.ProbabilityLabel != null)
                                {
                                    IntCorrosionType = need24.ProbabilityLabel;
                                }
                                else if (getIntCorrosionType != null && need24.ConsequenceLabel != null)
                                {
                                    IntCorrosionType = need24.ConsequenceLabel;
                                }
                                else if (getIntCorrosionType != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    IntCorrosionType = need24.Value;
                                }
                                else
                                {
                                    var getIntCorrosionOption = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Corrosion Option").FirstOrDefault();//其實應該要24
                                    if (getIntCorrosionOption != null && need24.DisplayValue != null)
                                    {
                                        IntCorrosionOption = need24.DisplayValue;
                                    }
                                    else if (getIntCorrosionOption != null && need24.ProbabilityLabel != null)
                                    {
                                        IntCorrosionOption = need24.ProbabilityLabel;
                                    }
                                    else if (getIntCorrosionOption != null && need24.ConsequenceLabel != null)
                                    {
                                        IntCorrosionOption = need24.ConsequenceLabel;
                                    }
                                    else if (getIntCorrosionOption != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        IntCorrosionOption = need24.Value;
                                    }
                                    else
                                    {

                                        var getIntExpectedCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Expected").FirstOrDefault();//其實應該要24
                                        if (getIntExpectedCorrRate != null && need24.DisplayValue != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && need24.ProbabilityLabel != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && need24.ConsequenceLabel != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                        }
                                        else
                                        {
                                            var getIntLTCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Long Term").FirstOrDefault();//其實應該要24
                                            if (getIntLTCorrRate != null && need24.DisplayValue != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && need24.ProbabilityLabel != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && need24.ConsequenceLabel != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                            }
                                            else
                                            {
                                                var getIntSTCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Short Term").FirstOrDefault();//其實應該要24
                                                if (getIntSTCorrRate != null && need24.DisplayValue != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && need24.ProbabilityLabel != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && need24.ConsequenceLabel != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                                }
                                                else
                                                {
                                                    var getCompType = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Component Type").FirstOrDefault();//其實應該要24
                                                    if (getCompType != null && need24.DisplayValue != null)
                                                    {
                                                        CompType = need24.DisplayValue;
                                                    }
                                                    else if (getCompType != null && need24.ProbabilityLabel != null)
                                                    {
                                                        CompType = need24.ProbabilityLabel;
                                                    }
                                                    else if (getCompType != null && need24.ConsequenceLabel != null)
                                                    {
                                                        CompType = need24.ConsequenceLabel;
                                                    }
                                                    else if (getCompType != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                    {
                                                        CompType = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getDesignPressure = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Design Pressure").FirstOrDefault();//其實應該要24
                                                        if (getDesignPressure != null && need24.DisplayValue != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                                        }
                                                        else if (getDesignPressure != null && need24.ProbabilityLabel != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                                        }
                                                        else if (getDesignPressure != null && need24.ConsequenceLabel != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                                        }
                                                        else if (getDesignPressure != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(need24.Value) * 25.4;

                                                        }
                                                        else
                                                        {
                                                            var getDesignTemp = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Design Temperature").FirstOrDefault();//其實應該要24
                                                            if (getDesignTemp != null && need24.DisplayValue != null)
                                                            {
                                                                //(value - 32)x5 / 9
                                                                var DesignTemp1 = Convert.ToDouble(need24.DisplayValue) ;
                                                                DesignTemp = ((DesignTemp1 - 32) * 5 )/ 9;

                                                            }
                                                            else if (getDesignTemp != null && need24.ProbabilityLabel != null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(need24.ProbabilityLabel);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;

                                                          

                                                            }
                                                            else if (getDesignTemp != null && need24.ConsequenceLabel != null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(need24.ConsequenceLabel);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;

                                                               

                                                            }
                                                            else if (getDesignTemp != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(need24.Value);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;

                                                               

                                                            }
                                                            else
                                                            {
                                                                var getDiameter = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Diameter").FirstOrDefault();//其實應該要24
                                                                if (getDiameter != null && need24.DisplayValue != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                                                }
                                                                else if (getDiameter != null && need24.ProbabilityLabel != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                                                }
                                                                else if (getDiameter != null && need24.ConsequenceLabel != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                                                }
                                                                else if (getDiameter != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                {
                                                                    Diameter = Convert.ToDouble(need24.Value) * 25.4;

                                                                }
                                                                else
                                                                {
                                                                    var getIntServDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Internal Date in Service").FirstOrDefault();//其實應該要24
                                                                    if (getIntServDate != null && need24.DisplayValue != null)
                                                                    {
                                                                        IntServDate = need24.DisplayValue;
                                                                    }
                                                                    else if (getIntServDate != null && need24.ProbabilityLabel != null)
                                                                    {
                                                                        IntServDate = need24.ProbabilityLabel;
                                                                    }
                                                                    else if (getIntServDate != null && need24.ConsequenceLabel != null)
                                                                    {
                                                                        IntServDate = need24.ConsequenceLabel;
                                                                    }
                                                                    else if (getIntServDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                    {
                                                                        IntServDate = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getODOverride = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Override Outside Diameter ").FirstOrDefault();//其實應該要24
                                                                        if (getODOverride != null && need24.DisplayValue != null)
                                                                        {
                                                                            ODOverride = Convert.ToDouble(need24.DisplayValue) * 25.4;

                                                                        }
                                                                        else if (getODOverride != null && need24.ProbabilityLabel != null)
                                                                        {
                                                                            ODOverride = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                                                        }
                                                                        else if (getODOverride != null && need24.ConsequenceLabel != null)
                                                                        {
                                                                            ODOverride = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                                                        }
                                                                        else if (getODOverride != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                        {
                                                                            ODOverride = Convert.ToDouble(need24.Value) * 25.4;

                                                                        }
                                                                        else
                                                                        {
                                                                            var getODOverrideFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Override Outside Diameter?").FirstOrDefault();//其實應該要24
                                                                            if (getODOverrideFlag != null && need24.DisplayValue != null)
                                                                            {
                                                                                ODOverrideFlag = need24.DisplayValue;
                                                                            }
                                                                            else if (getODOverrideFlag != null && need24.ProbabilityLabel != null)
                                                                            {
                                                                                ODOverrideFlag = need24.ProbabilityLabel;
                                                                            }
                                                                            else if (getODOverrideFlag != null && need24.ConsequenceLabel != null)
                                                                            {
                                                                                ODOverrideFlag = need24.ConsequenceLabel;
                                                                            }
                                                                            else if (getODOverrideFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                            {
                                                                                ODOverrideFlag = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getAnalysisDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "General Information、NULL"
                                                                        && x.Label.ToString() == "Analysis Date").FirstOrDefault();//其實應該要24
                                                                                if (getAnalysisDate != null && need24.DisplayValue != null)
                                                                                {
                                                                                    AnalysisDate = need24.DisplayValue;
                                                                                }
                                                                                else if (getAnalysisDate != null && need24.ProbabilityLabel != null)
                                                                                {
                                                                                    AnalysisDate = need24.ProbabilityLabel;
                                                                                }
                                                                                else if (getAnalysisDate != null && need24.ConsequenceLabel != null)
                                                                                {
                                                                                    AnalysisDate = need24.ConsequenceLabel;
                                                                                }
                                                                                else if (getAnalysisDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                {
                                                                                    AnalysisDate = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getIntInpsConf = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                                       && x.Label.ToString() == "Confidence").FirstOrDefault();//其實應該要24
                                                                                    if (getIntInpsConf != null && need24.DisplayValue != null)
                                                                                    {
                                                                                        IntInpsConf = need24.DisplayValue;
                                                                                    }
                                                                                    else if (getIntInpsConf != null && need24.ProbabilityLabel != null)
                                                                                    {
                                                                                        IntInpsConf = need24.ProbabilityLabel;
                                                                                    }
                                                                                    else if (getIntInpsConf != null && need24.ConsequenceLabel != null)
                                                                                    {
                                                                                        IntInpsConf = need24.ConsequenceLabel;
                                                                                    }
                                                                                    else if (getIntInpsConf != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                    {
                                                                                        IntInpsConf = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getIntLastInspDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                                       && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                                                                        if (getIntLastInspDate != null && need24.DisplayValue != null)
                                                                                        {
                                                                                            IntLastInspDate = need24.DisplayValue;
                                                                                        }
                                                                                        else if (getIntLastInspDate != null && need24.ProbabilityLabel != null)
                                                                                        {
                                                                                            IntLastInspDate = need24.ProbabilityLabel;
                                                                                        }
                                                                                        else if (getIntLastInspDate != null && need24.ConsequenceLabel != null)
                                                                                        {
                                                                                            IntLastInspDate = need24.ConsequenceLabel;
                                                                                        }
                                                                                        else if (getIntLastInspDate != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                        {
                                                                                            IntLastInspDate = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getIntNoOfInps = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                                       && x.Label.ToString() == "No. of Inspection").FirstOrDefault();//其實應該要24
                                                                                            if (getIntNoOfInps != null && need24.DisplayValue != null)
                                                                                            {
                                                                                                IntNoOfInps = need24.DisplayValue;
                                                                                            }
                                                                                            else if (getIntNoOfInps != null && need24.ProbabilityLabel != null)
                                                                                            {
                                                                                                IntNoOfInps = need24.ProbabilityLabel;
                                                                                            }
                                                                                            else if (getIntNoOfInps != null && need24.ConsequenceLabel != null)
                                                                                            {
                                                                                                IntNoOfInps = need24.ConsequenceLabel;
                                                                                            }
                                                                                            else if (getIntNoOfInps != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                            {
                                                                                                IntNoOfInps = need24.Value;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                var getConstCode = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                                       && x.Label.ToString() == "Construction Code").FirstOrDefault();//其實應該要24
                                                                                                if (getConstCode != null && need24.DisplayValue != null)
                                                                                                {
                                                                                                    ConstCode = need24.DisplayValue;
                                                                                                }
                                                                                                else if (getConstCode != null && need24.ProbabilityLabel != null)
                                                                                                {
                                                                                                    ConstCode = need24.ProbabilityLabel;
                                                                                                }
                                                                                                else if (getConstCode != null && need24.ConsequenceLabel != null)
                                                                                                {
                                                                                                    ConstCode = need24.ConsequenceLabel;
                                                                                                }
                                                                                                else if (getConstCode != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                {
                                                                                                    ConstCode = need24.Value;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    var getJointEfficiency = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                                       && x.Label.ToString() == "Joint Efficiency").FirstOrDefault();//其實應該要24
                                                                                                    if (getJointEfficiency != null && need24.DisplayValue != null)
                                                                                                    {
                                                                                                        JointEfficiency = need24.DisplayValue;
                                                                                                    }
                                                                                                    else if (getJointEfficiency != null && need24.ProbabilityLabel != null)
                                                                                                    {
                                                                                                        JointEfficiency = need24.ProbabilityLabel;
                                                                                                    }
                                                                                                    else if (getJointEfficiency != null && need24.ConsequenceLabel != null)
                                                                                                    {
                                                                                                        JointEfficiency = need24.ConsequenceLabel;
                                                                                                    }
                                                                                                    else if (getJointEfficiency != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                    {
                                                                                                        JointEfficiency = need24.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        var getOverideAllowableStress = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                                       && x.Label.ToString() == "Override Allowable Stress").FirstOrDefault();//其實應該要24
                                                                                                        if (getOverideAllowableStress != null && need24.DisplayValue != null)
                                                                                                        {
                                                                                                            OverideAllowableStress = Convert.ToDouble(need24.DisplayValue) * 0.0689476;

                                                                                                        }
                                                                                                        else if (getOverideAllowableStress != null && need24.ProbabilityLabel != null)
                                                                                                        {
                                                                                                            OverideAllowableStress = Convert.ToDouble(need24.ProbabilityLabel) * 0.0689476;

                                                                                                        }
                                                                                                        else if (getOverideAllowableStress != null && need24.ConsequenceLabel != null)
                                                                                                        {
                                                                                                            OverideAllowableStress = Convert.ToDouble(need24.ConsequenceLabel) * 0.0689476;

                                                                                                        }
                                                                                                        else if (getOverideAllowableStress != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                        {
                                                                                                            OverideAllowableStress = Convert.ToDouble(need24.Value) * 0.0689476;

                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            var getOverideAllowableStressFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                                       && x.Label.ToString() == "Override Allowable Stress?").FirstOrDefault();//其實應該要24
                                                                                                            if (getOverideAllowableStressFlag != null && need24.DisplayValue != null)
                                                                                                            {
                                                                                                                OverideAllowableStressFlag = need24.DisplayValue;
                                                                                                            }
                                                                                                            else if (getOverideAllowableStressFlag != null && need24.ProbabilityLabel != null)
                                                                                                            {
                                                                                                                OverideAllowableStressFlag = need24.ProbabilityLabel;
                                                                                                            }
                                                                                                            else if (getOverideAllowableStressFlag != null && need24.ConsequenceLabel != null)
                                                                                                            {
                                                                                                                OverideAllowableStressFlag = need24.ConsequenceLabel;
                                                                                                            }
                                                                                                            else if (getOverideAllowableStressFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                            {
                                                                                                                OverideAllowableStressFlag = need24.Value;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                var getEstMinThicknessFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                                       && x.Label.ToString() == "Override Est. Min. Thickness?").FirstOrDefault();//其實應該要24
                                                                                                                if (getEstMinThicknessFlag != null && need24.DisplayValue != null)
                                                                                                                {
                                                                                                                    EstMinThicknessFlag = need24.DisplayValue;
                                                                                                                }
                                                                                                                else if (getEstMinThicknessFlag != null && need24.ProbabilityLabel != null)
                                                                                                                {
                                                                                                                    EstMinThicknessFlag = need24.ProbabilityLabel;
                                                                                                                }
                                                                                                                else if (getEstMinThicknessFlag != null && need24.ConsequenceLabel != null)
                                                                                                                {
                                                                                                                    EstMinThicknessFlag = need24.ConsequenceLabel;
                                                                                                                }

                                                                                                                else if (getEstMinThicknessFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                                {
                                                                                                                    EstMinThicknessFlag = need24.Value;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    var getEstMinThickness = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                                  && x.Label.ToString() == "Est. Min. Thickness").FirstOrDefault();
                                                                                                                    if (getEstMinThicknessFlag != null && need24.DisplayValue25 != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = Convert.ToDouble(need24.DisplayValue25)*25.4;
                                                                                                                    }
                                                                                                                    else if (getEstMinThickness != null && need24.ProbabilityLabel25 != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = Convert.ToDouble(need24.ProbabilityLabel25)*25.4;
                                                                                                                    }
                                                                                                                    else if (getEstMinThickness != null && need24.ConsequenceLabel25 != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = Convert.ToDouble(need24.ConsequenceLabel25)*25.4;
                                                                                                                    }

                                                                                                                    else if (getEstMinThickness != null && need24.DisplayValue25 == null && need24.ProbabilityLabel25 == null && need24.ConsequenceLabel25 == null)
                                                                                                                    {
                                                                                                                        EstMinThickness = Convert.ToDouble(need24.Value25)*25.4;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        var getIntEstWallRemain = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                                    && x.Label.ToString() == "Estimated Wall Remaining").FirstOrDefault();
                                                                                                                        if (getIntEstWallRemain != null && need24.DisplayValue25 != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = Convert.ToDouble(need24.DisplayValue25)*25.4;
                                                                                                                        }
                                                                                                                        else if (getIntEstWallRemain != null && need24.ProbabilityLabel25 != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = Convert.ToDouble(need24.ProbabilityLabel25) * 25.4;
                                                                                                                        }
                                                                                                                        else if (getIntEstWallRemain != null && need24.ConsequenceLabel25 != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = Convert.ToDouble(need24.ConsequenceLabel25) * 25.4;
                                                                                                                        }

                                                                                                                        else if (getIntEstWallRemain != null && need24.DisplayValue25 == null && need24.ProbabilityLabel25 == null && need24.ConsequenceLabel25 == null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = Convert.ToDouble(need24.Value25) * 25.4;
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            var getEstHalfLife = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Calculations"
                                                                   && x.Label.ToString() == "Estimated Half Life").FirstOrDefault();
                                                                                                                            if (getEstHalfLife != null && need24.DisplayValue25 != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.DisplayValue25;
                                                                                                                            }
                                                                                                                            else if (getEstHalfLife != null && need24.ProbabilityLabel25 != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.ProbabilityLabel25;
                                                                                                                            }
                                                                                                                            else if (getEstHalfLife != null && need24.ConsequenceLabel25 != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.ConsequenceLabel25;
                                                                                                                            }

                                                                                                                            else if (getEstHalfLife != null && need24.DisplayValue25 == null && need24.ProbabilityLabel25 == null && need24.ConsequenceLabel25 == null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.Value25;
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {

                                                                                                                                var getRepThick = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                 && (x.Label.ToString() == "Initial Floor Thickness" || x.Label.ToString() == "Initial Wall Thickness")).FirstOrDefault();
                                                                                                                                if (getRepThick != null && need24.DisplayValue != null)
                                                                                                                                {

                                                                                                                                    RepThick = Convert.ToDouble(need24.DisplayValue) * 25.4;
                                                                                                                                }
                                                                                                                                else if (getRepThick != null && need24.ProbabilityLabel != null)
                                                                                                                                {
                                                                                                                                    RepThick = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;

                                                                                                                                }
                                                                                                                                else if (getRepThick != null && need24.ConsequenceLabel != null)
                                                                                                                                {
                                                                                                                                    RepThick = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;

                                                                                                                                }

                                                                                                                                else if (getRepThick != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                                                {
                                                                                                                                    RepThick = Convert.ToDouble(need24.Value) * 25.4;

                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }
                                                    }
                                                }



                                            }


                                        }
                                    }
                                }


                                break;


                            case "OTHER DAMAGE MECHANISM":
                                var getODM1 = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                              && x.Label.ToString() == "Mechanism").FirstOrDefault();
                                if (getODM1 != null && need24.DisplayValue != null)
                                {
                                    ODM1 = need24.DisplayValue;
                                }
                                else if (getODM1 != null && need24.ProbabilityLabel != null)
                                {
                                    ODM1 = need24.ProbabilityLabel;
                                }
                                else if (getODM1 != null && need24.ConsequenceLabel != null)
                                {
                                    ODM1 = need24.ConsequenceLabel;
                                }
                                else if (getODM1 != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    ODM1 = need24.Value;
                                }
                                else
                                {
                                    var getODM1Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                    if (getODM1Potential != null && need24.DisplayValue != null)
                                    {
                                        ODM1Potential = need24.DisplayValue;
                                    }
                                    else if (getODM1Potential != null && need24.ProbabilityLabel != null)
                                    {
                                        ODM1Potential = need24.ProbabilityLabel;
                                    }
                                    else if (getODM1Potential != null && need24.ConsequenceLabel != null)
                                    {
                                        ODM1Potential = need24.ConsequenceLabel;
                                    }
                                    else if (getODM1Potential != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        ODM1Potential = need24.Value;
                                    }
                                    else
                                    {

                                        var getODM1Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                        if (getODM1Probability != null && need24.DisplayValue != null)
                                        {
                                            ODM1Probability = need24.DisplayValue;
                                        }
                                        else if (getODM1Probability != null && need24.ProbabilityLabel != null)
                                        {
                                            ODM1Probability = need24.ProbabilityLabel;
                                        }
                                        else if (getODM1Probability != null && need24.ConsequenceLabel != null)
                                        {
                                            ODM1Probability = need24.ConsequenceLabel;
                                        }
                                        else if (getODM1Probability != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            ODM1Probability = need24.Value;
                                        }
                                        else
                                        {
                                            var getODM1Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                            if (getODM1Comment != null && need24.DisplayValue != null)
                                            {
                                                ODM1Comment = need24.DisplayValue;
                                            }
                                            else if (getODM1Comment != null && need24.ProbabilityLabel != null)
                                            {
                                                ODM1Comment = need24.ProbabilityLabel;
                                            }
                                            else if (getODM1Comment != null && need24.ConsequenceLabel != null)
                                            {
                                                ODM1Comment = need24.ConsequenceLabel;
                                            }
                                            else if (getODM1Comment != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                            {
                                                ODM1Comment = need24.Value;
                                            }
                                            else
                                            {
                                                var getODM2 = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                if (getODM2 != null && need24.DisplayValue != null)
                                                {
                                                    ODM2 = need24.DisplayValue;
                                                }
                                                else if (getODM2 != null && need24.ProbabilityLabel != null)
                                                {
                                                    ODM2 = need24.ProbabilityLabel;
                                                }
                                                else if (getODM2 != null && need24.ConsequenceLabel != null)
                                                {
                                                    ODM2 = need24.ConsequenceLabel;
                                                }
                                                else if (getODM2 != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                {
                                                    ODM2 = need24.Value;
                                                }
                                                else
                                                {
                                                    var getODM2Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                    if (getODM2Potential != null && need24.DisplayValue != null)
                                                    {
                                                        ODM2Potential = need24.DisplayValue;
                                                    }
                                                    else if (getODM2Potential != null && need24.ProbabilityLabel != null)
                                                    {
                                                        ODM2Potential = need24.ProbabilityLabel;
                                                    }
                                                    else if (getODM2Potential != null && need24.ConsequenceLabel != null)
                                                    {
                                                        ODM2Potential = need24.ConsequenceLabel;
                                                    }
                                                    else if (getODM2Potential != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                    {
                                                        ODM2Potential = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getODM2Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                        if (getODM2Probability != null && need24.DisplayValue != null)
                                                        {
                                                            ODM2Probability = need24.DisplayValue;
                                                        }
                                                        else if (getODM2Probability != null && need24.ProbabilityLabel != null)
                                                        {
                                                            ODM2Probability = need24.ProbabilityLabel;
                                                        }
                                                        else if (getODM2Probability != null && need24.ConsequenceLabel != null)
                                                        {
                                                            ODM2Probability = need24.ConsequenceLabel;
                                                        }
                                                        else if (getODM2Probability != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            ODM2Probability = need24.Value;
                                                        }
                                                        else
                                                        {
                                                            var getODM2Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                            if (getODM2Comment != null && need24.DisplayValue != null)
                                                            {
                                                                ODM2Comment = need24.DisplayValue;
                                                            }
                                                            else if (getODM2Comment != null && need24.ProbabilityLabel != null)
                                                            {
                                                                ODM2Comment = need24.ProbabilityLabel;
                                                            }
                                                            else if (getODM2Comment != null && need24.ConsequenceLabel != null)
                                                            {
                                                                ODM2Comment = need24.ConsequenceLabel;
                                                            }
                                                            else if (getODM2Comment != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                ODM2Comment = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getODM3 = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                if (getODM3 != null && need24.DisplayValue != null)
                                                                {
                                                                    ODM3 = need24.DisplayValue;
                                                                }
                                                                else if (getODM3 != null && need24.ProbabilityLabel != null)
                                                                {
                                                                    ODM3 = need24.ProbabilityLabel;
                                                                }
                                                                else if (getODM3 != null && need24.ConsequenceLabel != null)
                                                                {
                                                                    ODM3 = need24.ConsequenceLabel;
                                                                }
                                                                else if (getODM3 != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                {
                                                                    ODM3 = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getODM3Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                    if (getODM3Potential != null && need24.DisplayValue != null)
                                                                    {
                                                                        ODM3Potential = need24.DisplayValue;
                                                                    }
                                                                    else if (getODM3Potential != null && need24.ProbabilityLabel != null)
                                                                    {
                                                                        ODM3Potential = need24.ProbabilityLabel;
                                                                    }
                                                                    else if (getODM3Potential != null && need24.ConsequenceLabel != null)
                                                                    {
                                                                        ODM3Potential = need24.ConsequenceLabel;
                                                                    }
                                                                    else if (getODM3Potential != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                    {
                                                                        ODM3Potential = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getODM3Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                        if (getODM3Probability != null && need24.DisplayValue != null)
                                                                        {
                                                                            ODM3Probability = need24.DisplayValue;
                                                                        }
                                                                        else if (getODM3Probability != null && need24.ProbabilityLabel != null)
                                                                        {
                                                                            ODM3Probability = need24.ProbabilityLabel;
                                                                        }
                                                                        else if (getODM3Probability != null && need24.ConsequenceLabel != null)
                                                                        {
                                                                            ODM3Probability = need24.ConsequenceLabel;
                                                                        }
                                                                        else if (getODM3Probability != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                        {
                                                                            ODM3Probability = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getODM3Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                            if (getODM3Comment != null && need24.DisplayValue != null)
                                                                            {
                                                                                ODM3Comment = need24.DisplayValue;
                                                                            }
                                                                            else if (getODM3Comment != null && need24.ProbabilityLabel != null)
                                                                            {
                                                                                ODM3Comment = need24.ProbabilityLabel;
                                                                            }
                                                                            else if (getODM3Comment != null && need24.ConsequenceLabel != null)
                                                                            {
                                                                                ODM3Comment = need24.ConsequenceLabel;
                                                                            }
                                                                            else if (getODM3Comment != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                            {
                                                                                ODM3Comment = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getODM4 = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                                if (getODM4 != null && need24.DisplayValue != null)
                                                                                {
                                                                                    ODM4 = need24.DisplayValue;
                                                                                }
                                                                                else if (getODM4 != null && need24.ProbabilityLabel != null)
                                                                                {
                                                                                    ODM4 = need24.ProbabilityLabel;
                                                                                }
                                                                                else if (getODM4 != null && need24.ConsequenceLabel != null)
                                                                                {
                                                                                    ODM4 = need24.ConsequenceLabel;
                                                                                }
                                                                                else if (getODM4 != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                {
                                                                                    ODM4 = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getODM4Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                                    if (getODM4Potential != null && need24.DisplayValue != null)
                                                                                    {
                                                                                        ODM4Potential = need24.DisplayValue;
                                                                                    }
                                                                                    else if (getODM4Potential != null && need24.ProbabilityLabel != null)
                                                                                    {
                                                                                        ODM4Potential = need24.ProbabilityLabel;
                                                                                    }
                                                                                    else if (getODM4Potential != null && need24.ConsequenceLabel != null)
                                                                                    {
                                                                                        ODM4Potential = need24.ConsequenceLabel;
                                                                                    }
                                                                                    else if (getODM4Potential != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                    {
                                                                                        ODM4Potential = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getODM4Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                                        if (getODM4Probability != null && need24.DisplayValue != null)
                                                                                        {
                                                                                            ODM4Probability = need24.DisplayValue;
                                                                                        }
                                                                                        else if (getODM4Probability != null && need24.ProbabilityLabel != null)
                                                                                        {
                                                                                            ODM4Probability = need24.ProbabilityLabel;
                                                                                        }
                                                                                        else if (getODM4Probability != null && need24.ConsequenceLabel != null)
                                                                                        {
                                                                                            ODM4Probability = need24.ConsequenceLabel;
                                                                                        }
                                                                                        else if (getODM4Probability != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                        {
                                                                                            ODM4Probability = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getODM4Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                                            if (getODM4Comment != null && need24.DisplayValue != null)
                                                                                            {
                                                                                                ODM4Comment = need24.DisplayValue;
                                                                                            }
                                                                                            else if (getODM4Comment != null && need24.ProbabilityLabel != null)
                                                                                            {
                                                                                                ODM4Comment = need24.ProbabilityLabel;
                                                                                            }
                                                                                            else if (getODM4Comment != null && need24.ConsequenceLabel != null)
                                                                                            {
                                                                                                ODM4Comment = need24.ConsequenceLabel;
                                                                                            }
                                                                                            else if (getODM4Comment != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                            {
                                                                                                ODM4Comment = need24.Value;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                var getODM5 = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                                                if (getODM5 != null && need24.DisplayValue != null)
                                                                                                {
                                                                                                    ODM5 = need24.DisplayValue;
                                                                                                }
                                                                                                else if (getODM5 != null && need24.ProbabilityLabel != null)
                                                                                                {
                                                                                                    ODM5 = need24.ProbabilityLabel;
                                                                                                }
                                                                                                else if (getODM5 != null && need24.ConsequenceLabel != null)
                                                                                                {
                                                                                                    ODM5 = need24.ConsequenceLabel;
                                                                                                }
                                                                                                else if (getODM5 != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                {
                                                                                                    ODM5 = need24.Value;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    var getODM5Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                                                    if (getODM5Potential != null && need24.DisplayValue != null)
                                                                                                    {
                                                                                                        ODM5Potential = need24.DisplayValue;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && need24.ProbabilityLabel != null)
                                                                                                    {
                                                                                                        ODM5Potential = need24.ProbabilityLabel;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && need24.ConsequenceLabel != null)
                                                                                                    {
                                                                                                        ODM5Potential = need24.ConsequenceLabel;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                    {
                                                                                                        ODM5Potential = need24.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        var getODM5Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                                                        if (getODM5Probability != null && need24.DisplayValue != null)
                                                                                                        {
                                                                                                            ODM5Probability = need24.DisplayValue;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && need24.ProbabilityLabel != null)
                                                                                                        {
                                                                                                            ODM5Probability = need24.ProbabilityLabel;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && need24.ConsequenceLabel != null)
                                                                                                        {
                                                                                                            ODM5Probability = need24.ConsequenceLabel;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                        {
                                                                                                            ODM5Probability = need24.Value;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            var getODM5Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                      && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                                                            if (getODM5Comment != null && need24.DisplayValue != null)
                                                                                                            {
                                                                                                                ODM5Comment = need24.DisplayValue;
                                                                                                            }
                                                                                                            else if (getODM5Comment != null && need24.ProbabilityLabel != null)
                                                                                                            {
                                                                                                                ODM5Comment = need24.ProbabilityLabel;
                                                                                                            }
                                                                                                            else if (getODM5Comment != null && need24.ConsequenceLabel != null)
                                                                                                            {
                                                                                                                ODM5Comment = need24.ConsequenceLabel;
                                                                                                            }

                                                                                                            else
                                                                                                            {
                                                                                                                ODM5Comment = need24.Value;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }
                                                    }
                                                }



                                            }


                                        }
                                    }
                                }


                                break;




                            default://以上都不符合走這個
                                    //Console.WriteLine("Default case");
                                break;








                        }

                        need24.JacketedFlag = JacketedFlag;
                        need24.IntEntryPossFlag = IntEntryPossFlag;
                        need24.InjectionPointFlag = InjectionPointFlag;
                        need24.MixedBoreFlag = MixedBoreFlag;
                        need24.SmallBoreFlag = SmallBoreFlag;
                        need24.NoBarrPenetrations = NoBarrPenetrations;
                        need24.NoDmgdInsd = NoDmgdInsd;
                        need24.NoDeadLegs = NoDeadLegs;
                        need24.NoElbows = NoElbows;
                        need24.NoErosionZones = NoErosionZones;
                        need24.NoHorizLowPts = NoHorizLowPts;
                        need24.NoInsdTerminators = NoInsdTerminators;
                        need24.NoLongHorizRuns = NoLongHorizRuns;
                        need24.NoReducers = NoReducers;
                        need24.NoSoilToAirIntfs = NoSoilToAirIntfs;
                        need24.NoTees = NoTees;
                        need24.NoVertRuns = NoVertRuns;

                        need24.DetectionTime = DetectionTime;
                        need24.IsolationTime = IsolationTime;
                        need24.DikeArea = DikeArea;
                        need24.DikedFlag = DikedFlag;
                        need24.Inventory = Inventory;
                        need24.OperatingPressure = OperatingPressure;
                        need24.OperatingTemp = OperatingTemp;
                        need24.RepFluid = RepFluid;
                        need24.ProductionLoss = ProductionLoss;
                        need24.PercentToxic = PercentToxic;
                        need24.ToxicFluid = ToxicFluid;
                        need24.ToxicMixtureFlag = ToxicMixtureFlag;

                        need24.EnvCrckgInspConf = EnvCrckgInspConf;
                        need24.EnvCrckgLastInspDate = EnvCrckgLastInspDate;
                        need24.EnvCrckgNoOfInsp = EnvCrckgNoOfInsp;
                        need24.EnvCrckgServDate = EnvCrckgServDate;
                        need24.EnvCrckgMech = EnvCrckgMech;

                        need24.Humidity = Humidity;
                        need24.ExtWettingFlag = ExtWettingFlag;
                        need24.ExtCorrosionOption = ExtCorrosionOption;
                        need24.ExtExpecedCorrRate = ExtExpecedCorrRate;
                        need24.ExtMeasuredCorrRate = ExtMeasuredCorrRate;
                        need24.ExtServDate = ExtServDate;
                        need24.OperatingTemperature = OperatingTemperature;
                        need24.CUIFlag = CUIFlag;
                        need24.ExtInspConf = ExtInspConf;
                        need24.ExtLastInspDate = ExtLastInspDate;
                        need24.ExtNoOfInsp = ExtNoOfInsp;
                        need24.ExtCoating = ExtCoating;
                        need24.InsulatedFlag = InsulatedFlag;
                        need24.InsulationCondition = InsulationCondition;
                        need24.InsulationType = InsulationType;

                        need24.OverideAllowableStress = OverideAllowableStress;
                        need24.BoilingPoint = BoilingPoint;
                        need24.FluidType = FluidType;
                        need24.ToxicBP = ToxicBP;
                        need24.OverideAllowableStressFlag = OverideAllowableStressFlag;
                        need24.EstMinThicknessFlag = EstMinThicknessFlag;


                        need24.ODM1 = ODM1;
                        need24.ODM1Potential = ODM1Potential;
                        need24.ODM1Probability = ODM1Probability;
                        need24.ODM1Comment = ODM1Comment;

                        need24.ODM2 = ODM2;
                        need24.ODM2Potential = ODM2Potential;
                        need24.ODM2Probability = ODM2Probability;
                        need24.ODM2Comment = ODM2Comment;

                        need24.ODM3 = ODM3;
                        need24.ODM3Potential = ODM3Potential;
                        need24.ODM3Probability = ODM3Probability;
                        need24.ODM3Comment = ODM3Comment;

                        need24.ODM4 = ODM4;
                        need24.ODM4Potential = ODM4Potential;
                        need24.ODM4Probability = ODM4Probability;
                        need24.ODM4Comment = ODM4Comment;

                        need24.ODM5 = ODM5;
                        need24.ODM5Potential = ODM5Potential;
                        need24.ODM5Probability = ODM5Probability;
                        need24.ODM5Comment = ODM5Comment;

                        need24.ExtCalcCorrRate = ExtCalcCorrRate;
                        need24.EstMinThickness = EstMinThickness;
                        need24.IntEstWallRemain = IntEstWallRemain;
                        need24.EstHalfLife = EstHalfLife;
                        need24.RepThick = RepThick;





                    }

                    var fileName = "T0NKME01" + Guid.NewGuid().ToString() + ".xlsx";
                    var guidFileName = DataList_T0NKME06model.ExportExcel<T0NKME06model>(fileName, "sheet");
                    return Ok(DataList_T0NKME06model);
                }

            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        } 
    }
}