using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Plugin
{
    public class RetrieveMultiple_pre : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // 获取执行上下文
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            // 获取服务工厂
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            // 获取个人的组织服务
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            // 获取管理员组织服务
            IOrganizationService adminService = factory.CreateOrganizationService(null);
            if (adminService is OrganizationServiceProxy proxy)
            {
                proxy.CallerId = new Guid();
            }

            if (context.MessageName.ToLower() == "retrievemultiple")
            {
                EntityCollection ec = context.OutputParameters["BusinessEntityCollection"] as EntityCollection;

                if (context.InputParameters["Query"] is FetchExpression)
                {
                    return;
                }
                QueryExpression query = (QueryExpression)context.InputParameters["Query"];

                if (query.EntityName!= "new_label")
                {
                    return;
                }

                foreach (var item in ec.Entities)
                {
                    string fetch = $@"<fetch mapping='logical' version='1.0'>
                                      <entity name='new_label'>
                                           <attribute name='new_countrycode' />
                                        <filter>
                                          <condition attribute='new_labelid' operator='eq' value='{item.Id}' />
                                        </filter>
                                        <link-entity name='new_labeltran' from='new_labelid' to='new_labelid' alias='b' link-type='inner' >
                                            <attribute name='new_name' />
                                            <attribute name='new_countrycode' />
                                        </link-entity>
                                      </entity>
                                    </fetch>";
                    var ecTran = service.RetrieveMultiple(new FetchExpression(fetch));
                    if (ecTran.Entities.Count>0)
                    {
                        var tran = ecTran.Entities.FirstOrDefault(x => x.GetAttributeValue<int>("new_countrycode") == (int)((AliasedValue)(x["b.new_countrycode"])).Value);
                      
                        if (tran!=null)
                        {
                            item["new_name"] = (string)((AliasedValue)(tran["b.new_name"])).Value;
                        }
                    }
                }
            }

            if ((context.MessageName.ToLower() == "retrieve"))
            {
                Entity e = context.OutputParameters["BusinessEntity"] as Entity;
                if (e.LogicalName.ToLower() != "new_label")
                {
                    return;
                }
                QueryExpression query = new QueryExpression("new_labeltran");
                query.Criteria.AddCondition("new_labelid", ConditionOperator.Equal, e.Id);
                query.ColumnSet = new ColumnSet("new_name", "new_countrycode");

                LinkEntity linkEntity = new LinkEntity("new_labeltran", "new_label", "new_labelid", "new_labelid", JoinOperator.LeftOuter);
                linkEntity.Columns= new ColumnSet("new_name", "new_countrycode");
                linkEntity.EntityAlias = "b";
                query.LinkEntities.Add(linkEntity);
                var ec = service.RetrieveMultiple(query);
                if (ec.Entities.Count>0)
                {
                    var tran = ec.Entities.FirstOrDefault(x => x.GetAttributeValue<int>("new_countrycode") == (int)((AliasedValue)(x["b.new_countrycode"])).Value);
                    e["new_name"] = tran.GetAttributeValue<string>("new_name");
                }
            }
        }
    }
}
