using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace WPC.Plugins
{
	public class OnCreateEntityPostContextToServiceBus : PluginBase
	{
		public OnCreateEntityPostContextToServiceBus(string unsecure, string secure) : base(typeof(OnCreateEntityPostContextToServiceBus))
		{
			// TODO: Implement your custom configuration handling.
		}

		protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
		{
			if (localContext == null) throw new ArgumentNullException("localContext");

			var sbQuery = new QueryExpression("serviceendpoint") { TopCount = 1 };
			var result = localContext.OrganizationService.RetrieveMultiple(sbQuery).Entities.FirstOrDefault();
			localContext.NotificationService.Execute(result.ToEntityReference(), localContext.PluginExecutionContext);
		}
	}
}