using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;
using MusicAPI.Data;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApiDbContext _context; 

        public EmployeesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //IAction Result is a way of returning status codes along with data.
            //async needs 
            return Ok( await _context.Personal.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var person = await _context.Personal.FindAsync(id);
            if(person == null)
            {
                return NotFound("No Record Found"); 
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Personal personal)
        {
            if(personal != null)
            {
                await _context.Personal.AddAsync(personal);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return NotFound("Record is not complete"); 
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Personal personal)
        {
            var person = await _context.Personal.FindAsync(id);
            if(person == null)
            {
                return NotFound("No record found with that Id");
            }
            else
            {
                person.FirstName = personal.FirstName;
                person.LastName = personal.LastName;
                await _context.SaveChangesAsync();
                return Ok("Record Updated Successfully");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Personal.FindAsync(id);
            if(person == null)
            {
                return NotFound("That record was not found"); 
            }
            else
            {
                _context.Personal.Remove(person);
                await _context.SaveChangesAsync();
                return Ok("Record Deleted");
            }
            
        }
    }
}
