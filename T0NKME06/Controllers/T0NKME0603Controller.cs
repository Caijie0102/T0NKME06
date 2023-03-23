using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using T0NKME06.Models;

namespace T0NKME06.Controllers
{
    public class T0NKME0603Controller : ApiController
    {

        // GET: T0NKME0602
        public IHttpActionResult Get()
        {
            List<T0NKME06model> DataList_T0NKME02model = new List<T0NKME06model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();


            try
            {
                using (Entities2 db = new Entities2())
                {
                    //var W0NKME02 = db.ComponentDetails.ToList();
                    //var W0NKME04 = db.HierarchyTemplates.ToList();
                    //var W0NKME03 = db.OrgTreeNodes.ToList();

                    var getAllComponent = from otmri in db.OrgTreeNodeModelRunInputs
                                          join otmr in db.OrgTreeNodeModelRuns on otmri.OrgTreeNodeModelRunId equals otmr.OrgTreeNodeModelRunId
                                          join nim in db.NodeInputMetadata on otmri.NodeInputId equals nim.NodeInputId
                                          join mnm in db.ModelNodeMetadata on otmri.ModelNodeId equals mnm.ModelNodeId
                                          //join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId
                                          //join cd in db.ComponentDetails on otn.ComponentDetailsId equals cd.ComponentDetailsId

                                          select new
                                          {
                                              NodeLabel = mnm.NodeLabel.ToString(),
                                              GroupName = nim.GroupName.ToString(),
                                              Label = nim.Label.ToString(),
                                              Value = otmri.Value.ToString(),

                                              DisplayValue = (from rldv in db.RunnableLookupRowDisplayValues
                                                              where otmri.Value.ToString() == rldv.LookupRowId.ToString()
                                                              select rldv.DisplayValue).FirstOrDefault(),
                                              ProbabilityLabel = (from rmpv in db.RiskMatrixProbabilityVectors
                                                                  where otmri.Value.ToString() == rmpv.Index.ToString()
                                                                  select rmpv.Label).FirstOrDefault(),
                                              ConsequenceLabel = (from rmcv in db.RiskMatrixConsequenceVectors
                                                                  where otmri.Value.ToString() == rmcv.Index.ToString()
                                                                  select rmcv.Label).FirstOrDefault(),

                                              //ComponentDetailsId = cd.ComponentDetailsId.ToString(),

                                              //OrgTreeNodeModelRunId= otmr.OrgTreeNodeModelRunInputs.ToString(),說是不能轉換成字串

                                              // ModelId= otmr.ModelId.ToString(),

                                              OrgTreeNodeId1 = otmr.OrgTreeNodeId.ToString(),
                                              //ParentId= otn.ParentId.ToString()


                                          };

                    //return Json(getAllComponent.Take(100).ToList());
                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var fake = getAllComponent
                  //.AsEnumerable()
                  .Take(10);
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
                                                var JacketedFlag = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Other Data"
                                                                 && x.Label.ToString().Contains("Jacket")).Select(x => new { x.Value }).FirstOrDefault();
                                                var IntEntryPossFlag = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && (x.GroupName.ToString() == "Column Locations"
                                                                     || x.GroupName.ToString() == "Exchanger Locations" || x.GroupName.ToString() == "Pipe Locations"
                                                                     || x.GroupName.ToString() == "Tank Locations" || x.GroupName.ToString() == "Vessel Locations")
                                                                     && x.Label.ToString() == "Internal Entry Possible").Select(x => new { x.Value }).FirstOrDefault();
                                                var InjectionPointFlag = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Other Data"
                                                                 && x.Label.ToString() == "Injection Point?").Select(x => new { x.Value }).FirstOrDefault();

                                                var MixedBoreFlag = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Other Data"
                                                                                         && x.Label.ToString() == "Mixed Bore?").Select(x => new { x.Value }).FirstOrDefault();

                                                var SmallBoreFlag = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Other Data"
                                                                 && x.Label.ToString() == "Small Bore?").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoBarrPenetrations = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Barrier Penetrations").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoDmgdInsd = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Damaged Insulation").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoDeadLegs = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Dead Legs").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoElbows = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Elbows").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoErosionZones = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Erosion Zones").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoHorizLowPts = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Horizontal Low Points").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoInsdTerminators = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Insulation Terminations").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoLongHorizRuns = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Long Horizontal Runs").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoReducers = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Reducers").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoSoilToAirIntfs = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Soil-Air Interface").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoTees = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Tees").Select(x => new { x.Value }).FirstOrDefault();

                                                var NoVertRuns = getAllComponent.Where(x => x.NodeLabel.ToString() == "ADDITIONAL INFORMATION" && x.GroupName.ToString() == "Pipe Locations"
                                                                                         && x.Label.ToString() == "Number of Vertical Runs").Select(x => new { x.Value }).FirstOrDefault();







                                                lock (lockMe)
                                                {
                                                    DataList_T0NKME02model.Add(new T0NKME06model()
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
                                                        JacketedFlag = JacketedFlag?.ToString() ?? string.Empty,
                                                        IntEntryPossFlag = IntEntryPossFlag?.ToString() ?? string.Empty,
                                                        InjectionPointFlag = InjectionPointFlag?.ToString() ?? string.Empty,
                                                        MixedBoreFlag = MixedBoreFlag?.ToString() ?? string.Empty,
                                                        SmallBoreFlag = SmallBoreFlag?.ToString() ?? string.Empty,
                                                        NoBarrPenetrations = NoBarrPenetrations?.ToString() ?? string.Empty,
                                                        NoDmgdInsd = NoDmgdInsd?.ToString() ?? string.Empty,
                                                        NoDeadLegs = NoDeadLegs?.ToString() ?? string.Empty,
                                                        NoElbows = NoElbows?.ToString() ?? string.Empty,
                                                        NoErosionZones = NoErosionZones?.ToString() ?? string.Empty,
                                                        NoHorizLowPts = NoHorizLowPts?.ToString() ?? string.Empty,
                                                        NoInsdTerminators = NoInsdTerminators?.ToString() ?? string.Empty,
                                                        NoLongHorizRuns = NoLongHorizRuns?.ToString() ?? string.Empty,
                                                        NoReducers = NoReducers?.ToString() ?? string.Empty,
                                                        NoSoilToAirIntfs = NoSoilToAirIntfs?.ToString() ?? string.Empty,
                                                        NoTees = NoTees?.ToString() ?? string.Empty,
                                                        NoVertRuns = NoVertRuns?.ToString() ?? string.Empty  // // 使用 Null-conditional Operator 判斷 NoVertRuns 是否為 null，若是則不執行 ToString()，直接回傳 null



                                                        //NoTeesOutput = NoTees?.ToString() ?? string.Empty;

                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });


                    return Ok(DataList_T0NKME02model);


                    //return Json(DataList_T0NKME02model.Take(100).ToList());



                    //var fileName = "T0NKME01" + Guid.NewGuid().ToString() + ".xlsx";
                    //var guidFileName = DataList_T0NKME02model.ExportExcel<T0NKME02model>(fileName, "sheet");

                    // return Ok(DataList_T0NKME02model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }




        }
    }
    
}