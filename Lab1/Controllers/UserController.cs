using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lab1.DTOs.UserDTOs;
using Lab1.Entities;
using Lab1.Entities.Parameters;
using Lab1.Interfaces.SqlServices;
using Lab1.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: Get all entities
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll([FromQuery] UserParameters userParameters)
        {
            return await _service.GetAll(userParameters);
        }

        // GET: Get single entity
        [Route("{id}")]
        [HttpGet]
        public async Task<User> GetById(int id)
        {
            return await _service.GetOneById(id);
        }

        // POST: Create entity
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserRequestDto dto)
        {
            var validator = new UserValidator();
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
        public async Task<ActionResult> Update(int id, [FromBody] UserRequestDto dto)
        {
            var validator = new UserValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            return Ok(await _service.Update(id, dto));
        }
    }
}