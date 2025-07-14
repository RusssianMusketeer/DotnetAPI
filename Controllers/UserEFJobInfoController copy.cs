using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFJobInfoController : ControllerBase
{
    private IJobInfoRepository _jobInfoRepository;
    public UserEFJobInfoController(IJobInfoRepository jobInfoRepository)
    {

        _jobInfoRepository = jobInfoRepository;
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {

        IEnumerable<UserJobInfo> usersJobInfo = _jobInfoRepository.GetUsersJobInfo();

        return usersJobInfo;

    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        return _jobInfoRepository.GetSingleJobInfo(userId);

    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _jobInfoRepository.GetSingleJobInfo(userJobInfo.UserId);

        if (userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;

            if (_jobInfoRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Job Info");
        }

        throw new Exception("Failed to Get User Job Info");

    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoDto userJobInfo)
    {
        UserJobInfo userJobInfoDb = new UserJobInfo();
        
            userJobInfoDb.Department = userJobInfo.Department;
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;

            _jobInfoRepository.AddEntity<UserJobInfo>(userJobInfoDb);

            if (_jobInfoRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Add User Job Info");
    }

    [HttpDelete("Delete/UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userDbJobInfo = _jobInfoRepository.GetSingleJobInfo(userId);

        if (userDbJobInfo != null)
        {
            _jobInfoRepository.DeleteEntity<UserJobInfo>(userDbJobInfo);

            if (_jobInfoRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Job Info");
        }

        throw new Exception("Failed to Get User Job Info");
    }
}

