using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace DataAccessLayer
{
    public interface IIngredientAccessor
    {
        List<Ingredient> SelectAllIngredients();

        int UpdateIngredientData(Ingredient oldIngredient, Ingredient newIngredient);

        int InsertIngredient(Ingredient ingredient);

        int DeleteIngredient(Ingredient ingredient);
        
        List<RecipeIngredient> SelectIngredientQuantityByRecipeID(int recipeID);

        int InsertIngredientIntoRecipe(int recipeID, int ingredientID, decimal quantity);

        int DeleteIngredientFromRecipe(int recipeID, int ingredientID, decimal quantity);

    }
}
