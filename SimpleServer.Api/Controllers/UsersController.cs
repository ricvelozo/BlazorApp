using Microsoft.AspNetCore.Mvc;
using SimpleServer.Domain.Users;

namespace SimpleServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ILogger<UsersController> logger, IUserRepository userRepository) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;

        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<User>> Index()
        {
            var users = await _userRepository.GetAll();

            return users;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
            {
                _logger.LogDebug("Invalid user ID.");
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Create([FromBody] UserCredentials user)
        {
            var newUser = await _userRepository.Create(user);
            if (newUser is null)
            {
                _logger.LogDebug("Can't save user.");
                return BadRequest();
            }

            return Ok(newUser);
        }
    }
}
