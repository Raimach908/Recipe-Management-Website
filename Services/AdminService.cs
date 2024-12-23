using RecipeManagementApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RecipeManagementApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly List<Admin> _admins;

        public AdminService()
        {
            // Mock admin data for demonstration purposes
            _admins = new List<Admin>
            {
                new Admin
                {
                    Id = 1,
                    Email = "admin@example.com",
                    PasswordHash = HashPassword("admin123"),
                    Name = "Admin User"
                }
            };
        }

        public Admin? AuthenticateAdmin(string email, string password)
        {
            var admin = _admins.FirstOrDefault(a => a.Email == email);
            if (admin != null && admin.PasswordHash == HashPassword(password))
            {
                return admin;
            }
            return null;
        }

        public bool CreateAdmin(Admin admin)
        {
            if (_admins.Any(a => a.Email == admin.Email))
            {
                return false; // Email already exists
            }

            admin.Id = _admins.Count + 1;
            admin.PasswordHash = HashPassword(admin.PasswordHash!);
            _admins.Add(admin);
            return true;
        }

        public Admin? GetAdminByEmail(string email)
        {
            return _admins.FirstOrDefault(a => a.Email == email);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
