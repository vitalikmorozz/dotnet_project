using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.DTOs.StationDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlServices;
using Lab1.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/v1/stations")]
    public class StationController : ControllerBase
    {
        private readonly IStationService _service;

        public StationController(IStationService service)
        {
            _service = service;
        }

        // GET: Get all entities
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Station>> GetAll([FromQuery] StationParameters stationParameters)
        {
            return await _service.GetAll(stationParameters);
        }

        // GET: Get single entity
        [Route("{id}")]
        [HttpGet]
        public async Task<Station> GetById(int id)
        {
            return await _service.GetOneById(id);
        }

        // POST: Create entity
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StationRequestDto dto)
        {
            var validator = new StationValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            return Ok(await _service.Create(dto));
        }

        // DELETE: Delete single entity by id
        [Route("{id}")]
        [HttpDelete]
        public async Task<int> DeleteById(int id)
        {
            return await _service.DeleteById(id);
        }

        // PUT: Update single entity
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] StationRequestDto dto)
        {
            var validator = new StationValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            return Ok(await _service.Update(id, dto));
        }
    }
}