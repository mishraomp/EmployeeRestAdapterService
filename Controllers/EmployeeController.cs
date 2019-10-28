using EmployeeRestAdapterService.Entity;
using EmployeeRestAdapterService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAdapterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeEntity>>> Get() { return await _employeeService.Get(); }

        [HttpGet("{id:length(24)}", Name = "GetEmployeeEntity")]
        public ActionResult<EmployeeEntity> Get(string id, [FromQuery(Name = "q1")] string q1)
        {
            
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<EmployeeEntity> Create(EmployeeEntity employee)
        {
            _employeeService.Create(employee);

            return CreatedAtRoute("GetEmployeeEntity", new { id = employee.Id.ToString() }, employee);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, EmployeeEntity employeeIn)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.Update(id, employeeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.Remove(employee.Id);

            return NoContent();
        }

    }
}
