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
    public class T0NKME0602Controller : ApiController
    {
        // GET: T0NKME0602
        public IHttpActionResult Get()
        {
            List<T0NKME06model> DataList_T0NKME06model = new List<T0NKME06model>();
            object lockMe = new object();

            HttpResponseMessage responsehttp = new HttpResponseMessage();


            try
            {
                using (Entities2 db = new Entities2())
                {

                    //var W0NKME02 = db.ComponentDetails.ToList();
                    //var W0NKME04 = db.HierarchyTemplates.ToList();
                    //var W0NKME03 = db.OrgTreeNodes.ToList();
                    //var W0NKME24 = db.OrgTreeNodeModelRunInputs.ToList();
                    //var W0NKME32 = db.OrgTreeNodeModelRuns.ToList();
                    //var W0NKME31 = db.NodeInputMetadata.ToList();
                    //var W0NKME29 = db.ModelNodeMetadata.ToList();

                    //32=24
                    var getAllComponent = from otmr in db.OrgTreeNodeModelRuns
                                          join otmri in db.OrgTreeNodeModelRunInputs on otmr.OrgTreeNodeModelRunId equals otmri.OrgTreeNodeModelRunId
                                          join otmro in db.OrgTreeNodeModelRunOutputs on otmr.OrgTreeNodeModelRunId equals otmro.OrgTreeNodeModelRunId
                                          //31 join nim in db.NodeInputMetadata on otmri.NodeInputId equals nim.NodeInputId
                                          //29join mnm in db.ModelNodeMetadata on otmri.ModelNodeId equals mnm.ModelNodeId

                                          join otn in db.OrgTreeNodes on otmr.OrgTreeNodeId equals otn.OrgTreeNodeId
                                          //join cd in db.ComponentDetails on otn.ComponentDetailsId equals cd.ComponentDetailsId

                                          select new
                                          {
                                              ModelNodeId = otmri.ModelNodeId.ToString(),
                                              NodeInputId = otmri.NodeInputId.ToString(),
                                              NodeOutputId = otmro.NodeOutputId.ToString(),

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

                                              //ComponentDetailsId = cd.ComponentDetailsId.ToString(),

                                              //OrgTreeNodeModelRunId= otmr.OrgTreeNodeModelRunInputs.ToString(),說是不能轉換成字串

                                              // ModelId= otmr.ModelId.ToString(),

                                              OrgTreeNodeId1 = otmr.OrgTreeNodeId.ToString(),
                                              ParentId = otn.ParentId.ToString()


                                          };
                    
                   
                    var W0NKME03 = db.OrgTreeNodes.ToList();
                    var fake = getAllComponent
                  //.AsEnumerable()
                  .Take(100);
                    //.ToList(); // 将 LINQ 对象转换为列表对象
                    //return Json(getAllComponent.Take(100).ToList());

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

                                                        ModelNodeId = item.ModelNodeId,
                                                        NodeInputId = item.NodeInputId,
                                                        
                                                        Value = item.Value,
                                                        DisplayValue = item.DisplayValue,
                                                        ProbabilityLabel = item.ProbabilityLabel,
                                                        ConsequenceLabel = item.ConsequenceLabel,
                                                        //NodeOutputId = item.NodeOutputId




                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                    });
                    return Ok(DataList_T0NKME06model);



                    //return Json(getAllComponent.Take(1000).ToList());

                    var fake2 = getAllComponent
                       //.AsEnumerable()
                       .Take(10)
                       .ToList(); // 将 LINQ 对象转换为列表对象

                    return Json(fake);


          

                    return Json(fake);



                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}