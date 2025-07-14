using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Data
{
    public class JobInfoRepository : IJobInfoRepository
    {
        DataContextEF _entityFramework;

        public JobInfoRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void DeleteEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove);
            }
        }

        public IEnumerable<UserJobInfo> GetUsersJobInfo()
        {

            IEnumerable<UserJobInfo> usersJobInfo = _entityFramework.UserJobInfo.ToList<UserJobInfo>();

            return usersJobInfo;

        }

        public UserJobInfo GetSingleJobInfo(int userId)
        {

            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
               .Where(u => u.UserId == userId)
               .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }

            throw new Exception("Failed to Get User Info");

        }
        

    }
}