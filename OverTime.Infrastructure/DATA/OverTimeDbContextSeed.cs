using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OverTime.Domain;

namespace OverTime.Infrastructure.DATA
{
	public class OverTimeDbContextSeed
	{
		public static async Task SeedAsync(OverTimeDbContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
		{
			int retryForAvailability = retry.Value;

			try
			{
				// INFO: Run this if using a real database. Used to automaticly migrate docker image of sql server db.
				orderContext.Database.Migrate();
				//orderContext.Database.EnsureCreated();

				if (!orderContext.Person.Any())
				{
					orderContext.Person.AddRange(GetPreconfiguredPerson());
				
					await orderContext.SaveChangesAsync();
				}
			}
			catch (Exception exception)
			{
				if (retryForAvailability < 50)
				{
					retryForAvailability++;
					var log = loggerFactory.CreateLogger<OverTimeDbContextSeed>();
					log.LogError(exception.Message);
					System.Threading.Thread.Sleep(2000);
					await SeedAsync(orderContext, loggerFactory, retryForAvailability);
				}
				throw;
			}
		}

		private static IEnumerable<PersonData> GetPreconfiguredPerson()
		{
			return new List<PersonData>()
			{
				new PersonData() { Name="Erfan" , BasicSalary=20000000,CreatedBy="Admin" ,CreatedDate=DateTime.Now,DateTime=DateTime.Now,LastModifiedBy="Admin",LastModifiedDate=DateTime.Now,PaidAllowance=1000000,Transportation=0},
				new PersonData() { Name="Mohammad" , BasicSalary=40000000,CreatedBy="Admin" ,CreatedDate=DateTime.Now,DateTime=DateTime.Now,LastModifiedBy="Admin",LastModifiedDate=DateTime.Now,PaidAllowance=2000000,Transportation=0},
			};
		}
		
	}
}
