using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using T0NKME06.Models;
using System.Web.WebPages;

namespace T0NKME06.Controllers
{
    public class T0NKME0803Controller : ApiController
    {
        // GET: T0NKME0803
        public IHttpActionResult Get()
        {
            List<T0NKME08model> DataList_T0NKME08model = new List<T0NKME08model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();

            try
            {
                using (Entities2 db = new Entities2())
                {




                    var getAllComponent = from otmr in db.OrgTreeNodeModelRuns  //32
                                                                                //join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId //03
                                          join ip in db.InspectionPlans on otmr.OrgTreeNodeModelRunId equals ip.OrgTreeNodeModelRunId  //09

                                          select new
                                          {

                                              OrgTreeNodeId = otmr.OrgTreeNodeId.ToString(), //otmr與otn連接的

                                              //AssetDetailsId = otn.AssetDetailsId.ToString(),


                                              OrgTreeNodeModelRunId = otmr.OrgTreeNodeModelRunId.ToString(),
                                              CreatedBy32 = otmr.CreatedBy.ToString(),
                                              CreationDate32 = otmr.CreationDate.ToString(),
                                              LastModifiedBy32 = otmr.LastModifiedBy.ToString(),
                                              LastModifiedDate32 = otmr.LastModifiedDate.ToString(),
                                              Name = otmr.Name.ToString(),
                                              ScenarioType = otmr.ScenarioType.ToString(),
                                              AnalysisDate = otmr.AnalysisDate.ToString(),
                                              MassCriticalityRunId = otmr.MassCriticalityRunId.ToString(),

                                              //09
                                              InspectionPlanId = ip.InspectionPlanId.ToString(),
                                              //09
                                              CreatedBy09 = ip.CreatedBy.ToString(),
                                              //09
                                              CreationDate09 = ip.CreationDate.ToString(),
                                              //09
                                              InspectionPlanStatus = ip.InspectionPlanStatus.ToString(),





                                          };
                    //  return Ok(getAllComponent.Take(100).ToList());




                    var W0NKME03 = db.OrgTreeNodes.ToList();

                    var fake = getAllComponent
                  //.AsEnumerable() //19
                  .Take(1000);
                    //.ToList(); // 将 LINQ 对象转换为列表对象

                    Parallel.ForEach(fake, item =>
                    {
                        //在這個迴圈中，程式碼使用Where方法從"W0NKME03"集合中過濾出元件（Components），以便獲取該元件的詳細資料，例如組織、位置、系統、設備、元件ID等。使用Select方法從過濾後的元件集合中，選擇需要的屬性來建立一個匿名物件，然後使用FirstOrDefault方法來取得第一個符合條件的元素，如果沒有符合條件的元素，則返回null。
                        var getComponent = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == item.OrgTreeNodeId.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();
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

                                                    DataList_T0NKME08model.Add(new T0NKME08model()
                                                    {
                                                        //128
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

                                                        OrgTreeNodeModelRunId = item.OrgTreeNodeModelRunId.ToString(),

                                                        CreatedBy32 = item.CreatedBy32.ToString(),
                                                        CreationDate32 = item.CreationDate32.ToString(),
                                                        LastModifiedBy32 = item.LastModifiedBy32.ToString(),
                                                        LastModifiedDate32 = item.LastModifiedDate32.ToString(),
                                                        Name = item.Name.ToString(),
                                                        ScenarioType = item.ScenarioType.ToString(),
                                                        AnalysisDate = item.AnalysisDate.ToString(),
                                                        MassCriticalityRunId = item.MassCriticalityRunId.ToString(),

                                                        InspectionPlanId = item.InspectionPlanId.ToString(), //09?
                                                        CreatedBy09 = item.CreatedBy09.ToString(),
                                                        CreationDate09 = item.CreationDate09.ToString(),
                                                        InspectionPlanStatus = item.InspectionPlanStatus.ToString(),

                                                        //InspectionPlanTaskId = item.InspectionPlanTaskId.ToString(),
                                                        //MitigationColumnId = item.MitigationColumnId.ToString(),



                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });

                    //return Ok(DataList_T0NKME08model);

                    var W0NKME38 = db.InspectionPlanTasks.ToList();
                    var W0NKME39 = db.InspectionPlanTaskMitigationColumns.ToList();
                    var W0NKME40 = db.MitigationColumns.ToList();
                    var W0NKME08 = db.Recommendations.ToList();

                    foreach (var need32 in DataList_T0NKME08model)
                    {
                        var n38 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new {
                            x.InspectionPlanTaskId,
                            x.InspectionPlanId,
                            x.CreatedBy,
                            x.CreationDate,
                            x.LastModifiedBy,
                            x.LastModifiedDate,
                            x.TaskExtent,
                            x.TaskFrequency,
                            x.TaskInterval,
                            x.TaskName,
                            x.TaskReference,
                            x.MitigationType,
                            x.DueDate,
                            x.IsStaticDueDate,
                            x.HistoricInspectionDate,
                            x.MasterInspectionMethodId,
                            x.ManualLastInspectionDate
                        }).ToList();

                        var CreatedBy381 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.CreatedBy }).FirstOrDefault();
                        var CreationDate381 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.CreationDate }).FirstOrDefault();
                        var LastModifiedBy381 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.LastModifiedBy }).FirstOrDefault();


