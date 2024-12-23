using RecipeManagementApp.Data;
using System.Collections.Generic;

namespace RecipeManagementApp.Services
{
    public interface ICartService
    {
        IEnumerable<CartItem> GetCartItems(string userEmail);
        void AddToCart(string userEmail, int recipeId, string recipeName);
        void RemoveFromCart(string userEmail, int recipeId);
        void ClearCart(string userEmail);
    }
}
