using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using RecipeManagementApp.Data;
using RecipeManagementApp.Interfaces;

namespace RecipeManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool RegisterUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Check if the email already exists
                var existingUser = db.QueryFirstOrDefault<User>(
                    "SELECT * FROM Users WHERE Email = @Email",
                    new { user.Email });

                if (existingUser != null)
                    return false; // Email already exists

                // Hash the password
                user.PasswordHash = HashPassword(user.PasswordHash);

                // Insert the user
                var sql = @"INSERT INTO Users (Name, Email, PasswordHash, PhoneNumber) 
                            VALUES (@Name, @Email, @PasswordHash, @PhoneNumber)";
                db.Execute(sql, user);
                return true;
            }
        }

        public User AuthenticateUser(string emailOrPhone, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Users 
                            WHERE Email = @EmailOrPhone OR PhoneNumber = @EmailOrPhone";
                var user = db.QueryFirstOrDefault<User>(sql, new { EmailOrPhone = emailOrPhone });

                if (user == null || user.PasswordHash != HashPassword(password))
                    return null; // Authentication failed

                return user;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public User GetUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Id = @UserId";
                return db.QueryFirstOrDefault<User>(sql, new { UserId = userId });
            }
        }
        public User GetUserByEmail(string email)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Email = @Email";
                return db.QueryFirstOrDefault<User>(sql, new { Email = email });
            }
        }
        public bool UpdateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Users SET 
                        Name = @Name, 
                        Email = @Email, 
                        PhoneNumber = @PhoneNumber 
                        WHERE Id = @Id";
                return db.Execute(sql, user) > 0;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Users WHERE Id = @UserId";
                return db.Execute(sql, new { UserId = userId }) > 0;
            }
        }

    }
}
