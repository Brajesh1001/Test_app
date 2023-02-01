using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> Employees = new List<Employee>
        {
            //my test data
            new Employee
                {

                    Id = 1,
                    Name = "Brajesh Kumar Sethi",
                    FastName = "Brajesh",
                    LastName = "Sethi",
                    City = "New Delhi",
                    State = "Delhi",
                    Country = "India" 

                }
        };
        private readonly DataContext _context;

        public EmployeeController (DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {

            return Ok(await _context.Employees.ToArrayAsync());
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Post(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
            //Employees.Add(emp);
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> Update(Employee request)
        {
            var dbemply = await _context.Employees.FindAsync(request.Id);
            if (dbemply == null)
                return BadRequest("Employee Not Found");

            dbemply.Name = request.Name;
            dbemply.FastName = request.FastName;
            dbemply.LastName = request.LastName;
            dbemply.City = request.City;
            dbemply.State = request.State;  
            dbemply.Country= request.Country;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var dbemp = await _context.Employees.FindAsync(id);
        if (dbemp == null)
        {
            return BadRequest("Employee Not Found");
        }
        _context.Employees.Remove(dbemp);
        await _context.SaveChangesAsync();
        return Ok(await _context.Employees.ToListAsync());
        }
    }
}