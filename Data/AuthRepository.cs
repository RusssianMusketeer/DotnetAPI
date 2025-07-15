using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class AuthoRepository
    {
        DataContextEF _entityFramework;

        public AuthoRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool CheckUserExists(int userId)
        {
            return _entityFramework.Auth.Where(u => u.UserId == userId).Count() > 0;
        }
    }
}