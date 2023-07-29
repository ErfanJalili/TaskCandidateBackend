using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OverTime.Application.Contracts.MicroORM
{
	public interface IDapper: IDisposable
	{
		DbConnection GetDbconnection();
		T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
	}
}
