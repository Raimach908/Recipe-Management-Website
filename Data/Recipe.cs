namespace RecipeManagementApp.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string? Instructions { get; set; }
        public string? Category { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

    }
}
