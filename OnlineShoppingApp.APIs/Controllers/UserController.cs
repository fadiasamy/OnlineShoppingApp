using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.BL.Dtos.Login;
using OnlineShoppingApp.BL.Dtos.User;
using OnlineShoppingApp.BL.Services.User;
using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingApp.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    CustomerName = registerDto.UserName 
                };

                var registeredUser = await _userService.RegisterAsync(user, registerDto.Password);

                if (registeredUser == null)
                {
                    return BadRequest(new { Message = "Registration failed. User might already exist or invalid data provided." });
                }

                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred during registration.", Details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid credentials. Please check your email and password." });
                }

                var roles = await _userService.GetUserRolesAsync(user);

                var token = await _userService.GenerateJwtToken(user, roles);

                return Ok(new { Message = "Login successful", Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred during login.", Details = ex.Message });
            }
        }

    }
}
