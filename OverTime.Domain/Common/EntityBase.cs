using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Domain.Common
{
	public class EntityBase
	{
		public int Id { get; protected set; }
		public string CreatedBy { get; set; } = "Admin";
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string LastModifiedBy { get; set; } = "Admin";
		public DateTime? LastModifiedDate { get; set; }	
	}
}
