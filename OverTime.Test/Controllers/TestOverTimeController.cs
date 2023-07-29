using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OverTime.API.Controllers;
using OverTime.API.Dtos;
using OverTime.API.Extentions;
using OverTime.API.Extentions.CSV;
using OverTime.Application.Contracts.IRepositories;
using OverTime.Application.Contracts.MicroORM;
using OverTime.Domain;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverTime.Test.Controllers
{
	public class TestOverTimeController
	{

		[Fact]
		public async Task GetAllPerson_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange


			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv =  A.Fake<ICSVService>();
			var dapper =  A.Fake<IDapper>();

			personService.Setup(_ => _.GetAllAsync()).ReturnsAsync(OverTimeMockData.GetPeople());

			var testOvertimeController = new OverTimeController(personService.Object,mapper,csv, dapper);


			//Act

			var result = await testOvertimeController.GetAll();


			//Assert

			Assert.NotNull(result);
			
			
			
		}

		[Fact]
		public async Task GetAllPersonDapper_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange
			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv = A.Fake<ICSVService>();
			var dapper = A.Fake<IDapper>();

			personService.Setup(_ => _.GetAllWithDapperAsync()).ReturnsAsync(OverTimeMockData.GetPeople());

			var testOvertimeController = new OverTimeController(personService.Object, mapper, csv, dapper);


			//Act
			var result =  testOvertimeController.GetAllWithDapper();


			//Assert
			Assert.NotNull(result);

		}

		[Fact]
		public async Task CreateJson_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange
			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv = A.Fake<ICSVService>();
			var dapper = A.Fake<IDapper>();

			personService.Setup(_ => _.AddAsync(OverTimeMockData.GetPerson())).ReturnsAsync(OverTimeMockData.GetPerson());

			var testOvertimeController = new OverTimeController(personService.Object, mapper, csv, dapper);


			//Act
			var result = await testOvertimeController.CalculateWithJson(OverTimeMockData.GetPersonDto(),"CalculateA");


			//Assert
			Assert.NotNull(result);

		}

		[Fact]
		public async Task CreateXml_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange
			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv = A.Fake<ICSVService>();
			var dapper = A.Fake<IDapper>();

			personService.Setup(_ => _.AddAsync(OverTimeMockData.GetPerson())).ReturnsAsync(OverTimeMockData.GetPerson());

			var testOvertimeController = new OverTimeController(personService.Object, mapper, csv, dapper);


			//Act
			var result = await testOvertimeController.CalculateWithXml(OverTimeMockData.GetPersonXmlDto(), "CalculateA");


			//Assert
			Assert.NotNull(result);

		}
		

		[Fact]
		public async Task Delete_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange
			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv = A.Fake<ICSVService>();
			var dapper = A.Fake<IDapper>();

			personService.Setup(_ => _.DeleteAsync(OverTimeMockData.GetPerson()));

			var testOvertimeController = new OverTimeController(personService.Object, mapper, csv, dapper);


			//Act
			var result = await testOvertimeController.DeletePerson(1);


			//Assert
			Assert.NotNull(result);

		}

		[Fact]
		public async Task Update_ReturnsIReadonlylistOfPersonDto()
		{
			//Arrange
			var personService = new Mock<IPersonRepository>();
			var mapper = A.Fake<IMapper>();
			var csv = A.Fake<ICSVService>();
			var dapper = A.Fake<IDapper>();

			personService.Setup(_ => _.UpdateAsync(OverTimeMockData.GetPerson()));

			var testOvertimeController = new OverTimeController(personService.Object, mapper, csv, dapper);


			//Act
			var result = await testOvertimeController.UpdatePerson(1,OverTimeMockData.GetUpdatePersonDto());


			//Assert
			Assert.NotNull(result);

		}
	}
}
