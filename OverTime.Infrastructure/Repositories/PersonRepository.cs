using OverTime.Application.Contracts.IRepositories;
using OverTime.Domain;
using OverTime.Infrastructure.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Infrastructure.Repositories
{
	public class PersonRepository : RepositoryBase<PersonData>, IPersonRepository
	{
		public PersonRepository(OverTimeDbContext dbContext) : base(dbContext)
		{

		}

		public Task<IReadOnlyList<PersonData>> GetAllWithDapperAsync()
		{

			throw new NotImplementedException();
		}
	}
}
