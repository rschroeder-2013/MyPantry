using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
    public class RecipeManager : IRecipeManager
    {
        private IRecipeAccessor _recipeAccessor;

        public RecipeManager()
        {
            _recipeAccessor = new RecipeAccessor();
        }

        public RecipeManager(IRecipeAccessor iAccessor)
        {
            this._recipeAccessor = iAccessor;
        }

        public bool AddRecipe(string recipeName)
        {
            bool result = false;
            try
            {
                if (recipeName.Length == 0)
                {
                    throw new ApplicationException("Recipe not added.");
                }
                else
                {
                    _recipeAccessor.InsertNewRecipeName(recipeName);
                }
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insert Failed.", ex);
            }
           

            return result;
        }

        public bool DeleteRecipe(int recipeID)
        {
            bool result = false;
            try
            {
                
                _recipeAccessor.DeleteRecipeAndIngredients(recipeID);
                

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Deletion Failed.", ex);
            }

            return result;
        }

        public List<Recipe> RetrieveAllRecipes()
        {
            List<Recipe> recipes = null;

            recipes = _recipeAccessor.SelectAllRecipes();

            return recipes;
        }
    }
}
