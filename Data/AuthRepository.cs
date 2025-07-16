using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        DataContextEF _entityFramework;

        public AuthRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool CheckUserExists(string email)
        {
            return _entityFramework.Auth.Where(u => u.Email == email).Count() > 0;
        }

        public void RegisterUser(Auth userToRegister)
        {
            if (userToRegister != null)
            {
                _entityFramework.Add(userToRegister);
            }
        }


        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        
    }
}