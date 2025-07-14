using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class SalaryRepository : ISalaryRepository
    {
        DataContextEF _entityFramework;

        public SalaryRepository(IConfiguration config)
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

        public IEnumerable<UserSalary> GetUsersSalary()
        {

            IEnumerable<UserSalary> usersSalary = _entityFramework.UserSalary.ToList<UserSalary>();

            return usersSalary;

        }

        public UserSalary GetSingleUserSalary(int userId)
        {

            UserSalary? userSalary = _entityFramework.UserSalary
               .Where(u => u.UserId == userId)
               .FirstOrDefault<UserSalary>();

            if (userSalary!= null)
            {
                return userSalary;
            }

            throw new Exception("Failed to Get User Salary");

        }
        

    }
}