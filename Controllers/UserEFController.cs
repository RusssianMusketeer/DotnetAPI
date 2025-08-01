using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    private IUserRepository _userRepository;
    public UserEFController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {

        IEnumerable<User> users = _userRepository.GetUsers();
        return users;

    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {

        return _userRepository.GetSingleUser(userId);

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }

        throw new Exception("Failed to Get User");

    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = new User();
        
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            
            _userRepository.AddEntity<User>(userDb);
            if (_userRepository.SaveChanges())
        {
            return Ok();
        }
            throw new Exception("Failed to Add User");
    }

    [HttpDelete("Delete/User/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _userRepository.GetSingleUser(userId);

        if (userDb != null)
        {
            _userRepository.DeleteEntity<User>(userDb);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");
        }

        throw new Exception("Failed to Get User");
    }
}


