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
            string start = (DateTime.Now.ToString("HH:mm ss tt"));

            List<T0NKME08model> DataList_T0NKME08model = new List<T0NKME08model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();

            try
            {
                using (Entities2 db = new Entities2())
                {




                    var getAllComponent = from otmr in db.OrgTreeNodeModelRuns  //32
                                          join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId //03
                                          where otmr.ScenarioType==1  //??
                                          // join ip in db.InspectionPlans on otmr.OrgTreeNodeModelRunId equals ip.OrgTreeNodeModelRunId  //09


                                          select new
                                          {

                                              OrgTreeNodeId32 = otn.OrgTreeNodeId.ToString(), //otmr與otn連接的

                                              AssetDetailsId = otn.AssetDetailsId.ToString(),


                                              OrgTreeNodeModelRunId = otmr.OrgTreeNodeModelRunId,
                                              CreatedBy32 = otmr.CreatedBy,
                                              CreationDate32 = otmr.CreationDate,
                                              LastModifiedBy32 = otmr.LastModifiedBy,
                                              LastModifiedDate32 = otmr.LastModifiedDate,
                                              Name = otmr.Name,
                                              ScenarioType = otmr.ScenarioType,
                                              AnalysisDate = otmr.AnalysisDate,
                                              MassCriticalityRunId = otmr.MassCriticalityRunId,

                                              /**
                                              //09
                                              InspectionPlanId = ip.InspectionPlanId.ToString(),
                                              //09
                                              CreatedBy09 = ip.CreatedBy.ToString(),
                                              //09
                                              CreationDate09 = ip.CreationDate.ToString(),
                                              //09
                                              InspectionPlanStatus = ip.InspectionPlanStatus.ToString(),
                                              */



                                          };
                   



                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var W0NKME09 = db.InspectionPlans.ToList();

                    var fake = getAllComponent
                  //.AsEnumerable() //19
                  .Take(500);
                    //.ToList(); // 将 LINQ 对象转换为列表对象

                    Parallel.ForEach(fake, item =>
                    {
                        string InspectionPlanId09 = ""; //09
                        string CreatedBy09 = ""; //09
                        string CreationDate09 = ""; //09
                        string InspectionPlanStatus09 = ""; //09
                        var InspectionPlans = W0NKME09.Where(x =>x.InspectionPlanStatus==2 && x.OrgTreeNodeModelRunId.ToString() == item.OrgTreeNodeModelRunId.ToString()).FirstOrDefault();
                        if (InspectionPlans != null)
                        {
                            InspectionPlanId09 = InspectionPlans.InspectionPlanId.ToString();
                            CreatedBy09 = InspectionPlans.CreatedBy.ToString();
                            CreationDate09 = InspectionPlans.CreationDate.ToString();
                            InspectionPlanStatus09 = InspectionPlans.InspectionPlanStatus.ToString();


                        }
                        else
                        {
                            InspectionPlanId09 = "";
                            CreatedBy09 = "";
                            CreationDate09 = "";
                            InspectionPlanStatus09 = "";

                        }

                        //在這個迴圈中，程式碼使用Where方法從"W0NKME03"集合中過濾出元件（Components），以便獲取該元件的詳細資料，例如組織、位置、系統、設備、元件ID等。使用Select方法從過濾後的元件集合中，選擇需要的屬性來建立一個匿名物件，然後使用FirstOrDefault方法來取得第一個符合條件的元素，如果沒有符合條件的元素，則返回null。

                        var getAssetitem = W0NKME03.Where(x => x.OrgTreeNodeId.ToString() == item.OrgTreeNodeId32.ToString()).Select(x => new { x.OrgTreeNodeId, x.ParentId, x.Name, x.Description, x.InstallationDate }).FirstOrDefault();
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


                                                    InspectionPlanId = InspectionPlanId09,
                                                    CreatedBy09 = CreatedBy09,
                                                    CreationDate09 = CreationDate09,
                                                    InspectionPlanStatus = InspectionPlanStatus09,


                                                    OrgTreeNodeModelRunId = item.OrgTreeNodeModelRunId.ToString(),
                                                    CreatedBy32 = item.CreatedBy32.ToString(),
                                                    CreationDate32 = item.CreationDate32.ToString(),
                                                    LastModifiedBy32 = item.LastModifiedBy32.ToString(),
                                                    LastModifiedDate32 = item.LastModifiedDate32.ToString(),
                                                    Name = item.Name,
                                                    ScenarioType = item.ScenarioType.ToString(),
                                                    AnalysisDate = item.AnalysisDate.ToString(),
                                                    MassCriticalityRunId = item.MassCriticalityRunId.ToString(),

                                                    //InspectionPlanTaskId = item.InspectionPlanTaskId.ToString(),
                                                    //MitigationColumnId = item.MitigationColumnId.ToString(),



                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // }



                    });

                    //return Ok(DataList_T0NKME08model);

                    var W0NKME38 = db.InspectionPlanTasks.ToList();
                     var W0NKME39 = db.InspectionPlanTaskMitigationColumns.ToList();
                     var W0NKME40 = db.MitigationColumns.ToList();
                    // var W0NKME08 = db.Recommendations.ToList();

                    foreach (var need32 in DataList_T0NKME08model)
                    {


                        var InspectionPlanTasks = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).FirstOrDefault();
                        if (InspectionPlanTasks != null)
                        {
                            //38-39
                            var InspectionPlanTaskMitigationColumns = W0NKME39.Where(x => x.InspectionPlanTaskId.ToString() == InspectionPlanTasks.InspectionPlanTaskId.ToString()).FirstOrDefault();
                            if (InspectionPlanTaskMitigationColumns!= null)
                            {
                                //39-40
                                var MitigationColumns = W0NKME40.Where(x => x.MitigationColumnId.ToString() == InspectionPlanTaskMitigationColumns.MitigationColumnId.ToString()).FirstOrDefault();
                                if (MitigationColumns != null)
                                {
                                    /**
                                     * InspectionPlanTaskMitigationColumns.CellText為字串，但要轉乘double來比較
                                     */
                                    string a1 = null;
                                    string aa = InspectionPlanTaskMitigationColumns.CellText;
                                    //double bb;
                                    if (double.TryParse(aa,out double b))
                                    {//轉換成功變b
                                        if (b >= 0 && b <= 2)
                                        {
                                            a1 = "2";
                                        }
                                        else
                                        {
                                            int bb = (int)Math.Ceiling(b);
                                            a1 = Convert.ToString(bb);
                                        }

                                    }
                                    else
                                    {
                                        aa = aa;
                                    }
                                    //a1 = Convert.ToString(b);



                                    /**string a1;
                                    double a = Convert.ToDouble(InspectionPlanTaskMitigationColumns.CellText);
                                    if(a >= 0 && a <= 2)
                                    {
                                        a1 = "2";
                                    }
                                    else
                                    {
                                        double b = Math.Ceiling(a);
                                        a1= Convert.ToString(b);
                                    }*/


                                    switch (MitigationColumns.HeaderText)
                                {
                                    case "LOCATION":
                                        need32.LOCATION = InspectionPlanTaskMitigationColumns.CellText; break;
                                    case "AVAILABILITY":
                                        need32.AVAILABILITY = InspectionPlanTaskMitigationColumns.CellText; break;
                                    case "BARRIER PENETRATION":
                                        need32.NoBarrPenetrationsRdgs = a1; break;
                                    case "DEAD LEGS":
                                        need32.NoDeadLegRdgs = a1; break;
                                    case "DAMAGED INSULATION":
                                         need32.NoDmgdInsdRdgs = a1; break;

                                    case "ELBOWS":
                                         need32.NoElbowRdgs = a1; break;
                                    case "EROSION ZONES":
                                         need32.NoErosionZoneRdgs = a1; break;
                                    case "HORIZONTAL LOW POINTS":
                                         need32.NoHorizLowPtRdgs = a1; break;

                                    case "INSULATION TERMINATIONS":
                                         need32.NoInsdTerminatorRdgs = a1; break;
                                    case "LONG HORIZONTAL RUNS":
                                         need32.NoLongHorizRdgs = a1; break;
                                    case "REDUCERS":
                                         need32.NoReducerRdgs = a1; break;

                                    case "SOIL-AIR INTERFACE":
                                         need32.NoSoilToAirIntfRdgs = a1; break;
                                    case "TEES":
                                         need32.NoTeeRdgs = a1; break;
                                    case "VERTICAL RUNS":
                                         need32.NoVertRunRdgs = a1; break;


                                    default:
                                                
                                    break;

                                    }



                                need32.CreatedBy38 = InspectionPlanTasks.CreatedBy != null ? InspectionPlanTasks.CreatedBy : null;
                                need32.CreationDate38 = InspectionPlanTasks.CreationDate != null ? Convert.ToString(InspectionPlanTasks.CreationDate) : null;
                                need32.LastModifiedBy38 = InspectionPlanTasks.LastModifiedBy != null ? InspectionPlanTasks.LastModifiedBy : null;
                                need32.LastModifiedDate38 = InspectionPlanTasks.LastModifiedDate != null ? Convert.ToString(InspectionPlanTasks.LastModifiedDate) : null;
                                need32.TaskExtent = InspectionPlanTasks.TaskExtent != null ? InspectionPlanTasks.TaskExtent : null;
                                need32.TaskFrequency = InspectionPlanTasks.TaskFrequency != null ? Convert.ToString(InspectionPlanTasks.TaskFrequency) : null;
                                need32.TaskInterval = InspectionPlanTasks.TaskInterval != null ? InspectionPlanTasks.TaskInterval : null;
                                need32.TaskName = InspectionPlanTasks.TaskName != null ? InspectionPlanTasks.TaskName : null;
                                need32.TaskReference = InspectionPlanTasks.TaskReference != null ? InspectionPlanTasks.TaskReference : null;
                                need32.MitigationType = InspectionPlanTasks.MitigationType != null ? InspectionPlanTasks.MitigationType : null;
                                need32.DueDate = InspectionPlanTasks.DueDate != null ? Convert.ToString(InspectionPlanTasks.DueDate) : null;
                                need32.HistoricInspectionDate = InspectionPlanTasks.HistoricInspectionDate != null ? Convert.ToString(InspectionPlanTasks.HistoricInspectionDate) : null;
                                need32.MasterInspectionMethodId = InspectionPlanTasks.MasterInspectionMethodId != null ? Convert.ToString(InspectionPlanTasks.MasterInspectionMethodId) : null;
                                need32.ManualLastInspectionDate = InspectionPlanTasks.ManualLastInspectionDate != null ? Convert.ToString(InspectionPlanTasks.ManualLastInspectionDate) : null;


                                need32.IsStaticDueDate = InspectionPlanTasks.IsStaticDueDate;//bit(0 or 1)



                                need32.InspectionPlanTaskId = InspectionPlanTasks.InspectionPlanTaskId.ToString();
                                need32.MitigationColumnId = MitigationColumns.MitigationColumnId.ToString();

                            }
                        }
                        }
                      


                    }






                    string end = (DateTime.Now.ToString("HH:mm ss tt"));

                    var response = new
                    {

                        startTime = start.ToString(),
                       
                        endTime = end.ToString(),
                        Data = DataList_T0NKME08model
                    };

                   
                    return Ok(response);

                    //return Ok(DataList_T0NKME08model);






                }

            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }





        }
    }
}