namespace OverTime.API.Extentions.CSV
{
	public interface ICSVService
	{
		public IEnumerable<T> ReadCSV<T>(Stream file);
	}
}
