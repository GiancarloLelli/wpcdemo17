using System;

namespace WPC.VirtualEntity.Models
{
	public class VirtualEntityModel : EntityBase
	{
		public VirtualEntityModel(string title, string subtitle, string description, string externalPk)
		{
			Id = GenerateDeterministicGuid(externalPk);
			CreatedOn = DateTime.Now;
			Title = title;
			Subtitle = subtitle;
			Description = description;
		}

		public DateTime CreatedOn { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
	}
}
