using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Authorization_Models;
using Authorization_Services.Interfaces;

namespace Authorization_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAttemptCrudController : ControllerBase
    {
        private readonly ITestAttemptCrudService _testAttemptCrudService;

        public TestAttemptCrudController(ITestAttemptCrudService testAttemptCrudService)
        {
            _testAttemptCrudService = testAttemptCrudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new JsonResult(await _testAttemptCrudService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestAttempt testAttempt)
        {
            await _testAttemptCrudService.CreateAsync(testAttempt);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TestAttempt testAttempt)
        {
            var result = await _testAttemptCrudService.UpdateAsync(testAttempt);
            if (result) return Ok();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testAttemptCrudService.DeleteAsync(id);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var testAttempts = await _testAttemptCrudService.GetAllAsync();
            var testAttempt = testAttempts.FirstOrDefault(x => x.Id == id);

            if (testAttempt == null)
            {
                return NotFound();
            }

            return new JsonResult(testAttempt);
        }
    }
}
