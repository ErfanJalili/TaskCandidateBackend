using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;

namespace OverTime.API.Extentions.CSV
{
	public class CSVService : ICSVService
	{
		public IEnumerable<T> ReadCSV<T>(Stream file)
		{
			var reader = new StreamReader(file);

			var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			var records = csv.GetRecords<T>();
			return records;
		}
	}
}