                        var CreatedBy38 = n38.Select(x => x.CreatedBy).FirstOrDefault();
                        need32.CreatedBy38 = CreatedBy38 ?? null;

                        var CreationDate38 = n38.Select(x => x.CreationDate).FirstOrDefault();
                        if (CreationDate38 != null)
                        {
                            need32.CreationDate38 = Convert.ToString(CreationDate38);
                        }
                        else
                        {
                            need32.CreationDate38 = null;
                        }


                        var LastModifiedBy38 = n38.Select(x => x.LastModifiedBy).FirstOrDefault();
                        need32.LastModifiedBy38 = LastModifiedBy38 ?? null;

                        var LastModifiedDate38 = n38.Select(x => x.LastModifiedDate).FirstOrDefault();
                        need32.LastModifiedDate38 = Convert.ToString(LastModifiedDate38) ?? null;

                        var TaskExtent = n38.Select(x => x.TaskExtent).FirstOrDefault();
                        need32.TaskExtent = TaskExtent ?? null;

                        var TaskFrequency = n38.Select(x => x.TaskFrequency).FirstOrDefault();
                        need32.TaskFrequency = Convert.ToString(TaskFrequency) ?? null;

                        var TaskInterval = n38.Select(x => x.TaskInterval).FirstOrDefault();
                        need32.TaskInterval = TaskInterval ?? null;

                        var TaskName = n38.Select(x => x.TaskName).FirstOrDefault();
                        need32.TaskName = TaskName ?? null;

                        var TaskReference = n38.Select(x => x.TaskReference).FirstOrDefault();
                        need32.TaskReference = TaskReference ?? null;

                        var MitigationType = n38.Select(x => x.MitigationType).FirstOrDefault();
                        need32.MitigationType = MitigationType ?? null;

                        var DueDate = n38.Select(x => x.DueDate).FirstOrDefault();
                        need32.DueDate = Convert.ToString(DueDate) ?? null;

                        var IsStaticDueDate = n38.Select(x => x.IsStaticDueDate).FirstOrDefault();
                        need32.IsStaticDueDate = Convert.ToString(IsStaticDueDate) ?? null;


                        var HistoricInspectionDate = n38.Select(x => x.HistoricInspectionDate).FirstOrDefault();
                        need32.HistoricInspectionDate = Convert.ToString(HistoricInspectionDate) ?? null;


                        var MasterInspectionMethodId = n38.Select(x => x.MasterInspectionMethodId).FirstOrDefault();
                        need32.MasterInspectionMethodId = Convert.ToString(MasterInspectionMethodId) ?? null;


                        var ManualLastInspectionDate = n38.Select(x => x.ManualLastInspectionDate).FirstOrDefault();
                        need32.ManualLastInspectionDate = Convert.ToString(ManualLastInspectionDate) ?? null;


