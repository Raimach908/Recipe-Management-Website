using Microsoft.Data.SqlClient;
using Dapper;
using RecipeManagementApp.Data;

namespace RecipeManagementApp.Services
{
    public class CartService : ICartService
    {
        private readonly string _dbConnection;

        public CartService(string connectionString)
        {
            _dbConnection = connectionString;
        }

        // Get all cart items for a user
        public IEnumerable<CartItem> GetCartItems(string userEmail)
        {
            using var connection = new SqlConnection(_dbConnection);
            var query = "SELECT * FROM CartItems WHERE UserEmail = @UserEmail";
            return connection.Query<CartItem>(query, new { UserEmail = userEmail });
        }

        // Add a recipe to the cart
        public void AddToCart(string userEmail, int recipeId, string recipeName)
        {
            using var connection = new SqlConnection(_dbConnection);
            var query = @"
                IF EXISTS (SELECT 1 FROM CartItems WHERE UserEmail = @UserEmail AND RecipeId = @RecipeId)
                BEGIN
                    UPDATE CartItems
                    SET Quantity = Quantity + 1
                    WHERE UserEmail = @UserEmail AND RecipeId = @RecipeId
                END
                ELSE
                BEGIN
                    INSERT INTO CartItems (UserEmail, RecipeId, RecipeName, Quantity)
                    VALUES (@UserEmail, @RecipeId, @RecipeName, 1)
                END";

            connection.Execute(query, new { UserEmail = userEmail, RecipeId = recipeId, RecipeName = recipeName });
        }

        // Remove a specific item from the cart
        public void RemoveFromCart(string userEmail, int recipeId)
        {
            using var connection = new SqlConnection(_dbConnection);
            var query = "DELETE FROM CartItems WHERE UserEmail = @UserEmail AND RecipeId = @RecipeId";
            connection.Execute(query, new { UserEmail = userEmail, RecipeId = recipeId });
        }

        // Clear all cart items for a user
        public void ClearCart(string userEmail)
        {
            using var connection = new SqlConnection(_dbConnection);
            var query = "DELETE FROM CartItems WHERE UserEmail = @UserEmail";
            connection.Execute(query, new { UserEmail = userEmail });
        }
    }
}
