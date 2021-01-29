using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace LogicLayer
{
    public interface IIngredientManager
    {
        List<Ingredient> RetrieveAllIngredients();

        bool AddIngredient(Ingredient ingredient);

        bool EditIngredientData(Ingredient oldIngredient, Ingredient newIngredient);

        bool DeleteIngredient(Ingredient ingredient);

        List<RecipeIngredient> RetrieveIngredientsByRecipeID(int recipeID);

        bool AddIngredientIntoRecipe(int recipeID, int ingredientID, decimal quantity);

        bool DeleteIngredientFromRecipe(int recipeID, int ingredientID, decimal quantity);
    }
}
