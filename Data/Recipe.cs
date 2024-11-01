namespace RecipeManagementApp.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Instructions { get; set; }
        public string? Category { get; set; }
        public string? IngredientsString { get; set; }

        public byte[]? ImageData { get; set; } // Store the image as byte array
        public string? Description { get; set; }
    }
}
