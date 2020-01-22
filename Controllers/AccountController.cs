using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Model.DTO;

namespace ToDoListAPI.Controllers
{
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(AccountCredentialsDTO credentials)
        {
            var user = new IdentityUser
            {
                Email = credentials.Email,
                UserName = credentials.Email
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponseDTO>> Login(AccountCredentialsDTO credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.Email);
            if (user == null)
                return Unauthorized();

            if (!await userManager.CheckPasswordAsync(user, credentials.Password))
                return Unauthorized();

            var authUser = new AuthResponseDTO
            {
                UserId = user.Id,
                UserName = user.UserName
            };

            return authUser;
            
        }
    }
}