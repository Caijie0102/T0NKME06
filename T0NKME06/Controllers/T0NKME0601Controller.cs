using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebPages;
using T0NKME06.Models;
//using T0NKME06.Extensions;

namespace T0NKME06.Controllers
{
    public class T0NKME0601Controller : ApiController
    {
        // GET: T0NKME0601
        public IHttpActionResult Get()
        {
            List<T0NKME06model> DataList_T0NKME06model = new List<T0NKME06model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();


            try
            {
                using (Entities2 db = new Entities2())
                {
                  
                    //32==24
                    var getAllComponent = from otmri in db.OrgTreeNodeModelRunInputs
                                          join otmr in db.OrgTreeNodeModelRuns on otmri.OrgTreeNodeModelRunId equals otmr.OrgTreeNodeModelRunId
                                         // join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId //31
                                          //31 join nim in db.NodeInputMetadata on otmri.NodeInputId equals nim.NodeInputId
                                          //29join mnm in db.ModelNodeMetadata on otmri.ModelNodeId equals mnm.ModelNodeId

                                         join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId
                                          //join cd in db.ComponentDetails on otn.ComponentDetailsId equals cd.ComponentDetailsId

                                          select new
                                          {
                                              ModelNodeId = otmri.ModelNodeId.ToString(),
                                              NodeInputId = otmri.NodeInputId.ToString(),

                                              //24=31
                                              //24=29

                                              // NodeLabel = mnm.NodeLabel.ToString(),
                                              //GroupName = nim.GroupName.ToString(),
                                              //Label = nim.Label.ToString(),
                                              
                                              //24
                                              Value = otmri.Value.ToString(),

                                              //24=26
                                              DisplayValue = (from rldv in db.RunnableLookupRowDisplayValues
                                                              where otmri.Value.ToString() == rldv.LookupRowId.ToString()
                                                              select rldv.DisplayValue).FirstOrDefault(),
                                              //24=27
                                              ProbabilityLabel = (from rmpv in db.RiskMatrixProbabilityVectors
                                                                  where otmri.Value.ToString() == rmpv.Index.ToString()
                                                                  select rmpv.Label).FirstOrDefault(),
                                              //24=28
                                              ConsequenceLabel = (from rmcv in db.RiskMatrixConsequenceVectors
                                                                  where otmri.Value.ToString() == rmcv.Index.ToString()
                                                                  select rmcv.Label).FirstOrDefault(),

                                              //(32)=25
                                              NodeOutputId1 = (from otmro in db.OrgTreeNodeModelRunOutputs
                                                              where otmr.OrgTreeNodeModelRunId.ToString() == otmro.OrgTreeNodeModelRunId.ToString()
                                                                  select otmro.NodeOutputId).FirstOrDefault(),

                                              Value25 = (from otmro in db.OrgTreeNodeModelRunOutputs
                                                               where otmr.OrgTreeNodeModelRunId.ToString() == otmro.OrgTreeNodeModelRunId.ToString()
                                                               select otmro.Value).FirstOrDefault(),

                                              //25=26
                                              DisplayValue25 = (from rldv in db.RunnableLookupRowDisplayValues
                                                                join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId
                                                                where otmro.Value.ToString() == rldv.LookupRowId.ToString()
                                                              select rldv.DisplayValue).FirstOrDefault(),
                                              //25=27
                                              ProbabilityLabel25 = (from rmpv in db.RiskMatrixProbabilityVectors
                                                                    join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId
                                                                    where otmro.Value.ToString() == rmpv.Index.ToString()
                                                                  select rmpv.Label).FirstOrDefault(),
                                              //25=28
                                              ConsequenceLabel25 = (from rmcv in db.RiskMatrixConsequenceVectors
                                                                    join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId
                                                                    where otmro.Value.ToString() == rmcv.Index.ToString()
                                                                  select rmcv.Label).FirstOrDefault(),


                                              //ComponentDetailsId = cd.ComponentDetailsId.ToString(),

                                              //OrgTreeNodeModelRunId= otmr.OrgTreeNodeModelRunInputs.ToString(),說是不能轉換成字串

                                              // ModelId= otmr.ModelId.ToString(),

                                              OrgTreeNodeId1 = otmr.OrgTreeNodeId.ToString(),
                                             ParentId= otn.ParentId.ToString()


                                          };

                    //return Json(getAllComponent.Take(100).ToList());
                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var fake = getAllComponent
                  //.AsEnumerable()
                  .Take(100);
                    //.ToList(); // 将 LINQ 对象转换为列表对象

                    Parallel.ForEach(fake, item =>
                    {
                        //在這個迴圈中，程式碼使用Where方法從"W0NKME03"集合中過濾出元件（Components），以便獲取該元件的詳細資料，例如組織、位置、系統、設備、元件ID等。使用Select方法從過濾後的元件集合中，選擇需要的屬性來建立一個匿名物件，然後使用FirstOrDefault方法來取得第一個符合條件的元素，如果沒有符合條件的元素，則返回null。
                        var getComponent = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == item.OrgTreeNodeId1.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();
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

                                                        ModelNodeId=item.ModelNodeId,
                                                        NodeInputId = item.NodeInputId,
                                                        NodeOutputId = item.NodeOutputId1.ToString(),
                                                        Value = item.Value,
                                                        DisplayValue = item.DisplayValue,
                                                        ProbabilityLabel = item.ProbabilityLabel,
                                                        ConsequenceLabel = item.ConsequenceLabel,

                                                        DisplayValue25 = item.DisplayValue25,
                                                        ProbabilityLabel25 = item.ProbabilityLabel25,
                                                        ConsequenceLabel25 = item.ConsequenceLabel25,
                                                        Value25 = item.Value25.ToString()




                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });
                    


                    string JacketedFlag = "",  IntEntryPossFlag = "", InjectionPointFlag=" ", MixedBoreFlag=" ", SmallBoreFlag=" ", NoBarrPenetrations=" ", NoDmgdInsd = " ", NoDeadLegs=" ", NoElbows=" ", NoErosionZones=" ", NoHorizLowPts=" ", NoInsdTerminators=" ", NoLongHorizRuns=" ", NoReducers=" ", NoSoilToAirIntfs=" ", NoTees=" ", NoVertRuns=" ",
                     DetectionTime = " ", IsolationTime = " ",  DikedFlag = " ",   RepFluid=" ", ProductionLoss=" ", PercentToxic=" ", ToxicFluid=" ", ToxicMixtureFlag=" ",
                     EnvCrckgInspConf=" ",EnvCrckgLastInspDate=" ",EnvCrckgNoOfInsp=" ",EnvCrckgServDate=" ",EnvCrckgMech=" ",
                     Humidity=" ", ExtWettingFlag=" ", ExtCorrosionOption=" ",  ExtServDate=" ", OperatingTemperature=" ", CUIFlag=" ", ExtInspConf=" ", ExtLastInspDate=" ", ExtNoOfInsp=" ", ExtCoating=" ", InsulatedFlag=" ", InsulationCondition=" ", InsulationType=" ",
                     ToxicBP=" ", FluidType=" ", BoilingPoint=" ", AIT=" ",
                     IntCorrosionType = " ", IntCorrosionOption = " ",  CompType = " ", IntServDate = " ",  ODOverrideFlag = " ", AnalysisDate = " ", IntInpsConf = " ", IntLastInspDate = " ", IntNoOfInps = " ", ConstCode = " ", JointEfficiency = " ",  OverideAllowableStressFlag=" ", EstMinThicknessFlag=" ",
                     ODM1 = " ", ODM2 = " ", ODM3 = " ", ODM4 = " ", ODM5 = " ", ODM1Potential = " ", ODM2Potential = " ", ODM3Potential = " ", ODM4Potential = " ", ODM5Potential = " ", ODM1Probability = " ", ODM2Probability = " ", ODM3Probability = " ", ODM4Probability = " ", ODM5Probability = " ", ODM1Comment = " ", ODM2Comment = " ", ODM3Comment = " ", ODM4Comment = " ", ODM5Comment = " ",  EstMinThickness=" ", IntEstWallRemain=" ", EstHalfLife=" ";

                    double DikeArea=0, Inventory=0, OperatingPressure=0, OperatingTemp=0, RepThick=0, ExtExpecedCorrRate=0, ExtMeasuredCorrRate=0, IntExpectedCorrRate =0, IntLTCorrRate = 0, IntSTCorrRate = 0, DesignPressure = 0, DesignTemp = 0, Diameter = 0, ODOverride = 0, OverideAllowableStress = 0, ExtCalcCorrRate = 0;
                    //Double DikeArea;

                    foreach (var need24 in DataList_T0NKME06model)
                    {   //31
                        var get31 = db.NodeInputMetadata.Where(x => x.NodeInputId.ToString() == need24.NodeInputId.ToString()).Select(x => new { x.GroupName, x.Label }).ToList();
                        var get30 = db.NodeOutputMetadata.Where(x => x.NodeOutputId.ToString() == need24.NodeOutputId.ToString()).Select(x => new { x.GroupName, x.Label }).ToList();

                        switch (db.ModelNodeMetadata.Where(x => x.ModelNodeId.ToString() == need24.ModelNodeId.ToString()).FirstOrDefault().NodeLabel) //24==29
                        {


                            case "ADDITIONAL INFORMATION":   
                                var getJacketedFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString().Contains("Jacket")).FirstOrDefault();//其實應該要24
                                if (getJacketedFlag != null && need24.DisplayValue != null)
                                {
                                    JacketedFlag = need24.DisplayValue;
                                }
                                else if (getJacketedFlag != null && need24.ProbabilityLabel != null)
                                {
                                    JacketedFlag = need24.ProbabilityLabel;
                                }
                                else if (getJacketedFlag != null && need24.ConsequenceLabel != null)
                                {
                                    JacketedFlag = need24.ConsequenceLabel;
                                }
                                else if (getJacketedFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    JacketedFlag = need24.Value;
                                }
                                else
                                {
                                    var getIntEntryPossFlag = get31.Where(x => x.GroupName.ToString() == "Column Locations"
                                || x.GroupName.ToString() == "Exchanger Locations" || x.GroupName.ToString() == "Pipe Locations"
                                                                     || x.GroupName.ToString() == "Tank Locations" || x.GroupName.ToString() == "Vessel Locations"
                                                                     && x.Label.ToString() == "Internal Entry Possible").FirstOrDefault();//其實應該要24
                                    if (getIntEntryPossFlag != null && need24.DisplayValue != null)
                                    {
                                        IntEntryPossFlag = need24.DisplayValue;
                                    }
                                    else if (getIntEntryPossFlag != null && need24.ProbabilityLabel != null)
                                    {
                                        IntEntryPossFlag = need24.ProbabilityLabel;
                                    }
                                    else if (getIntEntryPossFlag != null && need24.ConsequenceLabel != null)
                                    {
                                        IntEntryPossFlag = need24.ConsequenceLabel;
                                    }
                                    else if (getIntEntryPossFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        IntEntryPossFlag = need24.Value;
                                    }
                                    else
                                    {

                                    var getInjectionPointFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString() == "Injection Point?").FirstOrDefault();//其實應該要24
                                    if (getInjectionPointFlag != null && need24.DisplayValue != null)
                                    {
                                        InjectionPointFlag = need24.DisplayValue;
                                    }
                                    else if (getInjectionPointFlag != null && need24.ProbabilityLabel != null)
                                    {
                                        InjectionPointFlag = need24.ProbabilityLabel;
                                    }
                                    else if (getInjectionPointFlag != null && need24.ConsequenceLabel != null)
                                    {
                                        InjectionPointFlag = need24.ConsequenceLabel;
                                    }
                                    else if (getInjectionPointFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        InjectionPointFlag = need24.Value;
                                    }
                                    else
                                    {
                                        var getMixedBoreFlag = get31.Where(x => x.GroupName.ToString() == "Other Data" && x.Label.ToString() == "Mixed Bore?").FirstOrDefault();//其實應該要24
                                        if (getMixedBoreFlag != null && need24.DisplayValue != null)
                                        {
                                            MixedBoreFlag = need24.DisplayValue;
                                        }
                                        else if (getMixedBoreFlag != null && need24.ProbabilityLabel != null)
                                        {
                                            MixedBoreFlag = need24.ProbabilityLabel;
                                        }
                                        else if (getMixedBoreFlag != null && need24.ConsequenceLabel != null)
                                        {
                                            MixedBoreFlag = need24.ConsequenceLabel;
                                        }
                                        else if (getMixedBoreFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                        {
                                            MixedBoreFlag = need24.Value;
                                        }
                                        else
                                        {
                                            var getSmallBoreFlag = get31.Where(x => x.GroupName.ToString() == "Other Data"
                                                                             && x.Label.ToString() == "Small Bore?").FirstOrDefault();//其實應該要24
                                            if (getSmallBoreFlag != null && need24.DisplayValue != null)
                                            {
                                                SmallBoreFlag = need24.DisplayValue;
                                            }
                                            else if (getSmallBoreFlag != null && need24.ProbabilityLabel != null)
                                            {
                                                SmallBoreFlag = need24.ProbabilityLabel;
                                            }
                                            else if (getSmallBoreFlag != null && need24.ConsequenceLabel != null)
                                            {
                                                SmallBoreFlag = need24.ConsequenceLabel;
                                            }
                                            else if (getSmallBoreFlag != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                            {
                                                SmallBoreFlag = need24.Value;
                                            }
                                            else
                                            {
                                                var getNoBarrPenetrations = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                             && x.Label.ToString() == "Number of Barrier Penetrations").FirstOrDefault();//其實應該要24
                                                if (getNoBarrPenetrations != null && need24.DisplayValue != null)
                                                {
                                                    NoBarrPenetrations = need24.DisplayValue;
                                                }
                                                else if (getNoBarrPenetrations != null && need24.ProbabilityLabel != null)
                                                {
                                                    NoBarrPenetrations = need24.ProbabilityLabel;
                                                }
                                                else if (getNoBarrPenetrations != null && need24.ConsequenceLabel != null)
                                                {
                                                    NoBarrPenetrations = need24.ConsequenceLabel;
                                                }
                                                else if (getNoBarrPenetrations != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                {
                                                    NoBarrPenetrations = need24.Value;
                                                }
                                                else
                                                {
                                                    var getNoDmgdInsd = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                             && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                    if (getNoDmgdInsd != null && need24.DisplayValue != null)
                                                    {
                                                        NoDmgdInsd = need24.DisplayValue;
                                                    }
                                                    else if (getNoDmgdInsd != null && need24.ProbabilityLabel != null)
                                                    {
                                                        NoDmgdInsd = need24.ProbabilityLabel;
                                                    }
                                                    else if (getNoDmgdInsd != null && need24.ConsequenceLabel != null)
                                                    {
                                                        NoDmgdInsd = need24.ConsequenceLabel;
                                                    }
                                                    else if (getNoDmgdInsd != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                    {
                                                        NoDmgdInsd = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getNoDeadLegs = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                             && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                        if (getNoDeadLegs != null && need24.DisplayValue != null)
                                                        {
                                                            NoDeadLegs = need24.DisplayValue;
                                                        }
                                                        else if (getNoDeadLegs != null && need24.ProbabilityLabel != null)
                                                        {
                                                            NoDeadLegs = need24.ProbabilityLabel;
                                                        }
                                                        else if (getNoDeadLegs != null && need24.ConsequenceLabel != null)
                                                        {
                                                            NoDeadLegs = need24.ConsequenceLabel;
                                                        }
                                                        else if (getNoDeadLegs != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            NoDeadLegs = need24.Value;
                                                        }
                                                        else
                                                        {
                                                            var getNoElbows = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                             && x.Label.ToString() == "Number of Damaged Insulation").FirstOrDefault();//其實應該要24
                                                            if (getNoElbows != null && need24.DisplayValue != null)
                                                            {
                                                                NoElbows = need24.DisplayValue;
                                                            }
                                                            else if (getNoElbows != null && need24.ProbabilityLabel != null)
                                                            {
                                                                NoElbows = need24.ProbabilityLabel;
                                                            }
                                                            else if (getNoElbows != null && need24.ConsequenceLabel != null)
                                                            {
                                                                NoElbows = need24.ConsequenceLabel;
                                                            }
                                                            else if (getNoElbows != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                NoElbows = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getNoErosionZones = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Erosion Zones").FirstOrDefault();//其實應該要24
                                                                if (getNoErosionZones != null && need24.DisplayValue != null)
                                                                {
                                                                    NoErosionZones = need24.DisplayValue;
                                                                }
                                                                else if (getNoErosionZones != null && need24.ProbabilityLabel != null)
                                                                {
                                                                    NoErosionZones = need24.ProbabilityLabel;
                                                                }
                                                                else if (getNoErosionZones != null && need24.ConsequenceLabel != null)
                                                                {
                                                                    NoErosionZones = need24.ConsequenceLabel;
                                                                }
                                                                else if (getNoErosionZones != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                {
                                                                    NoErosionZones = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getNoHorizLowPts = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Horizontal Low Points").FirstOrDefault();//其實應該要24
                                                                    if (getNoHorizLowPts != null && need24.DisplayValue != null)
                                                                    {
                                                                        NoHorizLowPts = need24.DisplayValue;
                                                                    }
                                                                    else if (getNoHorizLowPts != null && need24.ProbabilityLabel != null)
                                                                    {
                                                                        NoHorizLowPts = need24.ProbabilityLabel;
                                                                    }
                                                                    else if (getNoHorizLowPts != null && need24.ConsequenceLabel != null)
                                                                    {
                                                                        NoHorizLowPts = need24.ConsequenceLabel;
                                                                    }
                                                                    else if (getNoHorizLowPts != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                    {
                                                                        NoHorizLowPts = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getNoInsdTerminators = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Insulation Terminations").FirstOrDefault();//其實應該要24
                                                                        if (getNoInsdTerminators != null && need24.DisplayValue != null)
                                                                        {
                                                                            NoInsdTerminators = need24.DisplayValue;
                                                                        }
                                                                        else if (getNoInsdTerminators != null && need24.ProbabilityLabel != null)
                                                                        {
                                                                            NoInsdTerminators = need24.ProbabilityLabel;
                                                                        }
                                                                        else if (getNoInsdTerminators != null && need24.ConsequenceLabel != null)
                                                                        {
                                                                            NoInsdTerminators = need24.ConsequenceLabel;
                                                                        }
                                                                        else if (getNoInsdTerminators != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                        {
                                                                            NoInsdTerminators = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getNoLongHorizRuns = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Long Horizontal Runs").FirstOrDefault();//其實應該要24
                                                                            if (getNoLongHorizRuns != null && need24.DisplayValue != null)
                                                                            {
                                                                                NoLongHorizRuns = need24.DisplayValue;
                                                                            }
                                                                            else if (getNoLongHorizRuns != null && need24.ProbabilityLabel != null)
                                                                            {
                                                                                NoLongHorizRuns = need24.ProbabilityLabel;
                                                                            }
                                                                            else if (getNoLongHorizRuns != null && need24.ConsequenceLabel != null)
                                                                            {
                                                                                NoLongHorizRuns = need24.ConsequenceLabel;
                                                                            }
                                                                            else if (getNoLongHorizRuns != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                            {
                                                                                NoLongHorizRuns = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getNoReducers = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Reducers").FirstOrDefault();//其實應該要24
                                                                                if (getNoReducers != null && need24.DisplayValue != null)
                                                                                {
                                                                                    NoReducers = need24.DisplayValue;
                                                                                }
                                                                                else if (getNoReducers != null && need24.ProbabilityLabel != null)
                                                                                {
                                                                                    NoReducers = need24.ProbabilityLabel;
                                                                                }
                                                                                else if (getNoReducers != null && need24.ConsequenceLabel != null)
                                                                                {
                                                                                    NoReducers = need24.ConsequenceLabel;
                                                                                }
                                                                                else if (getNoReducers != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                {
                                                                                    NoReducers = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getNoSoilToAirIntfs = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Soil-Air Interface").FirstOrDefault();//其實應該要24
                                                                                    if (getNoSoilToAirIntfs != null && need24.DisplayValue != null)
                                                                                    {
                                                                                        NoSoilToAirIntfs = need24.DisplayValue;
                                                                                    }
                                                                                    else if (getNoSoilToAirIntfs != null && need24.ProbabilityLabel != null)
                                                                                    {
                                                                                        NoSoilToAirIntfs = need24.ProbabilityLabel;
                                                                                    }
                                                                                    else if (getNoSoilToAirIntfs != null && need24.ConsequenceLabel != null)
                                                                                    {
                                                                                        NoSoilToAirIntfs = need24.ConsequenceLabel;
                                                                                    }
                                                                                    else if (getNoSoilToAirIntfs != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                    {
                                                                                        NoSoilToAirIntfs = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getNoTees = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Tees").FirstOrDefault();//其實應該要24
                                                                                        if (getNoTees != null && need24.DisplayValue != null)
                                                                                        {
                                                                                            NoTees = need24.DisplayValue;
                                                                                        }
                                                                                        else if (getNoTees != null && need24.ProbabilityLabel != null)
                                                                                        {
                                                                                            NoTees = need24.ProbabilityLabel;
                                                                                        }
                                                                                        else if (getNoTees != null && need24.ConsequenceLabel != null)
                                                                                        {
                                                                                            NoTees = need24.ConsequenceLabel;
                                                                                        }
                                                                                        else if (getNoTees != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                        {
                                                                                            NoTees = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getNoVertRuns = get31.Where(x => x.GroupName.ToString() == "Pipe Locations"
                                                                                                 && x.Label.ToString() == "Number of Vertical Runs").FirstOrDefault();//其實應該要24
                                                                                            if (getNoVertRuns != null && need24.DisplayValue != null)
                                                                                            {
                                                                                                NoVertRuns = need24.DisplayValue;
                                                                                            }
                                                                                            else if (getNoVertRuns != null && need24.ProbabilityLabel != null)
                                                                                            {
                                                                                                NoVertRuns = need24.ProbabilityLabel;
                                                                                            }
                                                                                            else if (getNoVertRuns != null && need24.ConsequenceLabel != null)
                                                                                            {
                                                                                                NoVertRuns = need24.ConsequenceLabel;
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

                                            DikeArea = Convert.ToDouble(need24.DisplayValue)*10.7639;
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
                                                    Inventory = Convert.ToDouble(need24.DisplayValue)* 0.45359237;
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
                                                        OperatingPressure = Convert.ToDouble(need24.DisplayValue)* 0.0689476;
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
                                                            OperatingTemp = Convert.ToDouble(need24.ProbabilityLabel);
                                                        }
                                                        else if (getOperatingTemp != null && need24.ConsequenceLabel != null)
                                                        {
                                                            OperatingTemp = Convert.ToDouble(need24.ConsequenceLabel);
                                                            
                                                        }
                                                        else if (getOperatingTemp != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            OperatingTemp = Convert.ToDouble(need24.Value);
                                                            
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
                                                
                                                ExtExpecedCorrRate = Convert.ToDouble(need24.DisplayValue)* 25.4;
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
                                                            OperatingTemperature = need24.DisplayValue;
                                                        }
                                                        else if (getOperatingTemperature != null && need24.ProbabilityLabel != null)
                                                        {
                                                            OperatingTemperature = need24.ProbabilityLabel;
                                                        }
                                                        else if (getOperatingTemperature != null && need24.ConsequenceLabel != null)
                                                        {
                                                            OperatingTemperature = need24.ConsequenceLabel;
                                                        }
                                                        else if (getOperatingTemperature != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                        {
                                                            OperatingTemperature = need24.Value;
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
                                break;



                            case "FLUID DATA":
                                var getAIT = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                && x.Label.ToString() == "AIT").FirstOrDefault();
                                if (getAIT != null && need24.DisplayValue != null)
                                {
                                    AIT = need24.DisplayValue;
                                }
                                else if (getAIT != null && need24.ProbabilityLabel != null)
                                {
                                    AIT = need24.ProbabilityLabel;
                                }
                                else if (getAIT != null && need24.ConsequenceLabel != null)
                                {
                                    AIT = need24.ConsequenceLabel;
                                }
                                else if (getAIT != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                {
                                    AIT = need24.Value;
                                }
                                else
                                {
                                    var getBoilingPoint = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                        && x.Label.ToString() == "Boiling Point").FirstOrDefault();//其實應該要24
                                    if (getBoilingPoint != null && need24.DisplayValue != null)
                                    {
                                        BoilingPoint = need24.DisplayValue;
                                    }
                                    else if (getBoilingPoint != null && need24.ProbabilityLabel != null)
                                    {
                                        BoilingPoint = need24.ProbabilityLabel;
                                    }
                                    else if (getBoilingPoint != null && need24.ConsequenceLabel != null)
                                    {
                                        BoilingPoint = need24.ConsequenceLabel;
                                    }
                                    else if (getBoilingPoint != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                    {
                                        BoilingPoint = need24.Value;
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
                                                ToxicBP = need24.DisplayValue;
                                            }
                                            else if (getToxicBP != null && need24.ProbabilityLabel != null)
                                            {
                                                ToxicBP = need24.ProbabilityLabel;
                                            }
                                            else if (getToxicBP != null && need24.ConsequenceLabel != null)
                                            {
                                                ToxicBP = need24.ConsequenceLabel;
                                            }
                                            else
                                            {
                                                ToxicBP = need24.Value;
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
                                            IntExpectedCorrRate=Convert.ToDouble(need24.DisplayValue) * 25.4;
                                            
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
                                                                DesignTemp = Convert.ToDouble(need24.DisplayValue) * 25.4;
                                                                
                                                            }
                                                            else if (getDesignTemp != null && need24.ProbabilityLabel != null)
                                                            {
                                                                DesignTemp = Convert.ToDouble(need24.ProbabilityLabel) * 25.4;
                                                                
                                                            }
                                                            else if (getDesignTemp != null && need24.ConsequenceLabel != null)
                                                            {
                                                                DesignTemp = Convert.ToDouble(need24.ConsequenceLabel) * 25.4;
                                                                
                                                            }
                                                            else if (getDesignTemp != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                            {
                                                                DesignTemp = Convert.ToDouble(need24.Value) * 25.4;
                                                                
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
                                                                                                                    if (getEstMinThicknessFlag != null && need24.DisplayValue != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = need24.DisplayValue;
                                                                                                                    }
                                                                                                                    else if (getEstMinThickness != null && need24.ProbabilityLabel != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = need24.ProbabilityLabel;
                                                                                                                    }
                                                                                                                    else if (getEstMinThickness != null && need24.ConsequenceLabel != null)
                                                                                                                    {
                                                                                                                        EstMinThickness = need24.ConsequenceLabel;
                                                                                                                    }

                                                                                                                    else if (getEstMinThickness != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                                    {
                                                                                                                        EstMinThickness = need24.Value;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        var getIntEstWallRemain = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                                    && x.Label.ToString() == "Estimated Wall Remaining").FirstOrDefault();
                                                                                                                        if (getIntEstWallRemain != null && need24.DisplayValue != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = need24.DisplayValue;
                                                                                                                        }
                                                                                                                        else if (getIntEstWallRemain != null && need24.ProbabilityLabel != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = need24.ProbabilityLabel;
                                                                                                                        }
                                                                                                                        else if (getIntEstWallRemain != null && need24.ConsequenceLabel != null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = need24.ConsequenceLabel;
                                                                                                                        }

                                                                                                                        else if (getIntEstWallRemain != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                                        {
                                                                                                                            IntEstWallRemain = need24.Value;
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            var getEstHalfLife = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Calculations"
                                                                   && x.Label.ToString() == "Estimated Half Life").FirstOrDefault();
                                                                                                                            if (getEstHalfLife != null && need24.DisplayValue != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.DisplayValue;
                                                                                                                            }
                                                                                                                            else if (getEstHalfLife != null && need24.ProbabilityLabel != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.ProbabilityLabel;
                                                                                                                            }
                                                                                                                            else if (getEstHalfLife != null && need24.ConsequenceLabel != null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.ConsequenceLabel;
                                                                                                                            }

                                                                                                                            else if (getEstHalfLife != null && need24.DisplayValue == null && need24.ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                                                            {
                                                                                                                                EstHalfLife = need24.Value;
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
                        //need24.OperatingTemperature = OperatingTemperature;
                        need24.CUIFlag = CUIFlag;
                        need24.ExtInspConf = ExtInspConf;
                        need24.ExtLastInspDate = ExtLastInspDate;
                        need24.ExtNoOfInsp = ExtNoOfInsp;
                        need24.ExtCoating = ExtCoating;
                        need24.InsulatedFlag = InsulatedFlag;
                        need24.InsulationCondition = InsulationCondition;
                        need24.InsulationType = InsulationType;

                        need24.OverideAllowableStress = OverideAllowableStress;
                        //need24.BoilingPoint = BoilingPoint;
                        need24.FluidType = FluidType;
                        //need24.ToxicBP = ToxicBP;
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
                       // need24.EstMinThickness = EstMinThickness;
                        //need24.IntEstWallRemain = IntEstWallRemain;
                        need24.EstHalfLife = EstHalfLife;
                        need24.RepThick = RepThick;





                    }
                            return Ok(DataList_T0NKME06model);
                        }

                    }
                


                        

                    
                    //return Json(DataList_T0NKME02model.Take(100).ToList());



                    //var fileName = "T0NKME01" + Guid.NewGuid().ToString() + ".xlsx";
                    //var guidFileName = DataList_T0NKME02model.ExportExcel<T0NKME02model>(fileName, "sheet");

                   // return Ok(DataList_T0NKME02model);
                
            



            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }




        
        }
}
}
