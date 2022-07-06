using Boilerplate.API.Models;
using Boilerplate.Domain.Interfaces;
using Boilerplate.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.API.Controllers
{
    /// <summary>
    /// Class <c>UserController</c> handles API requests for user registration solution.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for <c>UserController</c> that uses <c>UserService</c>
        /// for record management.
        /// </summary>
        /// <param name="userService"></param>
        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint responsible to create new user.
        /// </summary>
        /// /// <remarks>
        /// 
        ///        Test
        ///        
        /// Sample request:
        ///
        ///     {
        ///         Sample
        ///     }
        ///
        /// </remarks>
        /// <param name="newUser"><c>CreateUserModel</c> class object with user data.</param>
        /// <returns>Created <c>GetUserModel</c> class object with data of the created user.</returns>
        [HttpPut("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateUserModel newUser)
        {
            if (newUser == null)
                return BadRequest("Could not create user with invalid input.");

            var createdUser =
                await _userService.Add<CreateUserModel, GetUserModel, UserValidator>(newUser);

            if (createdUser != null)
            {
                return Ok(createdUser);
            }

            return Conflict("Could not create user, internal operation failure.");
        }

        /// <summary>
        /// Enpoint responsible to retrieve a list of all users.
        /// </summary>
        /// <returns>List of <c>GetUserModel</c> class objects of all found users.</returns>
        [HttpGet("GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll<GetUserModel>();

            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }

        /// <summary>
        /// Enpoint responsible to retrieve user with corresponding id.
        /// </summary>
        /// <param name="id">Id of the user.</param>
        /// <returns><c>GetUserModel</c> class object of the corresponding id.</returns>
        [HttpGet("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid user id:'{id}'.");

            var user = await _userService.GetById<GetUserModel>(id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound($"Could not find user with id:'{id}'.");
        }

        /// <summary>
        /// Enpoint responsible to update user with new values.
        /// </summary>
        /// <remarks>
        /// 
        ///        Test
        ///        
        /// Sample request:
        ///
        ///     {
        ///        Sample
        ///     }
        ///
        /// </remarks>
        /// <param name="userToUpdate"><c>UpdateUserModel</c> class object with user data to be updated.</param>
        /// <returns>Updated <c>GetUserModel</c> class object.</returns>
        [HttpPatch("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] UpdateUserModel userToUpdate)
        {
            if (userToUpdate == null)
                return BadRequest("Could not update user with invalid input.");

            var updatedUser =
                await _userService.Update<UpdateUserModel, GetUserModel, UserValidator>(userToUpdate);

            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return Conflict("Could not update user, internal operation failure.");
        }

        /// <summary>
        /// Enpoint responsible to delete user registry by its id.
        /// </summary>
        /// <param name="id">Id of the user to be purged.</param>
        /// <returns>String message with success or failure to delete register.</returns>
        [HttpDelete("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid user id:'{id}'.");

            var response = await _userService.Delete(id);

            if (response)
            {
                return Ok($"Sucessfully deleted register of user with id:'{id}'");
            }

            return NotFound($"Could not find user with id:'{id}'.");
        }
    }
}
