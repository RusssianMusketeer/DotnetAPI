using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IAuthRepository
    {
        public bool CheckUserExists(string email);

        public void RegisterUser(Auth userToRegister);

        public bool SaveChanges();


    }
}