namespace RecipeManagementApp.Data
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string name, string imageUrl, decimal price)
        {
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
        }
    }
}
