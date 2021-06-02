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
            //Task<IActionResult> is used when you need to return some data. 
            //Task would be used when you do not need to return data
            //Async allows the rest of the application to run while this method executes
            return Ok( await _context.Personal.ToListAsync());
        }

                //URL param = {id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var person = await _context.Personal.FindAsync(id);
            if(person == null)
            {
                //You can return custom error messages in the NotFound Object
                return NotFound("No Record Found"); 
            }
            else
            {
                return Ok(person);
            }
        }
            //api/Employees/Test/{id} - api attribute routing
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id; 
        }

        [HttpPost]                             //From Body tells the endpoint to accept data from the body of the request           
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
            //URL param
        [HttpPut("{id}")]                    //URL Param , From Body tells the endpoint to accept data from the body of the request
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
                //You can return a custom message in the OK object.
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
                                    //There is no RemoveAsync. So you cannot await this operation
                _context.Personal.Remove(person);
                await _context.SaveChangesAsync();
                return Ok("Record Deleted");
            }
            
        }
    }
}
