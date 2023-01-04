using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Test.Common
{
    public static class CrmHelper
    {
        /// <summary>
        /// 获取组织服务
        /// </summary>
        /// <returns></returns>
        public static IOrganizationService GetOrganization()
        {
            CrmServiceClient conn = new CrmServiceClient("AuthType=AD;Url=http://10.20.7.14:5555/CRM; Domain=WHEDEN; Username=crmadmin; Password=Eden.com");
            return conn.OrganizationServiceProxy;
        }

        public static void TestPlugin()
        {
            IOrganizationService service = GetOrganization();

            Entity pluginTestInfo = service.Retrieve("new_plugin_test", Guid.Parse("B3F072F3-0DDC-EC11-8ABE-000C296FDCA2"), new ColumnSet(true));

            var context = new XrmFakedContext();
            //context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(new_plugin_test));

            var le = new List<Entity>() { pluginTestInfo };
            //初始化插件查询需要用到的数据
            context.Initialize(le);

            //实际修改的数据
            var updateInfo = new Entity(pluginTestInfo.LogicalName, pluginTestInfo.Id);
            updateInfo["new_code"] = "code_123";
            updateInfo["new_name"] = "name_123";

            XrmFakedPluginExecutionContext plugCtx = context.GetDefaultPluginContext();
            plugCtx.PrimaryEntityName = updateInfo.LogicalName;
            plugCtx.MessageName = "Update";
            plugCtx.InputParameters = new ParameterCollection { { "Target", updateInfo } };
            //plugCtx.OutputParameters = outputParameters; 
            //https://docs.microsoft.com/zh-tw/previous-versions/dynamicscrm-2015/crm.7/gg326861(v=crm.7)
            //Valid values are 10 (pre-validation), 20 (pre-operation), 40 (post-operation), and 50 (post-operation, deprecated).
            plugCtx.Stage = 40;

            //context.ExecutePluginWith<new_travelcost_update_post>(plugCtx);
        }
    }
}
