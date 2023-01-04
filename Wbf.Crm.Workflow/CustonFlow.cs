using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using System.Collections.ObjectModel;

namespace Wbf.Crm.Workflow
{
    public class CustonFlow : CodeActivity
	{

		[Input("BankName")]
		public InArgument<string> BankName { get; set; }

		[Output("SolutionName")]
		public OutArgument<string> SolutionName { get; set; }
		protected override void Execute(CodeActivityContext executionContext)
		{
			SolutionName.Set(executionContext, "你大爷");
			//Create the tracing service
			ITracingService tracingService = executionContext.GetExtension<ITracingService>();

			//Create the context
			IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
			IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
			IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

			string bankN = BankName.Get<string>(executionContext);
            if (string.IsNullOrWhiteSpace(bankN))
            {
				throw new Exception("参数不能为空");
            }
			Entity bank = new Entity("new_bank");
			bank["new_name"]= bankN;
			var id = service.Create(bank);

			tracingService.Trace($"new_bank ID：{id}");

		}
	}
}
