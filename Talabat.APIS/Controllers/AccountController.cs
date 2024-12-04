using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Services;
using Talabat.core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager,IToken _tokenService) : ApiBaseController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return BadRequest(new ApiResponse(400, "Email is already registered."));
            }

            var appUser = new AppUser
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, string.Join(", ", result.Errors.Select(e => e.Description))));
            }


            var userDTO = new UserDTO
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = await _tokenService.CreateTokenAsync(appUser, _userManager)      // Generate JWT Token

            };

            return Ok(userDTO);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            // Find user by email
            var findUser = await _userManager.FindByEmailAsync(model.Email);
            if (findUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Check password
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(findUser, model.Password);
            if (!isPasswordCorrect)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate token
            var userDTO = new UserDTO
            {
                DisplayName = findUser.DisplayName,
                Email = findUser.Email,
                Token = await _tokenService.CreateTokenAsync(findUser, _userManager)      // Generate JWT Token

            };

            return Ok(userDTO);
        }

    }

}