                        //38-39
                        //39-40
                        var InspectionPlanTaskId38 = n38.Select(x => x.InspectionPlanTaskId).FirstOrDefault();
                        var n391 = W0NKME39.Where(x => x.InspectionPlanTaskId.ToString() == InspectionPlanTaskId38.ToString()).Select(x => new { x.InspectionPlanTaskId, x.MitigationColumnId, x.CellText }).ToList();
                        var n401 = W0NKME40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "LOCATION").Join(n391, x => x.MitigationColumnId.ToString(), n => n.MitigationColumnId.ToString(), (x, n) => new { n.CellText }).FirstOrDefault();
                        if (n401 != null) // 確認 n40 不為 null
                        {
                            need32.LOCATION = n401.CellText;

                        }
                        else
                        {
                            need32.LOCATION = null;
                        }


                        var celltext39 = W0NKME39.Where(x => x.InspectionPlanTaskId.ToString() == InspectionPlanTaskId38.ToString()).Select(x => new { x.CellText }).FirstOrDefault();


                        var n39 = W0NKME39.Where(x => n38.Any(n => n.InspectionPlanTaskId.ToString() == x.InspectionPlanTaskId.ToString())).Select(x => new { x.InspectionPlanTaskId, x.MitigationColumnId, x.CellText }).ToList();
                        var n40 = W0NKME40.Where(x => n39.Any(n => n.MitigationColumnId.ToString() == x.MitigationColumnId.ToString())).Select(x => new { x.HeaderText, x.MitigationColumnId }).ToList();
                        //need32.LOCATION2 = celltext39.CellText;


                        var AVAILABILITY = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "AVAILABILITY").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (AVAILABILITY != null)
                        {

                            need32.AVAILABILITY = celltext39.CellText;
                        }
                        else
                        {
                            need32.AVAILABILITY = null;
                        }

                        var NoBarrPenetrationsRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "BARRIER PENETRATION").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoBarrPenetrationsRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoBarrPenetrationsRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoBarrPenetrationsRdgs = Convert.ToString(b);
                            }

                        }
                        else
                        {
                            need32.NoBarrPenetrationsRdgs = null;
                        }

                        var NoDeadLegRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "DEAD LEGS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoDeadLegRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoDeadLegRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoDeadLegRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoDeadLegRdgs = null;
                        }

                        var NoDmgdInsdRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "DAMAGED INSULATION").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoDmgdInsdRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoDmgdInsdRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoDmgdInsdRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoDmgdInsdRdgs = null;
                        }

                        var NoElbowRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "ELBOWS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoElbowRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoElbowRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoElbowRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoElbowRdgs = null;
                        }

                        var NoErosionZoneRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "EROSION ZONES").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoErosionZoneRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoErosionZoneRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoErosionZoneRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoErosionZoneRdgs = null;
                        }

                        var NoHorizLowPtRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "HORIZONTAL LOW POINTS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoHorizLowPtRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoHorizLowPtRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoHorizLowPtRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoHorizLowPtRdgs = null;
                        }

                        var NoInsdTerminatorRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "INSULATION TERMINATIONS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoInsdTerminatorRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoInsdTerminatorRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoInsdTerminatorRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoInsdTerminatorRdgs = null;
                        }

                        var NoLongHorizRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "LONG HORIZONTAL RUNS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoLongHorizRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoLongHorizRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoLongHorizRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoLongHorizRdgs = null;
                        }

                        var NoReducerRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "REDUCERS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoReducerRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoReducerRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoReducerRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoReducerRdgs = null;
                        }

                        var NoSoilToAirIntfRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "SOIL-AIR INTERFACE").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoSoilToAirIntfRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoSoilToAirIntfRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoSoilToAirIntfRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoSoilToAirIntfRdgs = null;
                        }

                        var NoTeeRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "TEES").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoTeeRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoTeeRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoTeeRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoTeeRdgs = null;
                        }

                        var NoVertRunRdgs = n40.Where(x => !x.HeaderText.IsEmpty() && x.HeaderText.ToString() == "VERTICAL RUNS").Select(x => new { x.MitigationColumnId }).FirstOrDefault();
                        if (NoVertRunRdgs != null)
                        {
                            double a = Convert.ToDouble(celltext39.CellText);
                            if (a >= 0 && a <= 2)
                            {
                                need32.NoVertRunRdgs = "2";
                            }
                            else
                            {
                                double b = Math.Ceiling(a);
                                need32.NoVertRunRdgs = Convert.ToString(b);
                            }
                        }
                        else
                        {
                            need32.NoVertRunRdgs = null;
                        }













                    }


                    return Ok(DataList_T0NKME08model);






                }

            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }





        }
    }
}