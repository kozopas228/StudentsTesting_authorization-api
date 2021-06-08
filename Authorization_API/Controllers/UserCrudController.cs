using Authorization_Models;
using Authorization_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCrudController : ControllerBase
    {
        private readonly IUserCrudService _userCrudService;

        public UserCrudController(IUserCrudService userCrudService)
        {
            _userCrudService = userCrudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new JsonResult(await _userCrudService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _userCrudService.CreateAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var result = await _userCrudService.UpdateAsync(user);
            if (result) return Ok();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userCrudService.DeleteAsync(id);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var users = await _userCrudService.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return new JsonResult(user);
        }


    }
}
