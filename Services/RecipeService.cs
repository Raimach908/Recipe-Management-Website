using Dapper;
using Microsoft.Data.SqlClient;
using RecipeManagementApp.Data;

namespace RecipeManagementApp.Services
{
    public class RecipeService
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RecipeManagementApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public RecipeService() { }
        public RecipeService(string connectionString)
        {
            _connectionString = connectionString;
        }
        // Returns a list of all recipes
        public List<Recipe> GetAllRecipes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Recipes";
                var recipes = connection.Query<Recipe>(sql).ToList();
                return recipes;
            }
        }

        public Recipe GetRecipeById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Recipes WHERE Id = @Id";
                var recipe = connection.QueryFirstOrDefault<Recipe>(sql, new { Id = id });
                return recipe ?? new Recipe(); // Avoid returning null
            }
        }


        // Adds a new recipe to the database
        public void AddRecipe(Recipe newRecipe)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Recipes (Name, Category, IngredientsString, Instructions, ImageData, Description) 
                       VALUES (@Name, @Category, @IngredientsString, @Instructions, @ImageData, @Description);
                       SELECT CAST(SCOPE_IDENTITY() as int);";
                newRecipe.Id = connection.QuerySingle<int>(sql, new
                {
                    newRecipe.Name,
                    newRecipe.Category,
                    newRecipe.IngredientsString,
                    newRecipe.Instructions,
                    newRecipe.ImageData,
                    newRecipe.Description
                });
            }
        }

        // Updates an existing recipe in the database
        public void UpdateRecipe(Recipe recipe)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Recipes SET 
                        Name = @Name, 
                        Category = @Category, 
                        IngredientsString = @IngredientsString, 
                        Instructions = @Instructions, 
                        ImageData = @ImageData, 
                        Description = @Description 
                       WHERE Id = @Id";
                connection.Execute(sql, new
                {
                    recipe.Name,
                    recipe.Category,
                    recipe.IngredientsString,
                    recipe.Instructions,
                    recipe.ImageData,
                    recipe.Description,
                    recipe.Id
                });
            }
        }
        // Deletes a recipe based on its ID
        public void DeleteRecipe(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Recipes WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        // Returns recipes filtered by category
        public List<Recipe> GetRecipesByCategory(string category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (string.IsNullOrEmpty(category))
                {
                    return GetAllRecipes(); // If no category is specified, return all recipes
                }

                string sql = "SELECT * FROM Recipes WHERE LOWER(Category) = LOWER(@Category)";
                return connection.Query<Recipe>(sql, new { Category = category }).ToList();
            }
        }
    }
}
