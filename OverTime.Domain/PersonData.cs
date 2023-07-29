using OverTime.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Domain
{
	public class PersonData : EntityBase
	{
		public string Name { get; set; } = "Erfan";
		public decimal BasicSalary { get; set; } = 1000;
		public DateTime DateTime { get; set; } = DateTime.Now;
		public decimal PaidAllowance { get; set; } = 100;
		public decimal Transportation { get; set; } = 200;
		public decimal OverTimeCalculator { get; set; }
		public decimal Salery { get; set; }
	}
}
