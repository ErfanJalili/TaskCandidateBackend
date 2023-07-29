using Microsoft.AspNetCore.Http;
using Moq;
using OverTime.API.Dtos;
using OverTime.Domain;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Test
{
	public  class OverTimeMockData
	{
		public static  IReadOnlyList<PersonData> GetPeople() 
		{
			return new List<PersonData> {

			new PersonData
			{
				
				Name="ali"
			},
			new PersonData
			{

				Name="hassan"
			},
			new PersonData
			{

				Name="reza"
			},
			};
		
		}

		public	static PersonData GetPerson()
		{
			return new PersonData { Name="Jafar" };
		}
		public static PersonDto GetPersonDto()
		{
			return new PersonDto { Name = "Jafar" ,DateTime=DateTime.Now,BasicSalary=1000 };
		}

		public static PersonXmlDto GetPersonXmlDto()
		{
			return new PersonXmlDto { Name = "Jafar", DateTime = DateTime.Now, BasicSalary = 1000 };
		}

		public static UpdatePersonDto GetUpdatePersonDto()
		{
			return new UpdatePersonDto { Name="Asghar"};
		}

		
		
	}
}
