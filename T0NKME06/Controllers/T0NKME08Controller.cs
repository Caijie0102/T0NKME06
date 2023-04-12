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
    public class T0NKME08Controller : ApiController
    {
        // GET: T0NKME08
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
                                          join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId
                                          join ip in db.InspectionPlans on otmr.OrgTreeNodeModelRunId equals ip.OrgTreeNodeModelRunId  //09
                                          /**join ipt in db.InspectionPlanTasks on ip.InspectionPlanId equals ipt.InspectionPlanId //38
                                          join iptmc in db.InspectionPlanTaskMitigationColumns on ipt.InspectionPlanTaskId equals iptmc.InspectionPlanTaskId  //39
                                          join mc in db.MitigationColumns on iptmc.MitigationColumnId equals mc.MitigationColumnId //40
                                          join rc in db.Recommendations on otmr.OrgTreeNodeModelRunId equals rc.OrgTreeNodeModelRunId //08 */
                                          select new
                                          {


                                             // OrgTreeNodeModelRunId32 = otmr.OrgTreeNodeModelRunId.ToString(),
                                              OrgTreeNodeId32 = otmr.OrgTreeNodeId.ToString(), //otmr與otn連接的
                                              ParentId = otn.ParentId.ToString(),   //otn的
                                              OrgTreeNodeId03 = otn.OrgTreeNodeId, //otmr與otn連接的
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
                    // return Ok(getAllComponent.Take(100).ToList());
                  

                    

                    var W0NKME03 = db.OrgTreeNodes.ToList();

                    var fake = getAllComponent
                  //.AsEnumerable() //19
                  .Take(1000);
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

                                                    DataList_T0NKME08model.Add(new T0NKME08model()
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

                                                        OrgTreeNodeModelRunId=item.OrgTreeNodeModelRunId.ToString(),
                                                        InspectionPlanId = item.InspectionPlanId.ToString(), //09?
                                                        CreatedBy09 = item.CreatedBy09.ToString(),
                                                        CreationDate09 = item.CreationDate09.ToString(),
                                                        InspectionPlanStatus = item.InspectionPlanStatus.ToString(),

                                                        CreatedBy32 = item.CreatedBy32.ToString(),
                                                        CreationDate32 = item.CreationDate32.ToString(),
                                                        LastModifiedBy32 = item.LastModifiedBy32.ToString(),
                                                        LastModifiedDate32 = item.LastModifiedDate32.ToString(),
                                                        Name = item.Name.ToString(),
                                                        ScenarioType = item.ScenarioType.ToString(),
                                                        AnalysisDate = item.AnalysisDate.ToString(),
                                                        MassCriticalityRunId = item.MassCriticalityRunId.ToString(),

                                             

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
                        var n38= W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.InspectionPlanTaskId, x.InspectionPlanId }).ToList();
                        //var n39= W0NKME39.Where(x => x.InspectionPlanTaskId.ToString() == need32.InspectionPlanTaskId.ToString()).Select(x => new { x.InspectionPlanTaskId, x.InspectionPlanId }).ToList();


                        //09-38
                        //38-39
                        //39-40
                        //var get382 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.InspectionPlanTaskId }).FirstOrDefault();
                        var get38 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new {  x.InspectionPlanTaskId,x.InspectionPlanId,x.CreatedBy, x.CreationDate ,
                        x.LastModifiedBy,x.LastModifiedDate,x.TaskExtent,x.TaskFrequency,x.TaskInterval,x.TaskName,x.TaskReference,x.MitigationType,x.DueDate,x.IsStaticDueDate,x.HistoricInspectionDate,
                            x.MasterInspectionMethodId,x.ManualLastInspectionDate
                        }).ToList();


                        var CreatedBy38 = get38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.CreatedBy }).FirstOrDefault();

                        if (CreatedBy38 != null)
                        {
                            need32.CreatedBy38 = CreatedBy38.ToString();
                        }
                        else
                        {
                            need32.CreatedBy38 = null;
                        }


                        var CreationDate38 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.CreationDate }).FirstOrDefault();

                        if (CreationDate38 != null)
                        {
                            need32.CreationDate38 = CreationDate38.ToString();
                        }
                        else
                        {
                            need32.CreationDate38 = null;
                        }

                        var LastModifiedBy38 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.LastModifiedBy }).FirstOrDefault();

                        if (LastModifiedBy38 != null)
                        {
                            need32.LastModifiedBy38 = LastModifiedBy38.ToString();
                        }
                        else
                        {
                            need32.LastModifiedBy38 = null;
                        }
                        var LastModifiedDate38 = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.LastModifiedDate }).FirstOrDefault();

                        if (LastModifiedDate38 != null)
                        {
                            need32.LastModifiedDate38 = LastModifiedDate38.ToString();
                        }
                        else
                        {
                            need32.LastModifiedDate38 = null;
                        }
                        var TaskExtent = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.TaskExtent }).FirstOrDefault();

                        if (TaskExtent != null)
                        {
                            need32.TaskExtent = TaskExtent.ToString();
                        }
                        else
                        {
                            need32.TaskExtent = null;
                        }
                        var TaskFrequency = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.TaskFrequency }).FirstOrDefault();

                        if (TaskFrequency != null)
                        {
                            need32.TaskFrequency = TaskFrequency.ToString();
                        }
                        else
                        {
                            need32.TaskFrequency = null;
                        }
                        var TaskInterval = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.TaskInterval }).FirstOrDefault();

                        if (TaskInterval != null)
                        {
                            need32.TaskInterval = TaskInterval.ToString();
                        }
                        else
                        {
                            need32.TaskInterval = null;
                        }
                        var TaskName = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.TaskName }).FirstOrDefault();

                        if (TaskName != null)
                        {
                            need32.TaskName = TaskName.ToString();
                        }
                        else
                        {
                            need32.TaskName = null;
                        }

                        var TaskReference = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.TaskReference }).FirstOrDefault();

                        if (TaskReference != null)
                        {
                            need32.TaskReference = TaskReference.ToString();
                        }
                        else
                        {
                            need32.TaskReference = null;
                        }

                        var MitigationType = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.MitigationType }).FirstOrDefault();

                        if (MitigationType != null)
                        {
                            need32.MitigationType = MitigationType.ToString();
                        }
                        else
                        {
                            need32.MitigationType = null;
                        }

                        var DueDate = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.DueDate }).FirstOrDefault();

                        if (DueDate != null)
                        {
                            need32.DueDate = DueDate.ToString();
                        }
                        else
                        {
                            need32.DueDate = null;
                        }

                        var IsStaticDueDate = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.IsStaticDueDate }).FirstOrDefault();

                        if (IsStaticDueDate != null)
                        {
                            need32.IsStaticDueDate = IsStaticDueDate.ToString();
                        }
                        else
                        {
                            need32.IsStaticDueDate = null;
                        }

                        var HistoricInspectionDate = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.HistoricInspectionDate }).FirstOrDefault();

                        if (HistoricInspectionDate != null)
                        {
                            need32.HistoricInspectionDate = HistoricInspectionDate.ToString();
                        }
                        else
                        {
                            need32.HistoricInspectionDate = null;
                        }

                        var MasterInspectionMethodId = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.MasterInspectionMethodId }).FirstOrDefault();

                        if (MasterInspectionMethodId != null)
                        {
                            need32.MasterInspectionMethodId = MasterInspectionMethodId.ToString();
                        }
                        else
                        {
                            need32.MasterInspectionMethodId = null;
                        }

                        var ManualLastInspectionDate = W0NKME38.Where(x => x.InspectionPlanId.ToString() == need32.InspectionPlanId.ToString()).Select(x => new { x.ManualLastInspectionDate }).FirstOrDefault();

                        if (ManualLastInspectionDate != null)
                        {
                            need32.ManualLastInspectionDate = ManualLastInspectionDate.ToString();
                        }
                        else
                        {
                            need32.ManualLastInspectionDate = null;
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