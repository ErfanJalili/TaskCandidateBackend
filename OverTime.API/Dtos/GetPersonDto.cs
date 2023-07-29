namespace OverTime.API.Dtos
{
	public class GetPersonDto
	{
		public int Id { get; protected set; }
		public string CreatedBy { get; set; } 
		public DateTime CreatedDate { get; set; } 
		public string LastModifiedBy { get; set; } 
		public DateTime? LastModifiedDate { get; set; }
		public string Name { get; set; } 
		public decimal BasicSalary { get; set; } 
		public DateTime DateTime { get; set; } 
		public decimal PaidAllowance { get; set; } 
		public decimal Transportation { get; set; } 
		public decimal OverTimeCalculator { get; set; }
		public decimal Salery { get; set; }
	}
}
