global using Identity_Address = Talabat.core.Entitys.Identity.Address;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.core.Entitys.Identity;
using Talabat.core.Services.Contract;
using System.Security.Claims;
using Talabat.APIS.Extensions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Talabat.Applacation;
using Talabat.APIS.DTOs.Authntction;
using Talabat.APIS.DTOs.Shared;

namespace Talabat.APIS.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager,IToken _tokenService,IMapper _mapper) : ApiBaseController
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
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            // Find user by email
            var findUser = await _userManager.FindByEmailAsync(model.email);
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

        [Authorize]
        [HttpGet("GetAddres")]
        public async Task<ActionResult<AddressDto>> GetAddres()
        {

            var user = await _userManager.FindUserWithAddressByEmail(User);

            var AddressDto=_mapper.Map<AddressDto>(user.address);

            return Ok(AddressDto);
        }


        [Authorize]
        [HttpPut("UpdateAddres")]
        public async Task<ActionResult<AddressDto>> UpdateAddres(AddressDto addressDto)
        {

            var updatedAddress = _mapper.Map<Identity_Address>(addressDto);

            var user = await _userManager.FindUserWithAddressByEmail(User);
            if (user == null)
                return NotFound("User not found.");

            // Map the DTO to the Address entity
            updatedAddress.Id = user.address.Id;

            user.address = updatedAddress;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Return the updated address as a DTO
            return Ok(addressDto);
        }
        [HttpGet("emailexists") ]
        public async Task<ActionResult<bool>> ChekeEmailAddressExixt(string email)
           =>  await _userManager.FindByEmailAsync(email) is not null;  

         [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurentUser()
        {
            var email = User.FindFirstValue( ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(email);

            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        
        }

    }

}
