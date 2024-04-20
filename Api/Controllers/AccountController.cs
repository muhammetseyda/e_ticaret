using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Data_MongoDb.Abstract;
using Data_SSMS.Abstract;
using Entities_MongoDb.Models;
using Entities_SSMS.Models;
using Services_MongoDb.Abstract;
using Services_SSMS.Abstract;
using Entities_Genel.ViewModels;
using Data_SSMS.Identity;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private IRoleServices _roleServices;
        public AccountController(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager = null, IRoleServices roleServices = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleServices = roleServices;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new AppIdentityUser
                    {
                        UserName = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Gender = model.Gender,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _roleServices.AssignRoleAsync(user.Id,"User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok(model);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (model.Email == null)
                    return BadRequest(ModelState);

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    return Ok(new { Message = "Login successful", UserId = user.Id });
                }

                if (result.IsLockedOut)
                {
                    return BadRequest(new { Message = "Account is locked out" });
                }

                if (result.IsNotAllowed)
                {
                    return BadRequest(new { Message = "Account is not allowed to sign in" });
                }

                if (result.RequiresTwoFactor)
                {
                    return BadRequest(new { Message = "Two-factor authentication is required" });
                }

                return Unauthorized(new { Message = "Login failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false,message=ex.Message});
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new { Message = "Logout successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }

        }
    }
}
