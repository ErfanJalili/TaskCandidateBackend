using System.Globalization;

namespace OverTime.API.Extentions
{
	public static class PersianExtentions
	{

		public static DateTime ConvertToMiladi(string persianDate) 
		{
			
			int year = Convert.ToInt32(persianDate.Substring(0,4));
			int month = Convert.ToInt32(persianDate.Substring(4,2));
			int day = Convert.ToInt32(persianDate.Substring(6,2));
			Calendar persian = new PersianCalendar();
			DateTime date = new DateTime(year, month, day, persian);

			return date;

		}
	}
}
