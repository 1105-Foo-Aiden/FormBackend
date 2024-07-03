using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;
namespace FormBackend.Controllers{
    [ApiController] [Route("[controller]")]
    public class UserController: ControllerBase{
        private readonly UserService _service;
        public UserController(UserService service){
            _service = service;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] CreateAccountDTO user) => _service.AddUser(user) ? Ok("sucessfully added") : BadRequest("error adding user");

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO user) => _service.Login(user);

        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<UserDTO> GetAllUsers() => _service.GetUsers();

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public UserDTO GetUserByUsername(string username) => _service.GetUserByUsernameEndoint(username);

        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassDTO newPass) => _service.ResetPassword(newPass) ? Ok("Password reset") : BadRequest("Error resetting password");

        [HttpPut]
        [Route("EditUser")]
        public IActionResult EditUser([FromBody] UpdateUserDTO UserToUpdate) => _service.EditUser(UserToUpdate) ? Ok("Successfully Updated") : BadRequest("Error updating user");

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id) => _service.DeleteUser(id) ? Ok("Successfully Deleted") : BadRequest("Error deleting user");

    }
}