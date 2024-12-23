using RecipeManagementApp.Data;

namespace RecipeManagementApp.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        User AuthenticateUser(string emailOrPhone, string password);
        public User GetUserByEmail(string email);
        public User GetUserById(int userId);
        public bool UpdateUser(User user);
        public bool DeleteUser(int userId);
    }
}
