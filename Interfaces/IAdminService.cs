using RecipeManagementApp.Data;

namespace RecipeManagementApp.Services
{
    public interface IAdminService
    {
        Admin? AuthenticateAdmin(string email, string password);
        bool CreateAdmin(Admin admin);
        Admin? GetAdminByEmail(string email);
    }
}
