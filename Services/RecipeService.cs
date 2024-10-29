using RecipeManagementApp.Data;

namespace RecipeManagementApp.Services
{
    public class RecipeService
    {
        private List<Recipe> _recipes; 

        public RecipeService() 
        {
            _recipes = new List<Recipe>
            {
                new Recipe
                {
	                Id = 1,
	                Name = "Burger",
	                Ingredients = new List<string> { "Buns", "Beef Patty", "Lettuce", "Cheese", "Ketchup" },
	                Instructions = "Grill patty, assemble burger",
	                Category = "Lunch",
	                ImageUrl = "/Images/burger.JFIF",
	                Description = "A classic burger recipe"
                },
                new Recipe
                {
	                Id = 2,
	                Name = "Manchurian",
	                Ingredients = new List<string> { "Vegetables", "Soy Sauce", "Cornflour", "Garlic" },
	                Instructions = "Fry vegetables, make sauce, combine",
	                Category = "Dinner",
					 ImageUrl = "/Images/manchorian.JFIF",
					Description = "An Indo-Chinese dish"
                },
                new Recipe
                {
	                Id = 3,
	                Name = "Biryani",
	                Ingredients = new List<string> { "Rice", "Chicken", "Spices", "Yogurt" },
	                Instructions = "Cook rice and chicken with spices",
	                Category = "Dinner",
					 ImageUrl = "/Images/biryani.JFIF",
					Description = "A flavorful and aromatic rice dish"
                },
                new Recipe
                {
	                Id = 4,
	                Name = "Pizza",
	                Ingredients = new List<string> { "Pizza Dough", "Tomato Sauce", "Cheese", "Toppings" },
	                Instructions = "Spread sauce on dough, add toppings, bake",
	                Category = "Lunch",
					 ImageUrl = "/Images/pizza.JFIF",
					Description = "A homemade pizza recipe"
                },
                new Recipe
                {
                    Id = 5,
                    Name = "Pancakes",
                    Ingredients = new List<string> { "Flour", "Eggs", "Milk", "Sugar", "Baking Powder" },
                    Instructions = "Mix ingredients, cook on skillet, flip once bubbly",
                    Category = "Breakfast",
                    ImageUrl = "/Images/pancakes.JFIF",
                    Description = "Fluffy homemade pancakes"
                },
                new Recipe
                {
                    Id = 6,
                    Name = "Omelette",
                    Ingredients = new List<string> { "Eggs", "Onions", "Bell Peppers", "Cheese" },
                    Instructions = "Beat eggs, add vegetables, cook in pan, fold and serve",
                    Category = "Breakfast",
                    ImageUrl = "/Images/omelette.jpg",
                    Description = "A simple and healthy egg omelette"
                },
                new Recipe
                {
                    Id = 7,
                    Name = "Grilled Chicken Salad",
                    Ingredients = new List<string> { "Grilled Chicken Breast", "Lettuce", "Cherry Tomatoes", "Cucumber", "Olive Oil", "Lemon Juice" },
                    Instructions = "Grill the chicken, chop the vegetables, and toss everything together with dressing.",
                    Category = "Salad",
                    ImageUrl = "/Images/grilled.JFIF",
                    Description = "A healthy and fresh grilled chicken salad with a zesty dressing."
                },
                new Recipe
                {
                    Id = 8,
                    Name = "Chocolate Cake",
                    Ingredients = new List<string> { "Flour", "Cocoa Powder", "Sugar", "Eggs", "Butter", "Baking Powder", "Milk" },
                    Instructions = "Mix dry ingredients, add wet ingredients, and bake at 350°F for 30 minutes.",
                    Category = "Dessert",
                    ImageUrl = "/Images/cake.JFIF",
                    Description = "A rich and moist chocolate cake perfect for celebrations."
                },
                new Recipe
                {
                    Id = 9, 
                    Name = "Spaghetti Carbonara",
                    Ingredients = new List<string> { "Spaghetti", "Eggs", "Parmesan Cheese", "Pancetta", "Pepper", "Garlic" },
                    Instructions = "Cook spaghetti, fry pancetta, mix with eggs and cheese, and toss together.",
                    Category = "Pasta",
                    ImageUrl = "/Images/spaghetti.JFIF",
                    Description = "A creamy and classic Italian pasta dish made with eggs, cheese, and pancetta."
                },
            };
        }

        // Returns a list of all recipes
        public List<Recipe> GetAllRecipes()
        {
            return _recipes;
        }

        // Returns a single recipe based on its ID
        public Recipe GetRecipeById(int id)
        {
            return _recipes.FirstOrDefault(r => r.Id == id);
        }

        // Adds a new recipe to the list
        public void AddRecipe(Recipe newRecipe)
        {
            newRecipe.Id = _recipes.Any() ? _recipes.Max(r => r.Id) + 1 : 1; // Check if _recipes is empty
            _recipes.Add(newRecipe);
        }

        // Deletes a recipe based on its ID
        public void DeleteRecipe(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                _recipes.Remove(recipe);
            }
        }

        // Updates an existing recipe
        public void UpdateRecipe(Recipe recipe)
        {
            var existingRecipe = _recipes.FirstOrDefault(r => r.Id == recipe.Id); // Renamed variable for clarity
            if (existingRecipe != null)
            {
                existingRecipe.Name = recipe.Name;
                existingRecipe.Category = recipe.Category;
                existingRecipe.Ingredients = recipe.Ingredients;
                existingRecipe.Instructions = recipe.Instructions;
                existingRecipe.ImageUrl = recipe.ImageUrl;
                existingRecipe.Description = recipe.Description;
            }
        }
        // Returns recipes filtered by category
        public List<Recipe> GetRecipesByCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) // Simplified condition
            {
                return _recipes; // If category is empty, return all recipes
            }
            return _recipes.Where(r => r.Category.ToLower() == category.ToLower()).ToList(); // Added ToLower for case-insensitivity
        }
    }
}