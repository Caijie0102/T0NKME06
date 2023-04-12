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
using T0NKME06.Extensions;
using T0NKME06.Models;

namespace T0NKME06.Models
{
    public class T0NKME0801Controller : ApiController
    {
        // GET: T0NKME0801
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
                                         // join ip in db.InspectionPlans on otmr.OrgTreeNodeModelRunId equals ip.OrgTreeNodeModelRunId  //09
                                        //  join ipt in db.InspectionPlanTasks on ip.InspectionPlanId equals ipt.InspectionPlanId //38
                                         // join iptmc in db.InspectionPlanTaskMitigationColumns on ipt.InspectionPlanTaskId equals iptmc.InspectionPlanTaskId  //39
                                         // join mc in db.MitigationColumns on iptmc.MitigationColumnId equals mc.MitigationColumnId //40
                                          //join rc in db.Recommendations on otmr.OrgTreeNodeModelRunId equals rc.OrgTreeNodeModelRunId //08
                                          select new
                                          {
                                              

                                              OrgTreeNodeModelRunId32 = otmr.OrgTreeNodeModelRunId.ToString(),
                                            


                                              OrgTreeNodeId32 = otmr.OrgTreeNodeId.ToString(), //otmr與otn連接的
                                              ParentId = otn.ParentId.ToString(),   //otn的

                                              

                                              OrgTreeNodeId03 = otn.OrgTreeNodeId, //otmr與otn連接的
                                              OrgTreeNodeModelRunId=otmr.OrgTreeNodeModelRunId.ToString(),
                                              CreatedBy32 = otmr.CreatedBy.ToString(),
                                              CreationDate32 = otmr.CreationDate.ToString(),
                                              LastModifiedBy32 = otmr.LastModifiedBy.ToString(),
                                              LastModifiedDate32 = otmr.LastModifiedDate.ToString(),
                                              Name = otmr.Name.ToString(),
                                              ScenarioType = otmr.ScenarioType.ToString(),
                                              AnalysisDate = otmr.AnalysisDate.ToString(),
                                              MassCriticalityRunId = otmr.MassCriticalityRunId.ToString(),

                                              //09
                                              InspectionPlanId = (from ip in db.InspectionPlans
                                                                  where otmr.OrgTreeNodeModelRunId.ToString() == ip.OrgTreeNodeModelRunId.ToString()
                                                                  select ip.InspectionPlanId.ToString()).FirstOrDefault(),

                                              //09
                                              CreatedBy09 = (from ip in db.InspectionPlans
                                                                  where otmr.OrgTreeNodeModelRunId.ToString() == ip.OrgTreeNodeModelRunId.ToString()
                                                                  select ip.CreatedBy).FirstOrDefault(),

                                              //09
                                              CreationDate09 = (from ip in db.InspectionPlans
                                                             where otmr.OrgTreeNodeModelRunId.ToString() == ip.OrgTreeNodeModelRunId.ToString()
                                                             select ip.CreationDate).FirstOrDefault(),


                                              //09
                                              InspectionPlanStatus = (from ip in db.InspectionPlans
                                                                where otmr.OrgTreeNodeModelRunId.ToString() == ip.OrgTreeNodeModelRunId.ToString()
                                                                select ip.InspectionPlanStatus).FirstOrDefault(),

                                              //38
                                              CreatedBy38 = (from ipt in db.InspectionPlanTasks
                                                             join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                             where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                             select ipt.CreatedBy).FirstOrDefault(),

                                              //38
                                              CreationDate38 = (from ipt in db.InspectionPlanTasks
                                                                join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                select ipt.CreationDate).FirstOrDefault(),

                                              //38
                                              LastModifiedBy38 = (from ipt in db.InspectionPlanTasks
                                                                  join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                  where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                  select ipt.LastModifiedBy).FirstOrDefault(),

                                              //38
                                              LastModifiedDate38 = (from ipt in db.InspectionPlanTasks
                                                                    join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                    where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                    select ipt.LastModifiedDate).FirstOrDefault(),

                                              //38
                                              TaskExtent = (from ipt in db.InspectionPlanTasks
                                                            join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                            where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                            select ipt.TaskExtent).FirstOrDefault(),

                                              //38
                                              TaskFrequency = (from ipt in db.InspectionPlanTasks
                                                               join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                               where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                               select ipt.TaskFrequency).FirstOrDefault(),

                                              //38
                                              TaskInterval = (from ipt in db.InspectionPlanTasks
                                                              join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                              where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                              select ipt.TaskInterval).FirstOrDefault(),

                                              //38
                                              TaskName = (from ipt in db.InspectionPlanTasks
                                                          join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                          where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                          select ipt.TaskName).FirstOrDefault(),

                                              //38
                                              TaskReference = (from ipt in db.InspectionPlanTasks
                                                               join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                               where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                               select ipt.TaskReference).FirstOrDefault(),

                                              //38
                                              MitigationType = (from ipt in db.InspectionPlanTasks
                                                                join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                select ipt.MitigationType).FirstOrDefault(),

                                              //38
                                              DueDate = (from ipt in db.InspectionPlanTasks
                                                         join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                         where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                         select ipt.DueDate).FirstOrDefault(),

                                              //38
                                              IsStaticDueDate = (from ipt in db.InspectionPlanTasks
                                                                 join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                 where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                 select ipt.IsStaticDueDate).FirstOrDefault(),

                                              //38
                                              HistoricInspectionDate = (from ipt in db.InspectionPlanTasks
                                                                        join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                        where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                        select ipt.HistoricInspectionDate).FirstOrDefault(),

                                              //38
                                              MasterInspectionMethodId = (from ipt in db.InspectionPlanTasks
                                                                          join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                          where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                          select ipt.MasterInspectionMethodId).FirstOrDefault(),

                                              //38
                                              ManualLastInspectionDate = (from ipt in db.InspectionPlanTasks
                                                                          join ip in db.InspectionPlans on ipt.InspectionPlanId equals ip.InspectionPlanId
                                                                          where ip.InspectionPlanId.ToString() == ipt.InspectionPlanId.ToString()
                                                                          select ipt.ManualLastInspectionDate).FirstOrDefault(),










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

                                                        
                                                        OrgTreeNodeModelRunId = item.OrgTreeNodeModelRunId,
                                                        InspectionPlanId = item.InspectionPlanId,

                                                        CreatedBy32 = item.CreatedBy32,
                                                        CreationDate32 = item.CreationDate32,
                                                        LastModifiedBy32 = item.LastModifiedBy32,
                                                        LastModifiedDate32 = item.LastModifiedDate32,
                                                        Name = item.Name,
                                                        ScenarioType = item.ScenarioType,
                                                        AnalysisDate = item.AnalysisDate,
                                                        MassCriticalityRunId = item.MassCriticalityRunId,
                                                        CreatedBy09 = item.CreatedBy09,
                                                        CreationDate09 = item.CreationDate09.ToString(),
                                                        InspectionPlanStatus = item.InspectionPlanStatus.ToString(),
                                                        CreatedBy38 = item.CreatedBy38,
                                                        CreationDate38 = item.CreationDate38.ToString(),
                                                        LastModifiedBy38 = item.LastModifiedBy38,
                                                        LastModifiedDate38 = item.LastModifiedDate38.ToString(),
                                                        TaskExtent = item.TaskExtent,
                                                        TaskFrequency = item.TaskFrequency.ToString(),
                                                        TaskInterval = item.TaskInterval,

                                                        TaskName = item.TaskName,
                                                        TaskReference = item.TaskReference,
                                                        MitigationType = item.MitigationType,
                                                        DueDate = item.DueDate.ToString(),
                                                        IsStaticDueDate = item.IsStaticDueDate.ToString(),
                                                        HistoricInspectionDate = item.HistoricInspectionDate.ToString(),
                                                        MasterInspectionMethodId = item.MasterInspectionMethodId.ToString(),
                                                        ManualLastInspectionDate = item.ManualLastInspectionDate.ToString(),











                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });

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