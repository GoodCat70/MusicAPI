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
        public IEnumerable<Personal> Get()
        {
            return _context.Personal.ToList();
        }

        [HttpGet("{id}")]
        public Personal Get(string id)
        {
            return _context.Personal.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] Personal personal)
        {
            _context.Personal.Add(personal);
            _context.SaveChanges();
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Personal personal)
        {
            var person = _context.Personal.Find(id);
            person.FirstName = personal.FirstName;
            person.LastName = personal.LastName;
            _context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var person = _context.Personal.Find(id);
            _context.Personal.Remove(person);
            _context.SaveChanges();
        }
    }
}
