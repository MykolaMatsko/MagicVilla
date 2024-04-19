﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDTO>> GetVilas()
		{
			return Ok(VillaStore.villaList);
		}

		[HttpGet("id:int")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDTO> GetVila(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(villa);
		}

		[HttpPost]
		public ActionResult<VillaDTO> CreateVilla ([FromBody] VillaDTO villaDto)
		{
			if (villaDto ==  null)
			{
				return BadRequest(villaDto);
			}
			if (villaDto.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
			VillaStore.villaList.Add(villaDto);

			return Ok(villaDto);
		}
	}
}
