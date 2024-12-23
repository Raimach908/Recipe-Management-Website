namespace RecipeManagementApp.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? UserEmail { get; set; } // To associate cart items with users
        public int Quantity { get; set; } = 1;
    }
}
