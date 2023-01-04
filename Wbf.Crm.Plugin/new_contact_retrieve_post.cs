using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wbf.Crm.Common;

namespace Wbf.Crm.Plugin
{
    public class new_contact_retrieve_post : IPlugin
    {
        protected IOrganizationService service;
        protected IOrganizationService adminserver;
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

            if (context.MessageName.ToLower()== "retrieve")
            {
                if (context.OutputParameters.ContainsKey("BusinessEntity") && context.OutputParameters["BusinessEntity"] is Entity entity)
                {
                    if (entity.LogicalName=="contact"&&entity.Contains("emailaddress1"))
                    {
                        entity["emailaddress1"] = "wubf@edensoft.com.cn";
                    }
                }
            }
            else if (context.MessageName.ToLower() == "retrievemultiple")
            {
                throw new InvalidPluginExecutionException(CrmHelper.GetTest());
                if (context.OutputParameters.ContainsKey("BusinessEntityCollection") && context.OutputParameters["BusinessEntityCollection"] is EntityCollection collection)
                {
                    
                    if (collection.EntityName == "contact")
                    {
                        foreach (var item in collection.Entities)
                        {
                            if (!item.GetAttributeValue<string>("emailaddress1").Contains("320031"))
                            {
                                continue;
                            }
                            item["emailaddress1"] = "wanzhangbingfeng@outlook.com";
                            if (context.UserId==null)
                            {
                                var contact = new Entity(item.LogicalName, item.Id);
                                contact["new_openid"] ="user is null";
                                service.Update(contact);
                                return;
                            }
                            string fetch = $@"<fetch mapping='logical' version='1.0'>
                                              <entity name='systemuserroles'>
                                                <filter>
                                                  <condition attribute='systemuserid' operator='eq' value='{context.UserId}' />
                                                </filter>
                                                <link-entity name='role' from='roleid' to='roleid' alias='r' link-type='outer'>
                                                  <attribute name='name' />
                                                </link-entity>
                                                <link-entity name='systemuser' from='systemuserid' to='systemuserid' alias='u' link-type='outer'>
                                                  <attribute name='domainname' />
                                                </link-entity>
                                              </entity>
                                            </fetch>";
                            var ecTran = service.RetrieveMultiple(new FetchExpression(fetch));
                            if (ecTran.Entities.Count>0)
                            {
                                string rolename = "";
                                foreach (var role in ecTran.Entities)
                                {
                                    rolename+= (string)((AliasedValue)(role["r.name"])).Value+"；";
                                }
                                var contact = new Entity(item.LogicalName, item.Id);
                                contact["new_openid"] = (string)((AliasedValue)(ecTran.Entities[0]["u.domainname"])).Value + "：" + rolename;
                                service.Update(contact);
                            }
                        }
                    }
                }
            }
        }
    }
}
