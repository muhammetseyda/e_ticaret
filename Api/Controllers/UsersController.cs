using Api.Models;
using Data_MongoDb.Abstract;
using Data_SSMS.Abstract;
using Entities_MongoDb.Models;
using Entities_SSMS.Models;
using Microsoft.AspNetCore.Mvc;
using Services_MongoDb.Abstract;
using Services_SSMS.Abstract;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserServices _userServices;
        private readonly IUserRepositorySSMS _userRepositorySSMS;
        private readonly IUserServicesSSMS _userServicesSSMS;
        public UsersController(IUserRepository userRepository, IUserServices userServices, IUserRepositorySSMS userRepositorySSMS, IUserServicesSSMS userServicesSSMS)
        {
            _userRepository = userRepository;
            _userServices = userServices;
            _userRepositorySSMS = userRepositorySSMS;
            _userServicesSSMS = userServicesSSMS;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MDUsers>>> Get()
        {
            var users = await _userServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MDUsers>> Get(string id)
        {
            try
            {
                var user = await _userServices.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new {success = false, message="Kullanıcı bulunamadı."}); 
                }

                return Ok(new { success = true, message = user }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false, message=ex.Message});
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModels user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            MDUsers model = new MDUsers();
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Gender = user.Gender;
            model.Role = user.Role;

            await _userServices.CreateUser(model);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, model);
        }

        [HttpGet("ssms")]
        public async Task<ActionResult<List<SSMSUsers>>> GetSSMS()
        {
            try
            {
                var users = await _userServicesSSMS.GettAllUsers();
                if(users == null)
                {
                    return NotFound(new{ success=false, message="Kullanıcı bulunamadı."});
                }
                else
                {
                    return Ok(new { success=true, message = users});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false, message=ex.Message});
            }
        }
    }
}
