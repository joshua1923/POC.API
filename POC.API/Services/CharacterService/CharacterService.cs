using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using POC.API.Data;
using POC.API.Dtos.Character;
using POC.API.Models;

namespace POC.API.Services.CharacterService
{
	public class CharacterService : ICharacterService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _dataContext;

		public CharacterService(IMapper mapper, DataContext dataContext)
		{
			_mapper = mapper;
			_dataContext = dataContext;
		}

		//private static List<Character> characters = new List<Character> {
		//	new Character(),
		//	new Character { Id = 1, Name = "Sam"}
		//};

		public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
		{
			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

			await using (var db = _dataContext)
			{
				serviceResponse.Data = db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
			}
			//serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
			
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
		{
			ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

			await using (var db = _dataContext)
			{
				serviceResponse.Data = _mapper.Map<GetCharacterDto>(db.Characters.FirstOrDefault(c => c.Id == id));
			}
			//serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
		{
			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
			Character character = _mapper.Map<Character>(newCharacter);
			
			await using (var db = _dataContext)
			{
				db.Characters.Add(character);
				db.SaveChanges();

				serviceResponse.Data = db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
			}

			//serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
			
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
		{
			ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

			try
			{
				await using (var db = _dataContext)
				{
					Character character = db.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
					character.Name = updatedCharacter.Name;
					character.Class = updatedCharacter.Class;
					character.Defense = updatedCharacter.Defense;
					character.HitPoints = updatedCharacter.HitPoints;
					character.Intelligence = updatedCharacter.Intelligence;
					character.Strength = updatedCharacter.Strength;

					serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
				}
				//Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;

		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
		{
			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

			try
			{
				await using (var db = _dataContext)
				{
					Character character = db.Characters.First(c => c.Id == id);
					db.Characters.Remove(character);

					serviceResponse.Data = db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
				}
				//Character character = characters.First(c => c.Id == id);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}
	}
}
