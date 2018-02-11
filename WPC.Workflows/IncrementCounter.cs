using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace WPC.Workflows
{
	public class IncrementCounter : CodeActivity
	{
		[Input("Record")]
		[ReferenceTarget("wpc_counter")]
		InArgument<EntityReference> WPCEntity { get; set; }

		protected override void Execute(CodeActivityContext context)
		{
			IWorkflowContext workflowContext = context.GetExtension<IWorkflowContext>();

			if (context == null)
				throw new InvalidPluginExecutionException("Failed to retrieve workflow context.");

			var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
			var service = serviceFactory.CreateOrganizationService(workflowContext.UserId);
		}
	}
}
