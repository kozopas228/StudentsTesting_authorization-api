using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Authorization_Models;
using Authorization_Services.Interfaces;

namespace Authorization_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _service;

        public UserManagementController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetIdByLogin")]
        public async Task<IActionResult> GetUserId(string login)
        {
            return Content((await _service.GetUserId(login)).ToString());
        }

        [HttpGet("GetAttempts")]
        public async Task<IActionResult> GetAttemptsById(Guid id)
        {
            return new JsonResult(await _service.GetAttemptsById(id));
        }

        [HttpGet("ChangeRole")]
        public async Task<IActionResult> ChangeRole(Guid userId, string role)
        {
            await _service.ChangeRole(userId, role);
            return Ok();
        }

        [HttpGet("AddUserAttempt")]
        public async Task<IActionResult> AddUserAttempt(Guid userId, Guid testId)
        {
            await _service.AddUserAttempt(userId, testId);
            return Ok();
        }

        [HttpGet("ChangeUserLogin")]
        public async Task<IActionResult> ChangeUserLogin(Guid userId, string newLogin)
        {
            var result = await _service.ChangeUserLogin(userId, newLogin);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(Guid userId, string newPassword)
        {
            var result = await _service.ChangeUserPassword(userId, newPassword);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("ChangeUserFirstName")]
        public async Task<IActionResult> ChangeUserFirstName(Guid userId, string newFirstName)
        {
            var result = await _service.ChangeUserFirstName(userId, newFirstName);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("ChangeUserLastName")]
        public async Task<IActionResult> ChangeUserLastName(Guid userId, string newLastName)
        {
            var result = await _service.ChangeUserLastName(userId, newLastName);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPost("SaveAttemptToUser")]
        public async Task<IActionResult> SaveAttempt(Guid userId, TestAttempt attempt)
        {
            var result = await _service.SaveAttempt(userId, attempt);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
