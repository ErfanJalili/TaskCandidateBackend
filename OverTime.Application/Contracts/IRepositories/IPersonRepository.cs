using OverTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Application.Contracts.IRepositories
{
	public interface IPersonRepository : IAsyncRepository<PersonData>
	{
		Task<IReadOnlyList<PersonData>> GetAllWithDapperAsync();
	}
}
