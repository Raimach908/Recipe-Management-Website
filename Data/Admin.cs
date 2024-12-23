namespace RecipeManagementApp.Data
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
