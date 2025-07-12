using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFJobInfoController : ControllerBase
{
    DataContextEF _entityFramework;
    public UserEFJobInfoController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {

        IEnumerable<UserJobInfo> usersJobInfo = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return usersJobInfo;

    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfo != null)
        {
            return userJobInfo;
        }

        throw new Exception("Failed to Get UserJobInfo");

    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId)
            .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update UserJobInfo");
        }

        throw new Exception("Failed to Get UserJobInfo");

    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoDto userJobInfo)
    {
        UserJobInfo userJobInfoDb = new UserJobInfo();
        
            userJobInfoDb.Department = userJobInfo.Department;
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;

            
            _entityFramework.Add(userJobInfoDb);
            if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
            throw new Exception("Failed to Add UserJobInfo");
    }

    [HttpDelete("Delete/UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userDbJobInfo = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (userDbJobInfo != null)
        {
            _entityFramework.UserJobInfo.Remove(userDbJobInfo);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");
        }

        throw new Exception("Failed to Get User");
    }
}

