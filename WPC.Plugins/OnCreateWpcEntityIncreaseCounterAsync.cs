using Microsoft.Xrm.Sdk;
using System;

namespace WPC.Plugins
{
	public class OnCreateWpcEntityIncreaseCounterAsync : PluginBase
	{
		public OnCreateWpcEntityIncreaseCounterAsync(string unsecure, string secure) : base(typeof(OnCreateWpcEntityIncreaseCounterAsync))
		{

		}

		protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
		{
			if (localContext == null)
			{
				throw new ArgumentNullException("localContext");
			}

			var target = localContext.PluginExecutionContext.InputParameters["Target"] as Entity;

			if (target != null)
			{
				var currentCounter = target.GetAttributeValue<int>("wpc_count");
				target["wpc_count"] = (currentCounter += 1);
				localContext.OrganizationService.Update(target);
			}
		}
	}
}
