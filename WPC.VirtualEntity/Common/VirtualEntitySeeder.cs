using System.Collections.Generic;
using WPC.VirtualEntity.Models;

namespace WPC.VirtualEntity.Common
{
	public class VirtualEntitySeeder
	{
		public static IEnumerable<VirtualEntityModel> Seed()
		{
			var seeded = new List<VirtualEntityModel>();

			seeded.Add(new VirtualEntityModel("Virtual Entity 1", "Virtual Entity Subtitle 1", "Virtual Entity Description 1", "18593658701"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 2", "Virtual Entity Subtitle 2", "Virtual Entity Description 2", "19462856062"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 3", "Virtual Entity Subtitle 3", "Virtual Entity Description 3", "10385638695"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 4", "Virtual Entity Subtitle 4", "Virtual Entity Description 4", "10386926597"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 5", "Virtual Entity Subtitle 5", "Virtual Entity Description 5", "10468923451"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 6", "Virtual Entity Subtitle 6", "Virtual Entity Description 6", "10103892482"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 7", "Virtual Entity Subtitle 7", "Virtual Entity Description 7", "11100044489"));
			seeded.Add(new VirtualEntityModel("Virtual Entity 8", "Virtual Entity Subtitle 8", "Virtual Entity Description 8", "10294867553"));

			return seeded;
		}
	}
}
