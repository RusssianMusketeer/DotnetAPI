using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface ISalaryRepository
    {
        public bool SaveChanges();

        public void AddEntity<T>(T entityToAdd);

        public void DeleteEntity<T>(T entityToRemove);

        public IEnumerable<UserSalary> GetUsersSalary();

        public UserSalary GetSingleUserSalary(int userId);
    }
}