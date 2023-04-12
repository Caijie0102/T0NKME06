using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.PTG;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebPages;
using T0NKME06.Extensions;
using T0NKME06.Models;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

namespace T0NKME06.Controllers
{
    public class T0NKME07Controller : ApiController
    {
        // GET: T0NKME07
        public IHttpActionResult Get()
        {
            List<T0NKME07model> DataList_T0NKME07model = new List<T0NKME07model>();
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
                                          join cd in db.ComponentDetails on otn.ComponentDetailsId equals cd.ComponentDetailsId
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
                                              OverallRisk = (from dim in db.DimModelRunOverallRisk
                                                             where otmr.OrgTreeNodeModelRunId.ToString() == dim.OrgTreeNodeModelRunId.ToString()
                                                             select dim.OverallRisk).FirstOrDefault(),
                                              //37
                                              ModelName = (from rm in db.RunnableModel
                                                           where otmr.ModelId.ToString() == rm.ModelId.ToString()
                                                           select rm.ModelName).FirstOrDefault(),

                                              ComponentDetailsId =cd.ComponentDetailsId,


                                              //ComponentDetailsId = componentdetails.ComponentDetailsId.ToString(),

                                              OrgTreeNodeModelRunId32 = otmr.OrgTreeNodeModelRunId.ToString(),
                                              NodeInputId = otmri.NodeInputId.ToString(),  //24
                                              NodeOutputId = otmro.NodeOutputId.ToString(),  //25

                                              ModelNodeId2429 = otmri.ModelNodeId.ToString(),
                                              ModelNodeId2529 = otmro.ModelNodeId.ToString(),


                                              NodeInputId31 = (from nim in db.NodeInputMetadata
                                                               where otmri.NodeInputId.ToString() == nim.NodeInputId.ToString()
                                                               select nim.NodeInputId).FirstOrDefault(),


                                              NodeOutputId30 = (from nom in db.NodeOutputMetadata
                                                                where otmro.NodeOutputId.ToString() == nom.NodeOutputId.ToString()
                                                                select nom.NodeOutputId).FirstOrDefault(),


                                              Value24 = otmri.Value,
                                              Value25 = otmro.Value,

                                              OrgTreeNodeId32 = otmr.OrgTreeNodeId.ToString(), //otmr與otn連接的
                                              ParentId = otn.ParentId.ToString(),   //otn的

                                              ExtCalcCorrRate = (from nomd in db.NodeOutputMetadata
                                                                 join otmro in db.OrgTreeNodeModelRunOutputs on nomd.NodeOutputId equals otmro.NodeOutputId
                                                                 join mnm in db.ModelNodeMetadata on otmro.ModelNodeId equals mnm.ModelNodeId
                                                                 where nomd.GroupName.ToString() == "Corrosion Information" && nomd.Label.ToString() == "Calculated Corrosion Rate" && mnm.NodeLabel == "EXTERNAL CORROSION"
                                                                 select otmro.Value).FirstOrDefault(),

                                              JacketedFlag = (from nimd in db.NodeInputMetadata
                                                              join otmri in db.OrgTreeNodeModelRunInputs on nimd.NodeInputId equals otmri.NodeInputId
                                                              join mnm in db.ModelNodeMetadata on otmri.ModelNodeId equals mnm.ModelNodeId
                                                              where mnm.NodeLabel == "ADDITIONAL INFORMATION" && nimd.GroupName.ToString() == "Other Data" && nimd.Label.ToString().Contains("Jacket")
                                                              select otmri.Value).FirstOrDefault(),


                                              IntEntryPossFlag = (from nimd in db.NodeInputMetadata
                                                                  join otmri in db.OrgTreeNodeModelRunInputs on nimd.NodeInputId equals otmri.NodeInputId
                                                                  join mnm in db.ModelNodeMetadata on otmri.ModelNodeId equals mnm.ModelNodeId
                                                                  where mnm.NodeLabel == "ADDITIONAL INFORMATION" && (nimd.GroupName.ToString() == "Column Locations" || nimd.GroupName.ToString() == "Exchanger Locations" || nimd.GroupName.ToString() == "Pipe Locations"
                                                                         || nimd.GroupName.ToString() == "Tank Locations" || nimd.GroupName.ToString() == "Vessel Locations")
                                                                         && nimd.Label.ToString() == "Internal Entry Possible"
                                                                  select otmri.Value).FirstOrDefault(),





                                          };
                   // return Ok(getAllComponent.Take(100).ToList());



                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var W0NKME26 = db.RunnableLookupRowDisplayValues.ToList();  //0筆
                    var W0NKME27 = db.RiskMatrixProbabilityVectors.ToList();
                    var W0NKME28 = db.RiskMatrixConsequenceVectors.ToList();


                    var fake = getAllComponent
                  //.AsEnumerable() //19
                  .Take(20000);
                    //.ToList(); // 将 LINQ 对象转换为列表对象


