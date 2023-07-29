using Microsoft.EntityFrameworkCore;
using OverTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Infrastructure.DATA
{
	public class OverTimeDbContext : DbContext
	{
        public DbSet<PersonData> Person { get; set; }
        public OverTimeDbContext(DbContextOptions<OverTimeDbContext> options) : base(options)
		{

		}
	}
}
