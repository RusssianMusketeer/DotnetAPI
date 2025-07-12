using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFSalaryController : ControllerBase
{
    DataContextEF _entityFramework;
    public UserEFSalaryController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {

        IEnumerable<UserSalary> usersSalary = _entityFramework.UserSalary.ToList<UserSalary>();
        return usersSalary;

    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {

        UserSalary? userSalary = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

        if (userSalary != null)
        {
            return userSalary;
        }

        throw new Exception("Failed to Get User Salary");

    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userSalary.UserId)
            .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Salary");
        }

        throw new Exception("Failed to Get User Salary");

    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserSalary(UserSalaryDto userSalary)
    {
        UserSalary userSalaryDb = new UserSalary();
        
            userSalaryDb.Salary = userSalary.Salary;

            _entityFramework.Add(userSalaryDb);
            if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
            throw new Exception("Failed to Add User Salary");
    }

    [HttpDelete("Delete/UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userDbSalary = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

        if (userDbSalary  != null)
        {
            _entityFramework.UserSalary.Remove(userDbSalary);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");
        }

        throw new Exception("Failed to Get User Salary");
    }
}

