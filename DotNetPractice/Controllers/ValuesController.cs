using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetPractice.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ValuesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var Values = await _dataContext.Values.ToListAsync();

            return Ok(Values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [EnableCors("AllowSpecificOrigins")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _dataContext.Values.FirstOrDefaultAsync(x=> x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