                    //parallel可同時處理多筆資料
                    Parallel.ForEach(fake, item =>
                    { //var getComponent = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == item.OrgTreeNodeId32.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();

                     var getComponent = W0NKME03.Where(x => x.ComponentDetailsId.ToString() == item.ComponentDetailsId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault(); ;
                        if (getComponent != null)
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

                                                    DataList_T0NKME07model.Add(new T0NKME07model()
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
                                                        ComponentId = getComponent.OrgTreeNodeId.ToString(),
                                                        Component = getComponent.Name,

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

                    //return Ok(DataList_T0NKME07model);

                    var W0NKME31 = db.NodeInputMetadata.ToList();
                    var W0NKME30 = db.NodeOutputMetadata.ToList();


                    //Double DikeArea;

                    foreach (var need24 in DataList_T0NKME07model)
                    {
                        string BundleExtCorrRate=" ", BundleIntCorrRate=" ", DetectionTime=" ", DikedFlag=" ", IsolationTime=" ",   RepFluid="", ProductionLoss="", PercentToxic=" ", ToxicFluid=" ", ToxicMixtureFlag="", EnvCrckgInspConf="", EnvCrckgLastInspDate="", EnvCrckgNoOfInsp="", EnvCrckgServDate="", EnvCrckgMech="", Humidity="", ExtWettingFlag="", ExtCorrosionOption="",
                             ExtServDate="",  CUIFlag="", ExtInspConf="",ExtLastInspDate="", ExtNoOfInsp="", ExtCoating="", InsulatedFlag="", InsulationCondition="", InsulationType="",
                             FluidType="",   IntCorrosionType="",   CompType="",   IntServDate="",
                            IntInpsConf="", IntLastInspDate="", IntNoOfInps="", ConstCode="", JointEfficiency="",  OverideAllowableStressFlag="", AnalysisDate="", IntCorrosionOption = "" ,
                            EstMinThicknessFlag ="",   
                    ODM1 = " ", ODM2 = " ", ODM3 = " ", ODM4 = " ", ODM5 = " ", ODM1Potential = " ", ODM2Potential = " ", ODM3Potential = " ", ODM4Potential = " ", ODM5Potential = " ", ODM1Probability = " ", ODM2Probability = " ", ODM3Probability = " ", ODM4Probability = " ", ODM5Probability = " ", ODM1Comment = " ", ODM2Comment = " ", ODM3Comment = " ", ODM4Comment = " ", ODM5Comment = " ", EstHalfLife = " ";


                        double DikeArea = 0, Inventory = 0, OperatingPressure=0, ExtExpecedCorrRate=0, ExtMeasuredCorrRate = 0, ExtCalcCorrRate = 0, OverideAllowableStress = 0
                            ,OperatingTemp = 0, AIT = 0, BoilingPoint = 0, ToxicBP = 0,  IntExpectedCorrRate = 0, IntLTCorrRate = 0, IntSTCorrRate =0, DesignPressure = 0,
                            DesignTemp = 0, Diameter = 0, EstMinThickness = 0, RepThick = 0, IntEstWallRemain = 0, EstMinThickness2 = 0;
                        //31
                        var get31 = W0NKME31.Where(x => x.NodeInputId.ToString() == need24.NodeInputId.ToString()).Select(x => new { x.GroupName, x.Label, need24.Value, x.NodeInputId }).ToList();
                        var get30 = W0NKME30.Where(x => x.NodeOutputId.ToString() == need24.NodeOutputId.ToString()).Select(x => new { x.GroupName, x.Label }).ToList();

                        var DisplayValue = W0NKME26.Where(x => x.LookupRowId.ToString() == need24.Value.ToString()).Select(x => new { x.DisplayValue }).FirstOrDefault();
                        var ProbabilityLabel = W0NKME27.Where(x => x.Index.ToString() == need24.Value.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        var ConsequenceLabel = W0NKME28.Where(x => x.Index.ToString() == need24.Value.ToString()).Select(x => new { x.Label }).FirstOrDefault();


                        var DisplayValue25 = W0NKME26.Where(x => x.LookupRowId.ToString() == need24.Value25.ToString()).Select(x => new { x.DisplayValue }).FirstOrDefault();
                        var ProbabilityLabel25 = W0NKME27.Where(x => !need24.Value25.IsEmpty() && x.Index.ToString() == need24.Value25.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        var ConsequenceLabel25 = W0NKME28.Where(x => !need24.Value25.IsEmpty() && x.Index.ToString() == need24.Value25.ToString()).Select(x => new { x.Label }).FirstOrDefault();
                        //!x.GroupName.IsEmpty() &&
                        switch (db.ModelNodeMetadata.Where(x => x.ModelNodeId.ToString() == need24.ModelNodeId.ToString()).FirstOrDefault().NodeLabel) //24==29
                        {
                            

                            case "CONSEQUENCE":

                                var getDetectionTime = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation" && x.Label.ToString() == "Detection Time").FirstOrDefault();
                                if (getDetectionTime != null && need24.DisplayValue != null)
                                {
                                    DetectionTime = DisplayValue.DisplayValue;
                                }
                                else if (getDetectionTime != null && ProbabilityLabel != null)
                                {
                                    DetectionTime = ProbabilityLabel.Label;
                                }
                                else if (getDetectionTime != null && ConsequenceLabel != null)
                                {
                                    DetectionTime = ConsequenceLabel.Label;
                                }
                                else if (getDetectionTime != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    DetectionTime = need24.Value;
                                }
                                else
                                {
                                    var getIsolationTime = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Isolation Time").FirstOrDefault();
                                    if (getIsolationTime != null && DisplayValue != null)
                                    {
                                        IsolationTime = DisplayValue.DisplayValue;
                                    }
                                    else if (getIsolationTime != null && ProbabilityLabel != null)
                                    {
                                        IsolationTime = ProbabilityLabel.Label;
                                    }
                                    else if (getIsolationTime != null && ConsequenceLabel != null)
                                    {
                                        IsolationTime = ConsequenceLabel.Label;
                                    }
                                    else if (getIsolationTime != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        IsolationTime = need24.Value;
                                    }
                                    else
                                    {

                                        var getDikeArea = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Dike Area").FirstOrDefault();
                                        if (getDikeArea != null && DisplayValue != null)
                                        {//依備註7原則顯示資料x10.7639

                                            DikeArea = Convert.ToDouble(DisplayValue.DisplayValue) * 10.7639;
                                        }
                                        else if (getDikeArea != null && ProbabilityLabel != null)
                                        {
                                            DikeArea = Convert.ToDouble(ProbabilityLabel.Label) * 10.7639;

                                        }
                                        else if (getDikeArea != null && ConsequenceLabel != null)
                                        {
                                            DikeArea = Convert.ToDouble(ConsequenceLabel.Label) * 10.7639;

                                        }
                                        else if (getDikeArea != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            DikeArea = Convert.ToDouble(need24.Value) * 10.7639;

                                        }
                                        else
                                        {
                                            var getDikedFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Mitigation"
                                                                         && x.Label.ToString() == "Dike Area?").FirstOrDefault();
                                            if (getDikedFlag != null && DisplayValue != null)
                                            {
                                                DikedFlag = DisplayValue.DisplayValue;
                                            }
                                            else if (getDikedFlag != null && ProbabilityLabel != null)
                                            {
                                                DikedFlag = ProbabilityLabel.Label;
                                            }
                                            else if (getDikedFlag != null && ConsequenceLabel != null)
                                            {
                                                DikedFlag = ConsequenceLabel.Label;
                                            }
                                            else if (getDikedFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                DikedFlag = need24.Value;
                                            }
                                            else
                                            {
                                                var getInventory = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Inventory").FirstOrDefault();
                                                if (getInventory != null && DisplayValue != null)
                                                {
                                                    Inventory = Convert.ToDouble(DisplayValue.DisplayValue) * 0.45359237;
                                                }
                                                else if (getInventory != null && ProbabilityLabel != null)
                                                {
                                                    Inventory = Convert.ToDouble(ProbabilityLabel.Label) * 0.45359237;

                                                }
                                                else if (getInventory != null && ConsequenceLabel != null)
                                                {
                                                    Inventory = Convert.ToDouble(ConsequenceLabel.Label) * 0.45359237;

                                                }
                                                else if (getInventory != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    Inventory = Convert.ToDouble(need24.Value) * 0.45359237;

                                                }
                                                else
                                                {
                                                    var getOperatingPressure = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Operating Pressure").FirstOrDefault();
                                                    if (getOperatingPressure != null && DisplayValue != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(DisplayValue.DisplayValue) * 0.0689476;
                                                    }
                                                    else if (getOperatingPressure != null && ProbabilityLabel != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(ProbabilityLabel.Label) * 0.0689476;


                                                    }
                                                    else if (getOperatingPressure != null && ConsequenceLabel != null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(ConsequenceLabel.Label) * 0.0689476;

                                                    }
                                                    else if (getOperatingPressure != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                    {
                                                        OperatingPressure = Convert.ToDouble(need24.Value) * 0.0689476;

                                                    }
                                                    
                                                       
                                                        else
                                                        {
                                                            var getRepFluid = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                 && x.Label.ToString() == "Representative Fluid").FirstOrDefault();
                                                            if (getRepFluid != null && DisplayValue != null)
                                                            {
                                                                RepFluid = DisplayValue.DisplayValue;
                                                            }
                                                            else if (getRepFluid != null && ProbabilityLabel != null)
                                                            {
                                                                RepFluid = ProbabilityLabel.Label;
                                                            }
                                                            else if (getRepFluid != null && ConsequenceLabel != null)
                                                            {
                                                                RepFluid = ConsequenceLabel.Label;
                                                            }
                                                            else if (getRepFluid != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                            {
                                                                RepFluid = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getProductionLoss = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Process Data"
                                                                         && x.Label.ToString() == "Production Loss").FirstOrDefault();
                                                                if (getProductionLoss != null && DisplayValue != null)
                                                                {
                                                                    ProductionLoss = DisplayValue.DisplayValue;
                                                                }
                                                                else if (getProductionLoss != null && ProbabilityLabel != null)
                                                                {
                                                                    ProductionLoss = ProbabilityLabel.Label;
                                                                }
                                                                else if (getProductionLoss != null && ConsequenceLabel != null)
                                                                {
                                                                    ProductionLoss = ConsequenceLabel.Label;
                                                                }
                                                                else if (getProductionLoss != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                {
                                                                    ProductionLoss = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getPercentToxic = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Percent Toxic").FirstOrDefault();
                                                                    if (getPercentToxic != null && DisplayValue != null)
                                                                    {
                                                                        PercentToxic = DisplayValue.DisplayValue;
                                                                    }
                                                                    else if (getPercentToxic != null && ProbabilityLabel != null)
                                                                    {
                                                                        PercentToxic = ProbabilityLabel.Label;
                                                                    }
                                                                    else if (getPercentToxic != null && ConsequenceLabel != null)
                                                                    {
                                                                        PercentToxic = ConsequenceLabel.Label;
                                                                    }
                                                                    else if (getPercentToxic != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                    {
                                                                        PercentToxic = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getToxicFluid = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Toxic Fluid").FirstOrDefault();
                                                                        if (getToxicFluid != null && DisplayValue != null)
                                                                        {
                                                                            ToxicFluid = DisplayValue.DisplayValue;
                                                                        }
                                                                        else if (getToxicFluid != null && ProbabilityLabel != null)
                                                                        {
                                                                            ToxicFluid = ProbabilityLabel.Label;
                                                                        }
                                                                        else if (getToxicFluid != null && ConsequenceLabel != null)
                                                                        {
                                                                            ToxicFluid = ConsequenceLabel.Label;
                                                                        }
                                                                        else if (getToxicFluid != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            ToxicFluid = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getToxicMixtureFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Toxic Mixture"
                                                                         && x.Label.ToString() == "Toxic Mixture?").FirstOrDefault();
                                                                            if (getToxicMixtureFlag != null && DisplayValue != null)
                                                                            {
                                                                                ToxicMixtureFlag = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getToxicMixtureFlag != null && ProbabilityLabel != null)
                                                                            {
                                                                                ToxicMixtureFlag = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getToxicMixtureFlag != null && ConsequenceLabel != null)
                                                                            {
                                                                                ToxicMixtureFlag = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getToxicMixtureFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            ToxicMixtureFlag = need24.Value;
                                                                        }

                                                                            else
                                                                            {
                                                                                
                                                                            var getBundleExtCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion"
                                                                         && x.Label.ToString() == "Ext. Corrosion Rate").FirstOrDefault();
                                                                            if (getBundleExtCorrRate != null && DisplayValue != null)
                                                                            {
                                                                                BundleExtCorrRate = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getBundleExtCorrRate != null && ProbabilityLabel != null)
                                                                            {
                                                                                BundleExtCorrRate = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getBundleExtCorrRate != null && ConsequenceLabel != null)
                                                                            {
                                                                                BundleExtCorrRate = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getBundleExtCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                            {
                                                                                BundleExtCorrRate = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getBundleIntCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion"
                                                                                                                                                         && x.Label.ToString() == "Int. Corrosion Rate").FirstOrDefault();
                                                                                if (getBundleIntCorrRate != null && DisplayValue != null)
                                                                                {
                                                                                    BundleIntCorrRate = DisplayValue.DisplayValue;
                                                                                }
                                                                                else if (getBundleIntCorrRate != null && ProbabilityLabel != null)
                                                                                {
                                                                                    BundleIntCorrRate = ProbabilityLabel.Label;
                                                                                }
                                                                                else if (getBundleIntCorrRate != null && ConsequenceLabel != null)
                                                                                {
                                                                                    BundleIntCorrRate = ConsequenceLabel.Label;
                                                                                }
                                                                                else if (getBundleIntCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                {
                                                                                    BundleIntCorrRate = need24.Value;
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

                            case "ENVIRONMENTAL CRACKING":
                                var getEnvCrckgInspConf = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Inspection Information"
                                                                 && x.Label.ToString() == "Confidence").FirstOrDefault();
                                if (getEnvCrckgInspConf != null && DisplayValue != null)
                                {
                                    EnvCrckgInspConf = DisplayValue.DisplayValue;
                                }
                                else if (getEnvCrckgInspConf != null && ProbabilityLabel != null)
                                {
                                    EnvCrckgInspConf = ProbabilityLabel.Label;
                                }
                                else if (getEnvCrckgInspConf != null && ConsequenceLabel != null)
                                {
                                    EnvCrckgInspConf = ConsequenceLabel.Label;
                                }
                                else if (getEnvCrckgInspConf != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    EnvCrckgInspConf = need24.Value;
                                }
                                else
                                {
                                    var getEnvCrckgLastInspDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Inspection Information"
                                                                 && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                    if (getEnvCrckgLastInspDate != null && DisplayValue != null)
                                    {
                                        EnvCrckgLastInspDate = DisplayValue.DisplayValue;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && ProbabilityLabel != null)
                                    {
                                        EnvCrckgLastInspDate = ProbabilityLabel.Label;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && ConsequenceLabel != null)
                                    {
                                        EnvCrckgLastInspDate = ConsequenceLabel.Label;
                                    }
                                    else if (getEnvCrckgLastInspDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        EnvCrckgLastInspDate = need24.Value;
                                    }
                                    else
                                    {

                                        var getEnvCrckgServDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Potential for Cracking"
                                                                         && x.Label.ToString() == "Environmental Cracking Date in Service").FirstOrDefault();//其實應該要24
                                        if (getEnvCrckgServDate != null && DisplayValue != null)
                                        {
                                            EnvCrckgServDate = DisplayValue.DisplayValue;
                                        }
                                        else if (getEnvCrckgServDate != null && ProbabilityLabel != null)
                                        {
                                            EnvCrckgServDate = ProbabilityLabel.Label;
                                        }
                                        else if (getEnvCrckgServDate != null && ConsequenceLabel != null)
                                        {
                                            EnvCrckgServDate = ConsequenceLabel.Label;
                                        }
                                        else if (getEnvCrckgServDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            EnvCrckgServDate = need24.Value;
                                        }
                                        else
                                        {
                                            var getEnvCrckgMech = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Potential for Cracking"
                                                                         && (x.Label.ToString()== "Environmental Cracking Mechanism ~ Initial Potential")
                                                                         || x.Label.ToString() == "Environmental Cracking Mechanism").FirstOrDefault();//其實應該要24
                                            if (getEnvCrckgMech != null && DisplayValue != null)
                                            {
                                                EnvCrckgMech = DisplayValue.DisplayValue;
                                            }
                                            else if (getEnvCrckgMech != null && ProbabilityLabel != null)
                                            {
                                                EnvCrckgMech = ProbabilityLabel.Label;
                                            }
                                            else if (getEnvCrckgMech != null && ConsequenceLabel != null)
                                            {
                                                EnvCrckgMech = ConsequenceLabel.Label;
                                            }
                                            else if(getEnvCrckgMech != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                EnvCrckgMech = need24.Value;
                                            }
                                            else
                                            {
                                                var getEnvCrckgNoOfInsp = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Inspection Information"
                                                                         && x.Label.ToString()=="No. of Inspection").FirstOrDefault();//其實應該要24
                                                if (getEnvCrckgNoOfInsp != null && DisplayValue != null)
                                                {
                                                    EnvCrckgNoOfInsp = DisplayValue.DisplayValue;
                                                }
                                                else if (getEnvCrckgNoOfInsp != null && ProbabilityLabel != null)
                                                {
                                                    EnvCrckgNoOfInsp = ProbabilityLabel.Label;
                                                }
                                                else if (getEnvCrckgNoOfInsp != null && ConsequenceLabel != null)
                                                {
                                                    EnvCrckgNoOfInsp = ConsequenceLabel.Label;
                                                }
                                                else if (getEnvCrckgNoOfInsp != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    EnvCrckgNoOfInsp = need24.Value;
                                                }
                                            }



                                        }
                                    }
                                }
                                break;


                            case "EXTERNAL CORROSION":
                                var getHumidity = get31.Where(x => x.GroupName.ToString() == "Atmospheric Influence"
                                                                 && x.Label.ToString() == "Humidity").FirstOrDefault();
                                if (getHumidity != null && DisplayValue != null)
                                {
                                    Humidity = DisplayValue.DisplayValue;
                                }
                                else if (getHumidity != null && ProbabilityLabel != null)
                                {
                                    Humidity = ProbabilityLabel.Label;
                                }
                                else if (getHumidity != null && ConsequenceLabel != null)
                                {
                                    Humidity = ConsequenceLabel.Label;
                                }
                                else if (getHumidity != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    Humidity = need24.Value;
                                }
                                else
                                {
                                    var getExtWettingFlag = get31.Where(x => x.GroupName.ToString() == "Atmospheric Influence"
                                                                         && x.Label.ToString() == "Near Cool Tower/Wetting?").FirstOrDefault();//其實應該要24
                                    if (getExtWettingFlag != null && DisplayValue != null)
                                    {
                                        ExtWettingFlag = DisplayValue.DisplayValue;
                                    }
                                    else if (getExtWettingFlag != null && ProbabilityLabel != null)
                                    {
                                        ExtWettingFlag = ProbabilityLabel.Label;
                                    }
                                    else if (getExtWettingFlag != null && ConsequenceLabel != null)
                                    {
                                        ExtWettingFlag = ConsequenceLabel.Label;
                                    }
                                    else if (getExtWettingFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        ExtWettingFlag = need24.Value;
                                    }
                                    else
                                    {

                                        var getExtCorrosionOption = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Corrosion Option").FirstOrDefault();//其實應該要24
                                        if (getExtCorrosionOption != null && DisplayValue != null)
                                        {
                                            ExtCorrosionOption = DisplayValue.DisplayValue;
                                        }
                                        else if (getExtCorrosionOption != null && ProbabilityLabel != null)
                                        {
                                            ExtCorrosionOption = ProbabilityLabel.Label;
                                        }
                                        else if (getExtCorrosionOption != null && ConsequenceLabel != null)
                                        {
                                            ExtCorrosionOption = ConsequenceLabel.Label;
                                        }
                                        else if (getExtCorrosionOption != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            ExtCorrosionOption = need24.Value;
                                        }
                                        else
                                        {
                                            var getExtExpecedCorrRate = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Expected").FirstOrDefault();//其實應該要24
                                            if (getExtExpecedCorrRate != null && DisplayValue != null)
                                            {

                                                ExtExpecedCorrRate = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;
                                            }
                                            else if (getExtExpecedCorrRate != null && ProbabilityLabel != null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                            }
                                            else if (getExtExpecedCorrRate != null && ConsequenceLabel != null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                            }
                                            else if (getExtExpecedCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                ExtExpecedCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                            }
                                            else
                                            {//4:13/19
                                                var getExtMeasuredCorrRate = get31.Where(x => x.GroupName.ToString() == "Corrosion Information"
                                                                         && x.Label.ToString() == "Measured").FirstOrDefault();//其實應該要24
                                                if (getExtMeasuredCorrRate != null && DisplayValue != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && ProbabilityLabel != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && ConsequenceLabel != null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;
                                                }
                                                else if (getExtMeasuredCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    ExtMeasuredCorrRate = Convert.ToDouble(need24.Value) * 25.4;
                                                }
                                                else
                                                {
                                                    var getExtServDate = get31.Where(x => x.GroupName.ToString() == "Design Information"
                                                                         && x.Label.ToString() == "External Date in Service").FirstOrDefault();//其實應該要24
                                                    if (getExtServDate != null && DisplayValue != null)
                                                    {
                                                        ExtServDate = DisplayValue.DisplayValue;
                                                    }
                                                    else if (getExtServDate != null && ProbabilityLabel != null)
                                                    {
                                                        ExtServDate = ProbabilityLabel.Label;
                                                    }
                                                    else if (getExtServDate != null && ConsequenceLabel != null)
                                                    {
                                                        ExtServDate = ConsequenceLabel.Label;
                                                    }
                                                    else if (getExtServDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                    {
                                                        ExtServDate = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getOperatingTemperature = get31.Where(x => x.GroupName.ToString() == "Design Information"
                                                                         && x.Label.ToString() == "Operating Temperature").FirstOrDefault();//其實應該要24
                                                        if (getOperatingTemperature != null && DisplayValue != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(DisplayValue.DisplayValue);
                                                            OperatingTemp = ((OperatingTemperature1 - 32) * 5) / 9;
                                                        }
                                                        else if (getOperatingTemperature != null && ProbabilityLabel != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(ProbabilityLabel.Label);
                                                            OperatingTemp = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else if (getOperatingTemperature != null && ConsequenceLabel != null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(ConsequenceLabel.Label);
                                                            OperatingTemp = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else if (getOperatingTemperature != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                        {
                                                            var OperatingTemperature1 = Convert.ToDouble(need24.Value);
                                                            OperatingTemp = ((OperatingTemperature1 - 32) * 5) / 9;

                                                        }
                                                        else
                                                        {
                                                            var getCUIFlag = get31.Where(x => x.GroupName.ToString() == "Design Information "
                                                                         && x.Label.ToString() == "Susceptible to Corrosion?").FirstOrDefault();//其實應該要24
                                                            if (getCUIFlag != null && DisplayValue != null)
                                                            {
                                                                CUIFlag = DisplayValue.DisplayValue;
                                                            }
                                                            else if (getCUIFlag != null && ProbabilityLabel != null)
                                                            {
                                                                CUIFlag = ProbabilityLabel.Label;
                                                            }
                                                            else if (getCUIFlag != null && ConsequenceLabel != null)
                                                            {
                                                                CUIFlag = ConsequenceLabel.Label;
                                                            }
                                                            else if (getCUIFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                            {
                                                                CUIFlag = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getExtInspConf = get31.Where(x => (x.GroupName.ToString() == "External Inspection Information" || x.GroupName.ToString() == "Inspection Information")
                                                                         && x.Label.ToString() == "Confidence").FirstOrDefault();//其實應該要24
                                                                if (getExtInspConf != null && DisplayValue != null)
                                                                {
                                                                    ExtInspConf = DisplayValue.DisplayValue;
                                                                }
                                                                else if (getExtInspConf != null && ProbabilityLabel != null)
                                                                {
                                                                    ExtInspConf = ProbabilityLabel.Label;
                                                                }
                                                                else if (getExtInspConf != null && ConsequenceLabel != null)
                                                                {
                                                                    ExtInspConf = ConsequenceLabel.Label;
                                                                }
                                                                else if (getExtInspConf != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                {
                                                                    ExtInspConf = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getExtLastInspDate = get31.Where(x => (x.GroupName.ToString() == "External Inspection Information" || x.GroupName.ToString() == "Inspection Information")
                                                                         && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                                                    if (getExtLastInspDate != null && DisplayValue != null)
                                                                    {
                                                                        ExtLastInspDate = DisplayValue.DisplayValue;
                                                                    }
                                                                    else if (getExtLastInspDate != null && ProbabilityLabel != null)
                                                                    {
                                                                        ExtLastInspDate = ProbabilityLabel.Label;
                                                                    }
                                                                    else if (getExtLastInspDate != null && ConsequenceLabel != null)
                                                                    {
                                                                        ExtLastInspDate = ConsequenceLabel.Label;
                                                                    }
                                                                    else if (getExtLastInspDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                    {
                                                                        ExtLastInspDate = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getExtNoOfInsp = get31.Where(x => x.GroupName.ToString() == "External Inspection Information" || x.GroupName.ToString() == "Inspection Information"
                                                                         && x.Label.ToString() == "No. of Inspection").FirstOrDefault();//其實應該要24
                                                                        if (getExtNoOfInsp != null && DisplayValue != null)
                                                                        {
                                                                            ExtNoOfInsp = DisplayValue.DisplayValue;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && ProbabilityLabel != null)
                                                                        {
                                                                            ExtNoOfInsp = ProbabilityLabel.Label;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && ConsequenceLabel != null)
                                                                        {
                                                                            ExtNoOfInsp = ConsequenceLabel.Label;
                                                                        }
                                                                        else if (getExtNoOfInsp != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            ExtNoOfInsp = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getExtCoating = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Coating").FirstOrDefault();//其實應該要24
                                                                            if (getExtCoating != null && DisplayValue != null)
                                                                            {//4:29/41
                                                                                ExtCoating = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getExtCoating != null && ProbabilityLabel != null)
                                                                            {
                                                                                ExtCoating = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getExtCoating != null && ConsequenceLabel != null)
                                                                            {
                                                                                ExtCoating = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getExtCoating != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                            {
                                                                                ExtCoating = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getInsulatedFlag = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulated?").FirstOrDefault();//其實應該要24
                                                                                if (getInsulatedFlag != null && DisplayValue != null)
                                                                                {
                                                                                    InsulatedFlag = DisplayValue.DisplayValue;
                                                                                }
                                                                                else if (getInsulatedFlag != null && ProbabilityLabel != null)
                                                                                {
                                                                                    InsulatedFlag = ProbabilityLabel.Label;
                                                                                }
                                                                                else if (getInsulatedFlag != null && ConsequenceLabel != null)
                                                                                {
                                                                                    InsulatedFlag = ConsequenceLabel.Label;
                                                                                }
                                                                                else if (getInsulatedFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                {
                                                                                    InsulatedFlag = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getInsulationCondition = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulation Condition").FirstOrDefault();//其實應該要24
                                                                                    if (getInsulationCondition != null && DisplayValue != null)
                                                                                    {
                                                                                        InsulationCondition = DisplayValue.DisplayValue;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && ProbabilityLabel != null)
                                                                                    {
                                                                                        InsulationCondition = ProbabilityLabel.Label;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && ConsequenceLabel != null)
                                                                                    {
                                                                                        InsulationCondition = ConsequenceLabel.Label;
                                                                                    }
                                                                                    else if (getInsulationCondition != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                    {
                                                                                        InsulationCondition = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getInsulationType = get31.Where(x => x.GroupName.ToString() == "Insulation Information"
                                                                        && x.Label.ToString() == "Insulation Type").FirstOrDefault();//其實應該要24
                                                                                        if (getInsulationType != null && DisplayValue != null)
                                                                                        {
                                                                                            InsulationType = DisplayValue.DisplayValue;
                                                                                        }
                                                                                        else if (getInsulationType != null && ProbabilityLabel != null)
                                                                                        {
                                                                                            InsulationType = ProbabilityLabel.Label;
                                                                                        }
                                                                                        else if (getInsulationType != null && ConsequenceLabel != null)
                                                                                        {
                                                                                            InsulationType = ConsequenceLabel.Label;
                                                                                        }
                                                                                        else if (getInsulationType != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                        {
                                                                                            InsulationType = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        { //!!!!超級奇怪
                                                                                            var getExtCalcCorrRate = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                                            && x.Label.ToString() == "Calculated Corrosion Rate").FirstOrDefault();//其實應該要24
                                                                                            if (getExtCalcCorrRate != null && DisplayValue25 != null)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(DisplayValue25.DisplayValue) * 25.4;

                                                                                            }
                                                                                            else if (getExtCalcCorrRate != null && ProbabilityLabel25 != null)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(ProbabilityLabel25.Label) * 25.4;

                                                                                            }
                                                                                            else if (getExtCalcCorrRate != null && ConsequenceLabel25 != null)
                                                                                            {
                                                                                                ExtCalcCorrRate = Convert.ToDouble(ConsequenceLabel25.Label) * 25.4;

                                                                                            }
                                                                                            else if (getExtCalcCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                            {

                                                                                                ExtCalcCorrRate = Convert.ToDouble(need24.ExtCalcCorrRate) * 25.4;
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
                                if (getAIT != null && DisplayValue != null)
                                {
                                    var AIT1 = Convert.ToDouble(DisplayValue.DisplayValue);
                                    AIT = ((AIT1 - 32) * 5) / 9;
                                }
                                else if (getAIT != null && ProbabilityLabel != null)
                                {
                                    var AIT1 = Convert.ToDouble(ProbabilityLabel.Label);
                                    AIT = ((AIT1 - 32) * 5) / 9;
                                }
                                else if (getAIT != null && ConsequenceLabel != null)
                                {
                                    var AIT1 = Convert.ToDouble(ConsequenceLabel.Label);
                                    AIT = ((AIT1 - 32) * 5) / 9;
                                }
                                else if (getAIT != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    var AIT1 = Convert.ToDouble(need24.Value);
                                    AIT = ((AIT1 - 32) * 5) / 9;
                                }
                                else
                                {
                                    var getBoilingPoint = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                        && x.Label.ToString() == "Boiling Point").FirstOrDefault();//其實應該要24
                                    if (getBoilingPoint != null && DisplayValue != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(DisplayValue.DisplayValue);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;


                                    }
                                    else if (getBoilingPoint != null && ProbabilityLabel != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(ProbabilityLabel.Label);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;


                                    }
                                    else if (getBoilingPoint != null && ConsequenceLabel != null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(ConsequenceLabel.Label);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;


                                    }
                                    else if (getBoilingPoint != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        var BoilingPoint1 = Convert.ToDouble(need24.Value);
                                        BoilingPoint = ((BoilingPoint1 - 32) * 5) / 9;


                                    }
                                    else
                                    {

                                        var getFluidType = get31.Where(x => x.GroupName.ToString() == "Representative Fluid Data"
                                                                        && x.Label.ToString() == "Fluid Type").FirstOrDefault();//其實應該要24
                                        if (getFluidType != null && DisplayValue != null)
                                        {
                                            FluidType = DisplayValue.DisplayValue;
                                        }
                                        else if (getFluidType != null && ProbabilityLabel != null)
                                        {
                                            FluidType = ProbabilityLabel.Label;
                                        }
                                        else if (getFluidType != null && ConsequenceLabel != null)
                                        {
                                            FluidType = ConsequenceLabel.Label;
                                        }
                                        else if (getFluidType != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            FluidType = need24.Value;
                                        }
                                        else
                                        {
                                            var getToxicBP = get31.Where(x => x.GroupName.ToString() == "Toxic Fluid Data"
                                                                        && x.Label.ToString() == "Boiling Point").FirstOrDefault();//其實應該要24
                                            if (getToxicBP != null && DisplayValue != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(DisplayValue.DisplayValue);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;


                                            }
                                            else if (getToxicBP != null && ProbabilityLabel != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(ProbabilityLabel.Label);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;


                                            }
                                            else if (getToxicBP != null && ConsequenceLabel != null)
                                            {
                                                var ToxicBP1 = Convert.ToDouble(ConsequenceLabel.Label);
                                                ToxicBP = ((ToxicBP1 - 32) * 5) / 9;


                                            }
                                            else if (getToxicBP != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
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
                                if (getIntCorrosionType != null && DisplayValue != null)
                                {
                                    IntCorrosionType = DisplayValue.DisplayValue;
                                }
                                else if (getIntCorrosionType != null && ProbabilityLabel != null)
                                {
                                    IntCorrosionType = ProbabilityLabel.Label;
                                }
                                else if (getIntCorrosionType != null && ConsequenceLabel != null)
                                {
                                    IntCorrosionType = ConsequenceLabel.Label;
                                }
                                else if (getIntCorrosionType != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    IntCorrosionType = need24.Value;
                                }
                                else
                                {
                                    var getIntCorrosionOption = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Corrosion Option").FirstOrDefault();//其實應該要24
                                    if (getIntCorrosionOption != null && DisplayValue != null)
                                    {
                                        IntCorrosionOption = DisplayValue.DisplayValue;
                                    }
                                    else if (getIntCorrosionOption != null && ProbabilityLabel != null)
                                    {
                                        IntCorrosionOption = ProbabilityLabel.Label;
                                    }
                                    else if (getIntCorrosionOption != null && ConsequenceLabel != null)
                                    {
                                        IntCorrosionOption = ConsequenceLabel.Label;
                                    }
                                    else if (getIntCorrosionOption != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        IntCorrosionOption = need24.Value;
                                    }
                                    else
                                    {

                                        var getIntExpectedCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Expected").FirstOrDefault();//其實應該要24
                                        if (getIntExpectedCorrRate != null && DisplayValue != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && ProbabilityLabel != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && ConsequenceLabel != null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                        }
                                        else if (getIntExpectedCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            IntExpectedCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                        }
                                        else
                                        {
                                            var getIntLTCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Long Term").FirstOrDefault();//其實應該要24
                                            if (getIntLTCorrRate != null && DisplayValue != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && ProbabilityLabel != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && ConsequenceLabel != null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                            }
                                            else if (getIntLTCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                IntLTCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                            }
                                            else
                                            {
                                                var getIntSTCorrRate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Corrosion Information"
                                                                        && x.Label.ToString() == "Short Term").FirstOrDefault();//其實應該要24
                                                if (getIntSTCorrRate != null && DisplayValue != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && ProbabilityLabel != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && ConsequenceLabel != null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                                }
                                                else if (getIntSTCorrRate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    IntSTCorrRate = Convert.ToDouble(need24.Value) * 25.4;

                                                }
                                                else
                                                {
                                                    var getCompType = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Component Type").FirstOrDefault();//其實應該要24
                                                    if (getCompType != null && DisplayValue != null)
                                                    {
                                                        CompType = DisplayValue.DisplayValue;
                                                    }
                                                    else if (getCompType != null && ProbabilityLabel != null)
                                                    {
                                                        CompType = ProbabilityLabel.Label;
                                                    }
                                                    else if (getCompType != null && ConsequenceLabel != null)
                                                    {
                                                        CompType = ConsequenceLabel.Label;
                                                    }
                                                    else if (getCompType != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                    {
                                                        CompType = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getDesignPressure = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Design Pressure").FirstOrDefault();//其實應該要24
                                                        if (getDesignPressure != null && DisplayValue != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(DisplayValue.DisplayValue) * 0.0689476;

                                                        }
                                                        else if (getDesignPressure != null && ProbabilityLabel != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(ProbabilityLabel.Label) * 0.0689476;

                                                        }
                                                        else if (getDesignPressure != null && ConsequenceLabel != null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(ConsequenceLabel.Label) * 0.0689476;

                                                        }
                                                        else if (getDesignPressure != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                        {
                                                            DesignPressure = Convert.ToDouble(need24.Value) * 0.0689476;

                                                        }
                                                        else
                                                        {
                                                            var getDesignTemp = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Design Temperature").FirstOrDefault();//其實應該要24
                                                            if (getDesignTemp != null && DisplayValue != null)
                                                            {
                                                                //(value - 32)x5 / 9
                                                                var DesignTemp1 = Convert.ToDouble(DisplayValue.DisplayValue);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;

                                                            }
                                                            else if (getDesignTemp != null && ProbabilityLabel != null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(ProbabilityLabel.Label);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;



                                                            }
                                                            else if (getDesignTemp != null && ConsequenceLabel != null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(ConsequenceLabel.Label);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;



                                                            }
                                                            else if (getDesignTemp != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                            {
                                                                var DesignTemp1 = Convert.ToDouble(need24.Value);
                                                                DesignTemp = ((DesignTemp1 - 32) * 5) / 9;



                                                            }
                                                            else
                                                            {
                                                                var getDiameter = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Diameter").FirstOrDefault();//其實應該要24
                                                                if (getDiameter != null && DisplayValue != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;

                                                                }
                                                                else if (getDiameter != null && ProbabilityLabel != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                                                }
                                                                else if (getDiameter != null && ConsequenceLabel != null)
                                                                {
                                                                    Diameter = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                                                }
                                                                else if (getDiameter != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                {
                                                                    Diameter = Convert.ToDouble(need24.Value) * 25.4;

                                                                }
                                                                else
                                                                {
                                                                    var getIntServDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                                        && x.Label.ToString() == "Internal Date in Service").FirstOrDefault();//其實應該要24
                                                                    if (getIntServDate != null && DisplayValue != null)
                                                                    {
                                                                        IntServDate = DisplayValue.DisplayValue;
                                                                    }
                                                                    else if (getIntServDate != null && ProbabilityLabel != null)
                                                                    {
                                                                        IntServDate = ProbabilityLabel.Label;
                                                                    }
                                                                    else if (getIntServDate != null && ConsequenceLabel != null)
                                                                    {
                                                                        IntServDate = ConsequenceLabel.Label;
                                                                    }
                                                                    else if (getIntServDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                    {
                                                                        IntServDate = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getAnalysisDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "General Information、NULL"
                                                                && x.Label.ToString() == "Analysis Date").FirstOrDefault();//其實應該要24
                                                                        if (getAnalysisDate != null && DisplayValue != null)
                                                                        {
                                                                            AnalysisDate = DisplayValue.DisplayValue;
                                                                        }
                                                                        else if (getAnalysisDate != null && ProbabilityLabel != null)
                                                                        {
                                                                            AnalysisDate = ProbabilityLabel.Label;
                                                                        }
                                                                        else if (getAnalysisDate != null && ConsequenceLabel != null)
                                                                        {
                                                                            AnalysisDate = ConsequenceLabel.Label;
                                                                        }
                                                                        else if (getAnalysisDate != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            AnalysisDate = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getIntInpsConf = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                               && x.Label.ToString() == "Confidence").FirstOrDefault();//其實應該要24
                                                                            if (getIntInpsConf != null && DisplayValue != null)
                                                                            {
                                                                                IntInpsConf = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getIntInpsConf != null && ProbabilityLabel != null)
                                                                            {
                                                                                IntInpsConf = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getIntInpsConf != null && ConsequenceLabel != null)
                                                                            {
                                                                                IntInpsConf = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getIntInpsConf != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                            {
                                                                                IntInpsConf = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getIntLastInspDate = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                               && x.Label.ToString() == "Date of Last Inspection").FirstOrDefault();//其實應該要24
                                                                                if (getIntLastInspDate != null && DisplayValue != null)
                                                                                {
                                                                                    IntLastInspDate = DisplayValue.DisplayValue;
                                                                                }
                                                                                else if (getIntLastInspDate != null && ProbabilityLabel != null)
                                                                                {
                                                                                    IntLastInspDate = ProbabilityLabel.Label;
                                                                                }
                                                                                else if (getIntLastInspDate != null && ConsequenceLabel != null)
                                                                                {
                                                                                    IntLastInspDate = ConsequenceLabel.Label;
                                                                                }
                                                                                else if (getIntLastInspDate != null && DisplayValue == null && ProbabilityLabel == null && need24.ConsequenceLabel == null)
                                                                                {
                                                                                    IntLastInspDate = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getIntNoOfInps = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Internal Inspection Information"
                                                               && x.Label.ToString() == "No. of Inspection").FirstOrDefault();//其實應該要24
                                                                                    if (getIntNoOfInps != null && DisplayValue != null)
                                                                                    {
                                                                                        IntNoOfInps = DisplayValue.DisplayValue;
                                                                                    }
                                                                                    else if (getIntNoOfInps != null && ProbabilityLabel != null)
                                                                                    {
                                                                                        IntNoOfInps = ProbabilityLabel.Label;
                                                                                    }
                                                                                    else if (getIntNoOfInps != null && ConsequenceLabel != null)
                                                                                    {
                                                                                        IntNoOfInps = ConsequenceLabel.Label;
                                                                                    }
                                                                                    else if (getIntNoOfInps != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                    {
                                                                                        IntNoOfInps = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getConstCode = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                               && x.Label.ToString() == "Construction Code").FirstOrDefault();//其實應該要24
                                                                                        if (getConstCode != null && DisplayValue != null)
                                                                                        {
                                                                                            ConstCode = DisplayValue.DisplayValue;
                                                                                        }
                                                                                        else if (getConstCode != null && ProbabilityLabel != null)
                                                                                        {
                                                                                            ConstCode = ProbabilityLabel.Label;
                                                                                        }
                                                                                        else if (getConstCode != null && ConsequenceLabel != null)
                                                                                        {
                                                                                            ConstCode = ConsequenceLabel.Label;
                                                                                        }
                                                                                        else if (getConstCode != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                        {
                                                                                            ConstCode = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getJointEfficiency = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                               && x.Label.ToString() == "Joint Efficiency").FirstOrDefault();//其實應該要24
                                                                                            if (getJointEfficiency != null && DisplayValue != null)
                                                                                            {
                                                                                                JointEfficiency = DisplayValue.DisplayValue;
                                                                                            }
                                                                                            else if (getJointEfficiency != null && ProbabilityLabel != null)
                                                                                            {
                                                                                                JointEfficiency = ProbabilityLabel.Label;
                                                                                            }
                                                                                            else if (getJointEfficiency != null && ConsequenceLabel != null)
                                                                                            {
                                                                                                JointEfficiency = ConsequenceLabel.Label;
                                                                                            }
                                                                                            else if (getJointEfficiency != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                            {
                                                                                                JointEfficiency = need24.Value;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                var getOverideAllowableStress = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                               && x.Label.ToString() == "Override Allowable Stress").FirstOrDefault();//其實應該要24
                                                                                                if (getOverideAllowableStress != null && DisplayValue != null)
                                                                                                {
                                                                                                    OverideAllowableStress = Convert.ToDouble(DisplayValue.DisplayValue) * 0.0689476;

                                                                                                }
                                                                                                else if (getOverideAllowableStress != null && ProbabilityLabel != null)
                                                                                                {
                                                                                                    OverideAllowableStress = Convert.ToDouble(ProbabilityLabel.Label) * 0.0689476;

                                                                                                }
                                                                                                else if (getOverideAllowableStress != null && ConsequenceLabel != null)
                                                                                                {
                                                                                                    OverideAllowableStress = Convert.ToDouble(ConsequenceLabel.Label) * 0.0689476;

                                                                                                }
                                                                                                else if (getOverideAllowableStress != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                {
                                                                                                    OverideAllowableStress = Convert.ToDouble(need24.Value) * 0.0689476;

                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    var getOverideAllowableStressFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Stress Material Information"
                                                               && x.Label.ToString() == "Override Allowable Stress?").FirstOrDefault();//其實應該要24
                                                                                                    if (getOverideAllowableStressFlag != null && DisplayValue != null)
                                                                                                    {
                                                                                                        OverideAllowableStressFlag = DisplayValue.DisplayValue;
                                                                                                    }
                                                                                                    else if (getOverideAllowableStressFlag != null && ProbabilityLabel != null)
                                                                                                    {
                                                                                                        OverideAllowableStressFlag = ProbabilityLabel.Label;
                                                                                                    }
                                                                                                    else if (getOverideAllowableStressFlag != null && ConsequenceLabel != null)
                                                                                                    {
                                                                                                        OverideAllowableStressFlag = ConsequenceLabel.Label;
                                                                                                    }
                                                                                                    else if (getOverideAllowableStressFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                    {
                                                                                                        OverideAllowableStressFlag = need24.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        var getEstMinThicknessFlag = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                               && x.Label.ToString() == "Override Est. Min. Thickness?").FirstOrDefault();//其實應該要24
                                                                                                        if (getEstMinThicknessFlag != null && DisplayValue != null)
                                                                                                        {
                                                                                                            EstMinThicknessFlag = DisplayValue.DisplayValue;
                                                                                                        }
                                                                                                        else if (getEstMinThicknessFlag != null && ProbabilityLabel != null)
                                                                                                        {
                                                                                                            EstMinThicknessFlag = ProbabilityLabel.Label;
                                                                                                        }
                                                                                                        else if (getEstMinThicknessFlag != null && ConsequenceLabel != null)
                                                                                                        {
                                                                                                            EstMinThicknessFlag = ConsequenceLabel.Label;
                                                                                                        }

                                                                                                        else if (getEstMinThicknessFlag != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                        {
                                                                                                            EstMinThicknessFlag = need24.Value;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            var getEstMinThickness = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                          && x.Label.ToString() == "Override Est. Min. Thickness").FirstOrDefault();
                                                                                                            if (getEstMinThickness != null && DisplayValue25 != null)
                                                                                                            {
                                                                                                                EstMinThickness = Convert.ToDouble(DisplayValue25.DisplayValue) * 25.4;
                                                                                                            }
                                                                                                            else if (getEstMinThickness != null && ProbabilityLabel25 != null)
                                                                                                            {
                                                                                                                EstMinThickness = Convert.ToDouble(ProbabilityLabel25.Label) * 25.4;
                                                                                                            }
                                                                                                            else if (getEstMinThickness != null && ConsequenceLabel25 != null)
                                                                                                            {
                                                                                                                EstMinThickness = Convert.ToDouble(ConsequenceLabel25.Label) * 25.4;
                                                                                                            }

                                                                                                            else if (getEstMinThickness != null && DisplayValue25 == null && ProbabilityLabel25 == null && ConsequenceLabel25 == null)
                                                                                                            {
                                                                                                                EstMinThickness = Convert.ToDouble(need24.Value25) * 25.4;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                var getIntEstWallRemain = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                            && x.Label.ToString() == "Estimated Wall Remaining").FirstOrDefault();
                                                                                                                if (getIntEstWallRemain != null && DisplayValue25 != null)
                                                                                                                {
                                                                                                                    IntEstWallRemain = Convert.ToDouble(DisplayValue25.DisplayValue) * 25.4;
                                                                                                                }
                                                                                                                else if (getIntEstWallRemain != null && ProbabilityLabel25 != null)
                                                                                                                {
                                                                                                                    IntEstWallRemain = Convert.ToDouble(ProbabilityLabel25.Label) * 25.4;
                                                                                                                }
                                                                                                                else if (getIntEstWallRemain != null && ConsequenceLabel25 != null)
                                                                                                                {
                                                                                                                    IntEstWallRemain = Convert.ToDouble(ConsequenceLabel25.Label) * 25.4;
                                                                                                                }

                                                                                                                else if (getIntEstWallRemain != null && DisplayValue25 == null && ProbabilityLabel25 == null && ConsequenceLabel25 == null)
                                                                                                                {
                                                                                                                    IntEstWallRemain = Convert.ToDouble(need24.Value25) * 25.4;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    var getEstHalfLife = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Calculations"
                                                           && x.Label.ToString() == "Estimated Half Life").FirstOrDefault();
                                                                                                                    if (getEstHalfLife != null && DisplayValue25 != null)
                                                                                                                    {
                                                                                                                        EstHalfLife = DisplayValue25.DisplayValue;
                                                                                                                    }
                                                                                                                    else if (getEstHalfLife != null && ProbabilityLabel25 != null)
                                                                                                                    {
                                                                                                                        EstHalfLife = ProbabilityLabel25.Label;
                                                                                                                    }
                                                                                                                    else if (getEstHalfLife != null && ConsequenceLabel25 != null)
                                                                                                                    {
                                                                                                                        EstHalfLife = ConsequenceLabel25.Label;
                                                                                                                    }

                                                                                                                    else if (getEstHalfLife != null && DisplayValue25 == null && ProbabilityLabel25 == null && ConsequenceLabel25 == null)
                                                                                                                    {
                                                                                                                        EstHalfLife = need24.Value25;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {

                                                                                                                        var getRepThick = get31.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Design Information"
                                                         && (x.Label.ToString() == "Initial Floor Thickness" || x.Label.ToString() == "Initial Wall Thickness")).FirstOrDefault();
                                                                                                                        if (getRepThick != null && DisplayValue != null)
                                                                                                                        {

                                                                                                                            RepThick = Convert.ToDouble(DisplayValue.DisplayValue) * 25.4;
                                                                                                                        }
                                                                                                                        else if (getRepThick != null && ProbabilityLabel != null)
                                                                                                                        {
                                                                                                                            RepThick = Convert.ToDouble(ProbabilityLabel.Label) * 25.4;

                                                                                                                        }
                                                                                                                        else if (getRepThick != null && ConsequenceLabel != null)
                                                                                                                        {
                                                                                                                            RepThick = Convert.ToDouble(ConsequenceLabel.Label) * 25.4;

                                                                                                                        }

                                                                                                                        else if (getRepThick != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                                        {
                                                                                                                            RepThick = Convert.ToDouble(need24.Value) * 25.4;

                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            var getEstMinThickness2 = get30.Where(x => !x.GroupName.IsEmpty() && x.GroupName.ToString() == "Wall Loss Analysis"
                                                          && x.Label.ToString() == "Est. Min. Thickness").FirstOrDefault();
                                                                                                                            if (getEstMinThickness2 != null && DisplayValue25 != null)
                                                                                                                            {
                                                                                                                                EstMinThickness2 = Convert.ToDouble(DisplayValue25.DisplayValue) * 25.4;
                                                                                                                            }
                                                                                                                            else if (getEstMinThickness2 != null && ProbabilityLabel25 != null)
                                                                                                                            {
                                                                                                                                EstMinThickness2 = Convert.ToDouble(ProbabilityLabel25.Label) * 25.4;
                                                                                                                            }
                                                                                                                            else if (getEstMinThickness2 != null && ConsequenceLabel25 != null)
                                                                                                                            {
                                                                                                                                EstMinThickness2 = Convert.ToDouble(ConsequenceLabel25.Label) * 25.4;
                                                                                                                            }

                                                                                                                            else if (getEstMinThickness2 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                                            {
                                                                                                                                EstMinThickness2 = 0;
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
                                if (getODM1 != null && DisplayValue != null)
                                {
                                    ODM1 = DisplayValue.DisplayValue;
                                }
                                else if (getODM1 != null && ProbabilityLabel != null)
                                {
                                    ODM1 = ProbabilityLabel.Label;
                                }
                                else if (getODM1 != null && ConsequenceLabel != null)
                                {
                                    ODM1 = ConsequenceLabel.Label;
                                }
                                else if (getODM1 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                {
                                    ODM1 = need24.Value;
                                }
                                else
                                {
                                    var getODM1Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                    if (getODM1Potential != null && DisplayValue != null)
                                    {
                                        ODM1Potential = DisplayValue.DisplayValue;
                                    }
                                    else if (getODM1Potential != null && ProbabilityLabel != null)
                                    {
                                        ODM1Potential = ProbabilityLabel.Label;
                                    }
                                    else if (getODM1Potential != null && ConsequenceLabel != null)
                                    {
                                        ODM1Potential = ConsequenceLabel.Label;
                                    }
                                    else if (getODM1Potential != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                    {
                                        ODM1Potential = need24.Value;
                                    }
                                    else
                                    {

                                        var getODM1Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                        if (getODM1Probability != null && DisplayValue != null)
                                        {
                                            ODM1Probability = DisplayValue.DisplayValue;
                                        }
                                        else if (getODM1Probability != null && ProbabilityLabel != null)
                                        {
                                            ODM1Probability = ProbabilityLabel.Label;
                                        }
                                        else if (getODM1Probability != null && ConsequenceLabel != null)
                                        {
                                            ODM1Probability = ConsequenceLabel.Label;
                                        }
                                        else if (getODM1Probability != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                        {
                                            ODM1Probability = need24.Value;
                                        }
                                        else
                                        {
                                            var getODM1Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 1"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                            if (getODM1Comment != null && DisplayValue != null)
                                            {
                                                ODM1Comment = DisplayValue.DisplayValue;
                                            }
                                            else if (getODM1Comment != null && ProbabilityLabel != null)
                                            {
                                                ODM1Comment = ProbabilityLabel.Label;
                                            }
                                            else if (getODM1Comment != null && ConsequenceLabel != null)
                                            {
                                                ODM1Comment = ConsequenceLabel.Label;
                                            }
                                            else if (getODM1Comment != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                            {
                                                ODM1Comment = need24.Value;
                                            }
                                            else
                                            {
                                                var getODM2 = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                if (getODM2 != null && DisplayValue != null)
                                                {
                                                    ODM2 = DisplayValue.DisplayValue;
                                                }
                                                else if (getODM2 != null && ProbabilityLabel != null)
                                                {
                                                    ODM2 = ProbabilityLabel.Label;
                                                }
                                                else if (getODM2 != null && ConsequenceLabel != null)
                                                {
                                                    ODM2 = ConsequenceLabel.Label;
                                                }
                                                else if (getODM2 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                {
                                                    ODM2 = need24.Value;
                                                }
                                                else
                                                {
                                                    var getODM2Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                    if (getODM2Potential != null && DisplayValue != null)
                                                    {
                                                        ODM2Potential = DisplayValue.DisplayValue;
                                                    }
                                                    else if (getODM2Potential != null && ProbabilityLabel != null)
                                                    {
                                                        ODM2Potential = ProbabilityLabel.Label;
                                                    }
                                                    else if (getODM2Potential != null && ConsequenceLabel != null)
                                                    {
                                                        ODM2Potential = ConsequenceLabel.Label;
                                                    }
                                                    else if (getODM2Potential != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                    {
                                                        ODM2Potential = need24.Value;
                                                    }
                                                    else
                                                    {
                                                        var getODM2Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                        if (getODM2Probability != null && DisplayValue != null)
                                                        {
                                                            ODM2Probability = DisplayValue.DisplayValue;
                                                        }
                                                        else if (getODM2Probability != null && ProbabilityLabel != null)
                                                        {
                                                            ODM2Probability = ProbabilityLabel.Label;
                                                        }
                                                        else if (getODM2Probability != null && ConsequenceLabel != null)
                                                        {
                                                            ODM2Probability = ConsequenceLabel.Label;
                                                        }
                                                        else if (getODM2Probability != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                        {
                                                            ODM2Probability = need24.Value;
                                                        }
                                                        else
                                                        {
                                                            var getODM2Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 2"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                            if (getODM2Comment != null && DisplayValue != null)
                                                            {
                                                                ODM2Comment = DisplayValue.DisplayValue;
                                                            }
                                                            else if (getODM2Comment != null && ProbabilityLabel != null)
                                                            {
                                                                ODM2Comment = ProbabilityLabel.Label;
                                                            }
                                                            else if (getODM2Comment != null && ConsequenceLabel != null)
                                                            {
                                                                ODM2Comment = ConsequenceLabel.Label;
                                                            }
                                                            else if (getODM2Comment != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                            {
                                                                ODM2Comment = need24.Value;
                                                            }
                                                            else
                                                            {
                                                                var getODM3 = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                if (getODM3 != null && DisplayValue != null)
                                                                {
                                                                    ODM3 = DisplayValue.DisplayValue;
                                                                }
                                                                else if (getODM3 != null && ProbabilityLabel != null)
                                                                {
                                                                    ODM3 = ProbabilityLabel.Label;
                                                                }
                                                                else if (getODM3 != null && ConsequenceLabel != null)
                                                                {
                                                                    ODM3 = ConsequenceLabel.Label;
                                                                }
                                                                else if (getODM3 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                {
                                                                    ODM3 = need24.Value;
                                                                }
                                                                else
                                                                {
                                                                    var getODM3Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                    if (getODM3Potential != null && DisplayValue != null)
                                                                    {
                                                                        ODM3Potential = DisplayValue.DisplayValue;
                                                                    }
                                                                    else if (getODM3Potential != null && ProbabilityLabel != null)
                                                                    {
                                                                        ODM3Potential = ProbabilityLabel.Label;
                                                                    }
                                                                    else if (getODM3Potential != null && ConsequenceLabel != null)
                                                                    {
                                                                        ODM3Potential = ConsequenceLabel.Label;
                                                                    }
                                                                    else if (getODM3Potential != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                    {
                                                                        ODM3Potential = need24.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var getODM3Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                        if (getODM3Probability != null && DisplayValue != null)
                                                                        {
                                                                            ODM3Probability = DisplayValue.DisplayValue;
                                                                        }
                                                                        else if (getODM3Probability != null && ProbabilityLabel != null)
                                                                        {
                                                                            ODM3Probability = ProbabilityLabel.Label;
                                                                        }
                                                                        else if (getODM3Probability != null && ConsequenceLabel != null)
                                                                        {
                                                                            ODM3Probability = ConsequenceLabel.Label;
                                                                        }
                                                                        else if (getODM3Probability != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                        {
                                                                            ODM3Probability = need24.Value;
                                                                        }
                                                                        else
                                                                        {
                                                                            var getODM3Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 3"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                            if (getODM3Comment != null && DisplayValue != null)
                                                                            {
                                                                                ODM3Comment = DisplayValue.DisplayValue;
                                                                            }
                                                                            else if (getODM3Comment != null && ProbabilityLabel != null)
                                                                            {
                                                                                ODM3Comment = ProbabilityLabel.Label;
                                                                            }
                                                                            else if (getODM3Comment != null && ConsequenceLabel != null)
                                                                            {
                                                                                ODM3Comment = ConsequenceLabel.Label;
                                                                            }
                                                                            else if (getODM3Comment != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                            {
                                                                                ODM3Comment = need24.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                var getODM4 = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                                if (getODM4 != null && DisplayValue != null)
                                                                                {
                                                                                    ODM4 = DisplayValue.DisplayValue;
                                                                                }
                                                                                else if (getODM4 != null && ProbabilityLabel != null)
                                                                                {
                                                                                    ODM4 = ProbabilityLabel.Label;
                                                                                }
                                                                                else if (getODM4 != null && ConsequenceLabel != null)
                                                                                {
                                                                                    ODM4 = ConsequenceLabel.Label;
                                                                                }
                                                                                else if (getODM4 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                {
                                                                                    ODM4 = need24.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    var getODM4Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                                    if (getODM4Potential != null && DisplayValue != null)
                                                                                    {
                                                                                        ODM4Potential = DisplayValue.DisplayValue;
                                                                                    }
                                                                                    else if (getODM4Potential != null && ProbabilityLabel != null)
                                                                                    {
                                                                                        ODM4Potential = ProbabilityLabel.Label;
                                                                                    }
                                                                                    else if (getODM4Potential != null && ConsequenceLabel != null)
                                                                                    {
                                                                                        ODM4Potential = ConsequenceLabel.Label;
                                                                                    }
                                                                                    else if (getODM4Potential != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                    {
                                                                                        ODM4Potential = need24.Value;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        var getODM4Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                                        if (getODM4Probability != null && DisplayValue != null)
                                                                                        {
                                                                                            ODM4Probability = DisplayValue.DisplayValue;
                                                                                        }
                                                                                        else if (getODM4Probability != null && ProbabilityLabel != null)
                                                                                        {
                                                                                            ODM4Probability = ProbabilityLabel.Label;
                                                                                        }
                                                                                        else if (getODM4Probability != null && ConsequenceLabel != null)
                                                                                        {
                                                                                            ODM4Probability = ConsequenceLabel.Label;
                                                                                        }
                                                                                        else if (getODM4Probability != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                        {
                                                                                            ODM4Probability = need24.Value;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            var getODM4Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 4"
                                                                       && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                                            if (getODM4Comment != null && DisplayValue != null)
                                                                                            {
                                                                                                ODM4Comment = DisplayValue.DisplayValue;
                                                                                            }
                                                                                            else if (getODM4Comment != null && ProbabilityLabel != null)
                                                                                            {
                                                                                                ODM4Comment = ProbabilityLabel.Label;
                                                                                            }
                                                                                            else if (getODM4Comment != null && ConsequenceLabel != null)
                                                                                            {
                                                                                                ODM4Comment = ConsequenceLabel.Label;
                                                                                            }
                                                                                            else if (getODM4Comment != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                            {
                                                                                                ODM4Comment = need24.Value;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                var getODM5 = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Mechanism").FirstOrDefault();//其實應該要24
                                                                                                if (getODM5 != null && DisplayValue != null)
                                                                                                {
                                                                                                    ODM5 = DisplayValue.DisplayValue;
                                                                                                }
                                                                                                else if (getODM5 != null && ProbabilityLabel != null)
                                                                                                {
                                                                                                    ODM5 = ProbabilityLabel.Label;
                                                                                                }
                                                                                                else if (getODM5 != null && ConsequenceLabel != null)
                                                                                                {
                                                                                                    ODM5 = ConsequenceLabel.Label;
                                                                                                }
                                                                                                else if (getODM5 != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                {
                                                                                                    ODM5 = need24.Value;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    var getODM5Potential = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Potential").FirstOrDefault();//其實應該要24
                                                                                                    if (getODM5Potential != null && DisplayValue != null)
                                                                                                    {
                                                                                                        ODM5Potential = DisplayValue.DisplayValue;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && ProbabilityLabel != null)
                                                                                                    {
                                                                                                        ODM5Potential = ProbabilityLabel.Label;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && ConsequenceLabel != null)
                                                                                                    {
                                                                                                        ODM5Potential = ConsequenceLabel.Label;
                                                                                                    }
                                                                                                    else if (getODM5Potential != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                    {
                                                                                                        ODM5Potential = need24.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        var getODM5Probability = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                       && x.Label.ToString() == "Probability").FirstOrDefault();//其實應該要24
                                                                                                        if (getODM5Probability != null && DisplayValue != null)
                                                                                                        {
                                                                                                            ODM5Probability = DisplayValue.DisplayValue;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && ProbabilityLabel != null)
                                                                                                        {
                                                                                                            ODM5Probability = ProbabilityLabel.Label;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && ConsequenceLabel != null)
                                                                                                        {
                                                                                                            ODM5Probability = ConsequenceLabel.Label;
                                                                                                        }
                                                                                                        else if (getODM5Probability != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
                                                                                                        {
                                                                                                            ODM5Probability = need24.Value;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            var getODM5Comment = get31.Where(x => x.GroupName.ToString() == "Mechanism 5"
                                                                      && x.Label.ToString() == "Comment").FirstOrDefault();//其實應該要24
                                                                                                            if (getODM5Comment != null && DisplayValue != null)
                                                                                                            {
                                                                                                                ODM5Comment = DisplayValue.DisplayValue;
                                                                                                            }
                                                                                                            else if (getODM5Comment != null && ProbabilityLabel != null)
                                                                                                            {
                                                                                                                ODM5Comment = ProbabilityLabel.Label;
                                                                                                            }
                                                                                                            else if (getODM5Comment != null && ConsequenceLabel != null)
                                                                                                            {
                                                                                                                ODM5Comment = ConsequenceLabel.Label;
                                                                                                            }

                                                                                                            else  if (getODM5Comment != null && DisplayValue == null && ProbabilityLabel == null && ConsequenceLabel == null)
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
                        need24.OperatingTemp = OperatingTemp;
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

                    return Json(DataList_T0NKME07model);
                    var fileName = "T0NKME01" + Guid.NewGuid().ToString() + ".xlsx";
                   // var guidFileName = DataList_T0NKME07model.ExportExcel<T0NKME06model>(fileName, "sheet");


                    


                    
                }

            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }





        }
    }
}