using DotnetAPI.Models;
using Microsoft.Data.SqlClient;

namespace DotnetAPI.Data
{
    public interface IAuthRepository
    {
        public bool CheckUserExists(string email);

        public void RegisterUser(Auth userToRegister);

        public bool SaveChanges();

        public bool ExecuteSqlWithParameters(string sql, List<SqlParameter> sqlParameters);


    }
}