using OverTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvetimePolicies
{
	public static class OvertimeMethods
	{
		public static void CalculateA(PersonData ps) 
		{
			var overTime = OverTimeCalculator(ps.BasicSalary, ps.PaidAllowance);
			ps.OverTimeCalculator = overTime;
			ps.Salery = ps.BasicSalary + ps.PaidAllowance + overTime - 100;
		}
		public static void CalculateB(PersonData ps)
		{
			var overTime = OverTimeCalculator(ps.BasicSalary, ps.PaidAllowance);
			ps.OverTimeCalculator = overTime;
			ps.Salery = ps.BasicSalary + ps.PaidAllowance + overTime - 200;
		}
		public static void CalculateC(PersonData ps)
		{
			var overTime = OverTimeCalculator(ps.BasicSalary, ps.PaidAllowance);
			ps.OverTimeCalculator = overTime;
			ps.Salery = ps.BasicSalary + ps.PaidAllowance + overTime - 300;
		}
		private static decimal OverTimeCalculator(decimal basicSalary ,decimal allowance)
		{
			return basicSalary + allowance;
		}
	}
}
