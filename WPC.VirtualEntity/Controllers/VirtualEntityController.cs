using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using WPC.VirtualEntity.Common;
using WPC.VirtualEntity.Models;

namespace WPC.VirtualEntity.Controllers
{
	[Route("OData/[controller]")]
	public class VirtualEntityController : Controller
	{
		private readonly IEnumerable<VirtualEntityModel> m_entities;

		public VirtualEntityController()
		{
			m_entities = VirtualEntitySeeder.Seed();
		}

		[EnableQuery]
		public IActionResult Get() => Ok(m_entities.AsQueryable());

		[EnableQuery]
		[HttpGet("{id}")]
		public IActionResult Get(Guid id) => Ok(m_entities.FirstOrDefault(v => v.Id == id));
	}
}