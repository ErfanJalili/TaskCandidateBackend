using AutoMapper;
using OverTime.API.Dtos;
using OverTime.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OverTime.API.Mapper
{
	public class PersonProfile : Profile
	{
        public PersonProfile()
        {
			CreateMap<PersonData, PersonDto>().ReverseMap();
			CreateMap<PersonData, PersonXmlDto>().ReverseMap();
			CreateMap<PersonData, GetPersonDto>().ReverseMap();
		}
    }
}
