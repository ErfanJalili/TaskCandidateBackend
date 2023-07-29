using AutoMapper;
using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OverTime.API.Dtos;
using OverTime.API.Extentions;
using OverTime.API.Extentions.CSV;
using OverTime.Application.Contracts.IRepositories;
using OverTime.Application.Contracts.MicroORM;
using OverTime.Domain;
using OvetimePolicies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OverTime.API.Controllers
{
	
	[ApiController]
	public class OverTimeController : ControllerBase
	{

		private readonly IPersonRepository _personRepository;
		private readonly IMapper _mapper;
		private readonly ICSVService _csvService;
		private readonly IDapper _dapper;

		public OverTimeController(IPersonRepository personRepository, IMapper mapper, ICSVService csvService, IDapper dapper)
		{
			_personRepository = personRepository;
			_mapper = mapper;
			_csvService = csvService;
			_dapper = dapper;
		}


		#region CalculateWithJson

		[Route("/JSON/[controller]/Create")]
		[Consumes("application/json")]
		[ProducesResponseType(typeof(ServiceResponse<PersonDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<PersonDto>>> CalculateWithJson([FromBody] PersonDto data, [FromQuery] string OverTimeCalculatorType)
		{
			try
			{
				//change dto to entity

				var dtoToPerson = _mapper.Map<PersonDto, PersonData>(data);

				//calcualate type

				switch (OverTimeCalculatorType)
				{
					case "CalculateA":
						OvertimeMethods.CalculateA(dtoToPerson);
						break;
					case "CalculateB":
						OvertimeMethods.CalculateB(dtoToPerson);
						break;
					case "CalculateC":
						OvertimeMethods.CalculateC(dtoToPerson);
						break;
					default:
						break;
				}

				//add to database

				var res = await _personRepository.AddAsync(dtoToPerson);

				//entity ti dto
				var personToDto = _mapper.Map<PersonData, PersonDto>(res);

				// dto to service response
				return Ok(new ServiceResponse<PersonDto> { Data = personToDto, Message = "Created" });
			}
			catch (Exception ex)
			{

				return BadRequest(new ServiceResponse<PersonDto> { Data = null, Message = ex.Message });
			}
			
		}

		#endregion


		#region CalculateWithXml

		[Route("/XML/[controller]/Create")]
		[Consumes("application/xml")]
		[ProducesResponseType(typeof(ServiceResponse<PersonDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<PersonDto>>> CalculateWithXml([FromBody] PersonXmlDto data, [FromQuery] string OverTimeCalculatorType)
		{
			try
			{
				// xml to entity
				var dtoToPerson = _mapper.Map<PersonXmlDto, PersonData>(data);

				// calculate type
				switch (OverTimeCalculatorType)
				{
					case "CalculateA":
						OvertimeMethods.CalculateA(dtoToPerson);
						break;
					case "CalculateB":
						OvertimeMethods.CalculateB(dtoToPerson);
						break;
					case "CalculateC":
						OvertimeMethods.CalculateC(dtoToPerson);
						break;
					default:
						break;
				}

				// add to database
				var res = await _personRepository.AddAsync(dtoToPerson);

				//entity to dto
				var personToDto = _mapper.Map<PersonData, PersonDto>(res);

				// dto to service response
				return Ok(new ServiceResponse<PersonDto> { Data = personToDto, Message = "Created" });
			}
			catch (Exception ex)
			{

				return BadRequest(new ServiceResponse<PersonDto> { Data = null, Message = ex.Message });
			}
			
		}

		#endregion


		#region CalculateWithCsv

		[Route("/CSV/[controller]/Create")]
		[ProducesResponseType(typeof(ServiceResponse<PersonDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<PersonDto>>> CalculateWithCsv([FromForm] IFormFileCollection file, [FromQuery] string OverTimeCalculatorType)
		{
			try
			{
				// check file Exist
				if (file.Count > 0)
				{
					//read file as dto
					var employees = _csvService.ReadCSV<PersonDto>(file[0].OpenReadStream()).SingleOrDefault();

					if (employees != null)
					{
						//dto to entity
						var dataList = _mapper.Map<PersonDto, PersonData>(employees);


						//calculate type
						switch (OverTimeCalculatorType)
						{
							case "CalculateA":
								OvertimeMethods.CalculateA(dataList);
								break;
							case "CalculateB":
								OvertimeMethods.CalculateB(dataList);
								break;
							case "CalculateC":
								OvertimeMethods.CalculateC(dataList);
								break;
							default:
								break;
						}
						// add to database
						var res = await _personRepository.AddAsync(dataList);

						//entity to dto
						var personToDto = _mapper.Map<PersonData, PersonDto>(res);

						//dto to service response
						return Ok(new ServiceResponse<PersonDto> { Data = personToDto, Message = "Created" });
					}

				}
				return BadRequest(new ServiceResponse<PersonDto> { Data = null, Message = "BadRequest" });
			}
			catch (Exception ex)
			{
				return NotFound(new ServiceResponse<PersonDto> { Data = null, Message = ex.ToString() });
			}

			
		}

		#endregion


		#region CalculateWithCustom

		[Route("/Custom/[controller]/Create")]
		[ProducesResponseType(typeof(ServiceResponse<PersonDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<PersonDto>>> CalculateWithCustom([FromForm] IFormFileCollection file , [FromQuery] string OverTimeCalculator)
		{
			try
			{
				//check file exist
				if (file.Count > 0)
				{
					//read file as string
					var res = await StringExtentions.ReadAsStringAsync(file[0]);
					if (!string.IsNullOrEmpty(res))
					{
						//split lineby line
						string[] splittedString = res.Split("\n");

						//split data with char : /
						string[] data = splittedString[1].Split('/');

						//change persian date to miladi
						var date = PersianExtentions.ConvertToMiladi(data[5].Remove(data[5].Length - 1, 1));

						//create dto
						PersonDto personDto = new PersonDto()
						{
							Name = data[0] + " " + data[1],
							BasicSalary = Convert.ToDecimal(data[2]),
							DateTime = date,
							PaidAllowance = Convert.ToDecimal(data[3]),
							Transportation = Convert.ToDecimal(data[4])

						};

						//dto to entity
						var personDtoToPerson = _mapper.Map<PersonDto, PersonData>(personDto);

						//add to database
						var result = await _personRepository.AddAsync(personDtoToPerson);

						//entity to dto
						var personToDto = _mapper.Map<PersonData, PersonDto>(result);
						//dto to service response
						return Ok(new ServiceResponse<PersonDto> { Data = personToDto, Message = "Created" });
					}
				}
				else
				{
					return NotFound(new ServiceResponse<PersonDto> { Data = null, Message = "NoFile" });
				}
				return BadRequest(new ServiceResponse<PersonDto> { Data = null, Message = "BadRequest" });
			}
			catch (Exception ex)
			{

				return Ok(new ServiceResponse<PersonDto> { Data = null, Message = ex.Message });
			}
			

			
		}
		#endregion


		#region GetData
		[Route("/[controller]/Get")]
		[ProducesResponseType(typeof(ServiceResponse<IReadOnlyList<GetPersonDto>>), (int)HttpStatusCode.OK)]
		[HttpGet]
		public async Task<ActionResult<ServiceResponse<IReadOnlyList<GetPersonDto>>>> GetAll()
		{
		 	var person= await _personRepository.GetAllAsync();

			var dtoToPerson = _mapper.Map<IReadOnlyList<PersonData>,IReadOnlyList<GetPersonDto>>(person);

			return Ok(new ServiceResponse<IReadOnlyList<GetPersonDto>> {Data=dtoToPerson,Message="Get all" });
		}
		#endregion


		#region GetDataWithDapper

		[Route("/[controller]/DapperGet")]
		[ProducesResponseType(typeof(ServiceResponse<IReadOnlyList<GetPersonDto>>), (int)HttpStatusCode.OK)]
		[HttpGet]
		public  ActionResult<ServiceResponse<IReadOnlyList<GetPersonDto>>> GetAllWithDapper()
		{
			//get sp
			var res=  _dapper.GetAll<PersonData>("GetAllPersonSP", null);

			//change to dto
			var dtoToPerson = _mapper.Map<IReadOnlyList<PersonData>, IReadOnlyList<GetPersonDto>>(res);

			//change to service response
			return Ok(new ServiceResponse<IReadOnlyList<GetPersonDto>> { Data = dtoToPerson, Message = "Get all" });
		}
		#endregion


		#region GetPersonRageByDateTime

		[Route("/[controller]/GetPersonRageByDateTime")]
		[ProducesResponseType(typeof(ServiceResponse<IReadOnlyList<GetPersonDto>>), (int)HttpStatusCode.OK)]
		[HttpGet]
		public ActionResult<ServiceResponse<IReadOnlyList<GetPersonDto>>> GetPersonRageByDateTime(DateTime from,DateTime to)
		{
			//define sp params

			var dbPara = new DynamicParameters();
			dbPara.Add("From", from, DbType.DateTime);
			dbPara.Add("To", to, DbType.DateTime);

			//get sp
			var res = _dapper.GetAll<PersonData>("GetPersonRageByDateTime", dbPara);

			//change to dto
			var dtoToPerson = _mapper.Map<IReadOnlyList<PersonData>, IReadOnlyList<GetPersonDto>>(res);

			//change to service response
			return Ok(new ServiceResponse<IReadOnlyList<GetPersonDto>> { Data = dtoToPerson, Message = "Get all" });
		}
		#endregion


		#region DeletePersonById
		[Route("/[controller]/DeletePerson")]
		[ProducesResponseType(typeof(ServiceResponse<GetPersonDto>), (int)HttpStatusCode.OK)]
		[HttpDelete]
		public async Task<ActionResult<ServiceResponse<GetPersonDto>>> DeletePerson(int id)
		{
			try
			{
				//find person
				var deletePerson = await _personRepository.GetByIdAsync(id);
				if (deletePerson != null)
				{
					//delete from database
					await _personRepository.DeleteAsync(deletePerson);

					//change to dto
					var deletedPersonDto = _mapper.Map<GetPersonDto>(deletePerson);
					
					//change to service respone
					return Ok(new ServiceResponse<GetPersonDto> { Data = deletedPersonDto, Message = "Deleted" });
				}

				return NotFound(new ServiceResponse<GetPersonDto> { Data = null, Message = "NotFound" });
			}
			catch (Exception ex)
			{
				return BadRequest(new ServiceResponse<GetPersonDto> { Data = null, Message = ex.Message });
			}

		}
		#endregion


		#region UpdatePersonById
		[Route("/[controller]/UpdatePerson")]
		[ProducesResponseType(typeof(ServiceResponse<GetPersonDto>), (int)HttpStatusCode.OK)]
		[HttpPut]
		public async Task<ActionResult<ServiceResponse<GetPersonDto>>> UpdatePerson(int id , UpdatePersonDto person)
		{
			try
			{
				//find person
				var updatePerson = await _personRepository.GetByIdAsync(id);
				if (updatePerson != null)
				{
					//edit validated props changes
					updatePerson.LastModifiedDate = DateTime.Now;
					updatePerson.Name=person.Name;
					
					//update database
					await _personRepository.UpdateAsync(updatePerson);

					//change to dto
					var deletedPersonDto = _mapper.Map<GetPersonDto>(updatePerson);

					//change to service respone
					return Ok(new ServiceResponse<GetPersonDto> { Data = deletedPersonDto, Message = "Updated" });
				}

				return NotFound(new ServiceResponse<GetPersonDto> { Data = null, Message = "NotFound" });
			}
			catch (Exception ex)
			{
				return BadRequest(new ServiceResponse<GetPersonDto> { Data = null, Message = ex.Message });
			}

		}
		#endregion
	}
}
