using Microsoft.Xrm.Sdk;
using System;
using System.Globalization;
using System.ServiceModel;

namespace WPC.Plugins
{
	public abstract class PluginBase : IPlugin
	{
		protected class LocalPluginContext
		{
			internal IServiceProvider ServiceProvider { get; private set; }
			internal IOrganizationService OrganizationService { get; private set; }
			internal IPluginExecutionContext PluginExecutionContext { get; private set; }
			internal IServiceEndpointNotificationService NotificationService { get; private set; }
			internal ITracingService TracingService { get; private set; }

			private LocalPluginContext() { }

			internal LocalPluginContext(IServiceProvider serviceProvider)
			{
				if (serviceProvider == null)
				{
					throw new ArgumentNullException("serviceProvider");
				}

				PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
				TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
				NotificationService = (IServiceEndpointNotificationService)serviceProvider.GetService(typeof(IServiceEndpointNotificationService));
				IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
				OrganizationService = factory.CreateOrganizationService(PluginExecutionContext.UserId);
			}

			/// <summary>
			/// Writes a trace message to the CRM trace log.
			/// </summary>
			/// <param name="message">Message name to trace.</param>
			internal void Trace(string message)
			{
				if (string.IsNullOrWhiteSpace(message) || TracingService == null)
				{
					return;
				}

				if (PluginExecutionContext == null)
				{
					TracingService.Trace(message);
				}
				else
				{
					TracingService.Trace(
						"{0}, Correlation Id: {1}, Initiating User: {2}",
						message,
						PluginExecutionContext.CorrelationId,
						PluginExecutionContext.InitiatingUserId);
				}
			}
		}

		protected string ChildClassName { get; private set; }

		internal PluginBase(Type childClassName)
		{
			ChildClassName = childClassName.ToString();
		}

		public void Execute(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}

			LocalPluginContext localcontext = new LocalPluginContext(serviceProvider);
			localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", this.ChildClassName));

			try
			{
				ExecuteCrmPlugin(localcontext);
				return;
			}
			catch (FaultException<OrganizationServiceFault> e)
			{
				localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", e.ToString()));
				throw;
			}
			finally
			{
				localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", this.ChildClassName));
			}
		}

		protected virtual void ExecuteCrmPlugin(LocalPluginContext localcontext)
		{
			// Do nothing. 
		}
	}
}