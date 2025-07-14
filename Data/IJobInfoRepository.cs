using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IJobInfoRepository
    {
        public bool SaveChanges();

        public void AddEntity<T>(T entityToAdd);

        public void DeleteEntity<T>(T entityToRemove);

        public IEnumerable<UserJobInfo> GetUsersJobInfo();

        public UserJobInfo GetSingleJobInfo(int userId);
    }
}