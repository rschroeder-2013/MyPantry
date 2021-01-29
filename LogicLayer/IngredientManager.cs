using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class IngredientManager : IIngredientManager
    {
        
        private IIngredientAccessor _ingredientAccessor;

        public IngredientManager()
        {
            _ingredientAccessor = new IngredientAccessor();
        }

        public IngredientManager(IIngredientAccessor iAccessor)
        {
            this._ingredientAccessor = iAccessor;
        }

        public List<Ingredient> RetrieveAllIngredients()
        {
            List<Ingredient> ingredients = null;

            ingredients = _ingredientAccessor.SelectAllIngredients();

            return ingredients;
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            bool result = false;

            try
            {
                int ingredientID = 0;
                ingredientID = _ingredientAccessor.InsertIngredient(ingredient);

                if (ingredientID == 0)
                {
                    throw new ApplicationException("Ingredient not added");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insert Failed.", ex);
            }
            return result;
        }

        public bool EditIngredientData(Ingredient oldIngredient, Ingredient newIngredient)
        {
            bool result = false;

            try
            {
                result = (1 == _ingredientAccessor.UpdateIngredientData(oldIngredient, newIngredient));

                if (result == false)
                {
                    throw new ApplicationException("Data not updated.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            bool result = false;

            try
            {
                _ingredientAccessor.DeleteIngredient(ingredient);           
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Deletion Failed.", ex);
            }
            return result;
        }

        public List<RecipeIngredient> RetrieveIngredientsByRecipeID(int recipeID)
        {
            List<RecipeIngredient> recipeIngredients = null;

            recipeIngredients = _ingredientAccessor.SelectIngredientQuantityByRecipeID(recipeID);

            return recipeIngredients;
        }

        public bool AddIngredientIntoRecipe(int recipeID, int ingredientID, decimal quantity)
        {
            bool result = false;

            try
            {
                _ingredientAccessor.InsertIngredientIntoRecipe(recipeID, ingredientID, quantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ingredient Insertion Failed.", ex);
            }

            return result;
        }

        public bool DeleteIngredientFromRecipe(int recipeID, int ingredientID, decimal quantity)
        {
            bool result = false;

            try
            {
                _ingredientAccessor.DeleteIngredientFromRecipe(recipeID, ingredientID, quantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ingredient Deletion Failed.", ex);
            }

            return result;
        }
    }
}
