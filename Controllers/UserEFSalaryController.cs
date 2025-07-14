using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFSalaryController : ControllerBase
{
    private ISalaryRepository _salaryRepository;
    public UserEFSalaryController(ISalaryRepository salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {

        IEnumerable<UserSalary> usersSalary = _salaryRepository.GetUsersSalary();
        return usersSalary;

    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {

        return _salaryRepository.GetSingleUserSalary(userId);

    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _salaryRepository.GetSingleUserSalary(userSalary.UserId);

        if (userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;

            if (_salaryRepository.SaveChanges())
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

        _salaryRepository.AddEntity<UserSalary>(userSalaryDb);

        if (_salaryRepository.SaveChanges())
            {
                return Ok();
            }
        throw new Exception("Failed to Add User Salary");
    }

    [HttpDelete("Delete/UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userDbSalary = _salaryRepository.GetSingleUserSalary(userId);

        if (userDbSalary  != null)
        {
            _salaryRepository.DeleteEntity<UserSalary>(userDbSalary);

            if (_salaryRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");
        }

        throw new Exception("Failed to Get User Salary");
    }
}

