using FakeXrmEasy;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wbf.Crm.Common;

namespace Wbf.Crm.Test
{
    class Program
    {
        private Dictionary<string, byte[]> _files = new Dictionary<string, byte[]>();
        static void Main(string[] args)
        {
            Console.WriteLine("开始");
            Console.WriteLine("请输入连接字符串");
            string connection = @"AuthType=IFD;url=https://cubedev.edenbssdemo.com/cubedev;domain=edenbssdemo.com;username=edenbssdemo\wubingfeng;password=Eden.com";

            while (string.IsNullOrWhiteSpace(connection))
            {
                Console.WriteLine("连接字符串不能为空，请重新输入");
                connection = Console.ReadLine();
            }
            CrmServiceClient conn = new CrmServiceClient(connection);
            var svc = (IOrganizationService)conn.OrganizationServiceProxy;
            
            while (svc==null)
            {
                Console.WriteLine("连接字符串不正确，请重新输入");
                connection = Console.ReadLine();
                conn = new CrmServiceClient(connection);
                svc = conn.OrganizationServiceProxy;
            }

            Console.WriteLine("请输入解决方案名称");
            string DefaultSolution = "test1";
            while (string.IsNullOrWhiteSpace(DefaultSolution))
            {
                Console.WriteLine("解决方案不能为空，请重新输入");
                DefaultSolution = Console.ReadLine();
            }
            var solution = CrmHelper.GetSolution(svc, DefaultSolution);
            if (solution==null)
            {
                Console.WriteLine($"解决方案{DefaultSolution}不存在");
            }
            {
               var form= CrmHelper.GetEntityForm(svc, 10223);
                foreach (var item in form.Entities)
                {
                    AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                    {
                        ComponentType = 60,//view
                        ComponentId = item.Id,
                        DoNotIncludeSubcomponents = true,
                        SolutionUniqueName = DefaultSolution
                    };
                    svc.Execute(addReq);
                }
            }
            {
                var view = CrmHelper.GetEntityView(svc, 10223);
                foreach (var item in view.Entities)
                {
                    AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                    {
                        ComponentType = 26,//view
                        ComponentId = item.Id,
                        DoNotIncludeSubcomponents = true,
                        SolutionUniqueName = DefaultSolution
                    };
                    svc.Execute(addReq);
                }
            }

            Console.WriteLine("请输入要添加的实体，输入【all】添加所有");
            string entity = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(entity))
            {
                Console.WriteLine("实体不能为空，请重新输入");
                entity = Console.ReadLine();
            }
            if (entity.ToLower()=="all")
            {
                var list = CrmHelper.GetAllEntity(svc);
                int count = list.Count;
                Console.WriteLine($"总共【{count}】个实体");
                int i = 0;
                foreach (var item in list)
                {
                    AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                    {
                        ComponentType = 1,//entity
                        ComponentId = item.MetadataId.Value,
                        DoNotIncludeSubcomponents = true,
                        SolutionUniqueName = DefaultSolution
                    };
                    svc.Execute(addReq);
                    Console.WriteLine($"已完成{i++}/{count}");
                }
            }
            else
            {
                //获取组件ID，添加到解决方案
                var respone = CrmHelper.GetEntityResponse(svc, entity);
                AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                {
                    ComponentType = 1,//entity
                    ComponentId = (Guid)respone.EntityMetadata.MetadataId,
                    DoNotIncludeSubcomponents = true,
                    SolutionUniqueName = DefaultSolution
                };
                svc.Execute(addReq);
            }
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("结束");
            Console.ReadKey();
        }
        
    }

    //public class CrmHelper
    //{
    //    /// <summary>
    //    /// 获取所有实体信息
    //    /// </summary>
    //    /// <returns></returns>
    //    public static List<EntityMetadata> GetAllEntity(IOrganizationService service)
    //    {
    //        List<EntityMetadata> crmEntities = new List<EntityMetadata>();
    //        RetrieveAllEntitiesRequest retrieveAll = new RetrieveAllEntitiesRequest
    //        {
    //            EntityFilters = EntityFilters.Entity,
    //            RetrieveAsIfPublished = true
    //        };
    //        var response = (RetrieveAllEntitiesResponse)service.Execute(retrieveAll);
    //        crmEntities = response.EntityMetadata
    //            .Where(x => x.DisplayName.UserLocalizedLabel != null && x.MetadataId != null && x.ObjectTypeCode.HasValue)
    //            .ToList();
    //        return crmEntities;
    //    }

    //    /// <summary>
    //    /// 获取实体数据
    //    /// </summary>
    //    /// <param name="service"></param>
    //    /// <param name="entityName"></param>
    //    /// <returns></returns>
    //    public static RetrieveEntityResponse GetEntityResponse(IOrganizationService service, string entityName)
    //    {
    //        RetrieveEntityRequest request = new RetrieveEntityRequest();
    //        request.LogicalName = entityName;
    //        return (RetrieveEntityResponse)service.Execute(request);
    //    }

    //    /// <summary>
    //    /// 获取解决方案,没有就新增
    //    /// </summary>
    //    /// <param name="svc"></param>
    //    /// <param name="solutionName">解决方案唯一名称</param>
    //    /// <returns></returns>
    //    public static Entity GetSolution(IOrganizationService svc, string solutionName)
    //    {
    //        QueryExpression querySolution = new QueryExpression("solution");
    //        querySolution.Criteria.AddCondition("uniquename", ConditionOperator.Equal, solutionName);
    //        querySolution.ColumnSet = new ColumnSet(true);
    //        var ec = svc.RetrieveMultiple(querySolution);
    //        if (ec.Entities.Count > 0)
    //        {
    //            return ec.Entities[0];
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}
}
