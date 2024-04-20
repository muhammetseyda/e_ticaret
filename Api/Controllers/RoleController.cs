using Microsoft.AspNetCore.Mvc;
using Services_SSMS.Abstract;
using Services_SSMS.Concrete;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleservices;

        public RoleController(IRoleServices roleservices)
        {
            _roleservices = roleservices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody]string roleName)
        {
            try
            {
                var result = await _roleservices.CreateRoleAsync(roleName);
                if (result)
                {
                    return Ok(new {success = true, message=$"'{roleName}' oluşturuldu."});
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false, message = ex.Message});
            }
        }
    }
}
