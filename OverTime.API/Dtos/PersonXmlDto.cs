using System.Xml.Serialization;

namespace OverTime.API.Dtos
{
	[XmlRoot(ElementName = "person")]
	public sealed class PersonXmlDto
	{
		[XmlElement(ElementName = "name")]
		public string Name { get; set; } 

		[XmlElement(ElementName = "basicSalary")]
		public decimal BasicSalary { get; set; }

		[XmlElement(ElementName = "dateTime")]
		public DateTime DateTime { get; set; }

		[XmlElement(ElementName = "paidAllowance")]
		public decimal PaidAllowance { get; set; }

		[XmlElement(ElementName = "transportation")]
		public decimal Transportation { get; set; }
	}
	//[XmlRoot(ElementName = "Person")]
	//public sealed class PersonXmlDto
	//{
	//	[XmlArray(ElementName = "Data")]
	//	public List<string> Data { get; set; } 

	//}
}
