using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPC.VirtualEntity.Models;

namespace WPC.VirtualEntity
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOData();
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var builder = new ODataConventionModelBuilder();
			builder.EntitySet<VirtualEntityModel>("VirtualEntity");
			builder.EntityType<VirtualEntityModel>();

			app.UseMvc(routebuilder =>
			{
				routebuilder.MapODataRoute("OData", builder.GetEdmModel());
			});
		}
	}
}