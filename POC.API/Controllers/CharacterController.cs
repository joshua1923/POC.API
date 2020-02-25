﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.API.Dtos.Character;
using POC.API.Models;
using POC.API.Services.CharacterService;

namespace POC.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CharacterController : ControllerBase
	{
		private readonly ICharacterService _characterService;

		public CharacterController(ICharacterService characterService)
		{
			_characterService = characterService;
		}

		[Route("GetAll")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _characterService.GetAllCharacters());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingle(int id)
		{
			return Ok(await _characterService.GetCharacterById(id));
		}

		[HttpPost]
		public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
		{
			return Ok(await _characterService.AddCharacter(newCharacter));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
		{
			ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updateCharacter);

			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);

			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
