using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //CrmServiceClient con = new CrmServiceClient("Url=https://honordev.crm5.dynamics.com;Username=crmdev@hihonor.onmicrosoft.com;Password=honor@dev123;authtype=Office365");
            new AxureCon().testAsync();

            //IOrganizationService service = con.OrganizationServiceProxy;
            //{
            //    var contactEntity = service.Retrieve("contact", new Guid("b7b07f7d-ce63-ed11-9560-6045bd226127"), new ColumnSet("donotbulkemail", "new_businessunitid"));
            //    if (contactEntity == null)
            //        return;

            //    if (contactEntity.GetAttributeValue<bool>("donotbulkemail"))
            //    {
            //        var businessunitId = contactEntity.GetAttributeValue<EntityReference>("new_businessunitid").Id;

            //        var segmentResult = GetSegmentDataList(service, businessunitId);


            //        var contactId = JsonConvert.SerializeObject(new List<string>() { "b7b07f7d-ce63-ed11-9560-6045bd226127" });

            //        foreach (var item in segmentResult)
            //        {
            //            OrganizationRequest request = new OrganizationRequest("msdyncrm_SegmentMembersUpdate");
            //            request["msdyncrm_segmentid"] = item.Id.ToString();
            //            request["msdyncrm_operation"] = "removeByIds";
            //            request["msdyncrm_memberids"] = contactId;
            //            service.Execute(request);
            //        }
            //    }
            //}
            Console.ReadKey();
            
        }

        static List<Entity> GetSegmentDataList(IOrganizationService service, Guid businessid)
        {
            QueryExpression query = new QueryExpression("msdyncrm_segment");
            query.Criteria.AddCondition("statuscode", ConditionOperator.Equal, 192350001);
            query.Criteria.AddCondition("new_businessunitid", ConditionOperator.Equal, businessid);
            query.ColumnSet.AddColumns("new_number", "msdyncrm_segmentqueryname");

            var data = service.RetrieveMultiple(query);

            return data.Entities.ToList();
        }
    }
}
