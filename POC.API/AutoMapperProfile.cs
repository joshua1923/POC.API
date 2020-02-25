using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using POC.API.Dtos.Character;
using POC.API.Models;

namespace POC.API
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Character, GetCharacterDto>();
			CreateMap<GetCharacterDto, Character>();
			CreateMap<AddCharacterDto, Character>();
			CreateMap<UpdateCharacterDto, Character>();
		}
	}
}
