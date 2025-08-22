using Api.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

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
        public async Task<ActionResult<User>> Register([FromBody] RegisterUserDto user)
        {
            var newUser = await _userRepository.Register(user);
            if (newUser is null)
            {
                _logger.LogDebug("Can't save user.");
                return BadRequest();
            }

            return Ok(newUser);
        }
    }
}
