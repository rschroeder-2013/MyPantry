using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPantry
{
    public static class ValidationHelpers
    {
        public static bool isValidPassword(this string password)
        {
            bool result = false;

            if (password.Length >= 7)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidEmail(this string email)
        {
            var result = false;

            if (email.Length > 6
                && email.Length <= 100
                && email.Contains("@")
                && email.Contains("."))
            {
                result = true;
            }

            return result;
        }

        public static bool isValidFirstName(this string firstName)
        {
            bool result = false;

            if (firstName.Length >= 1 && firstName.Length <= 50)
            {
                result = true;
            }

            return result;
        }
        public static bool isValidLastName(this string lastName)
        {
            bool result = false;

            if (lastName.Length >= 1 && lastName.Length <= 100)
            {
                result = true;
            }

            return result;
        }
        public static bool isValidPhoneNumber(this string phoneNumber)
        {
            bool result = false;

            if (phoneNumber.Length >= 7 && phoneNumber.Length <= 15)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidIngredientName(this string ingredientName)
        {
            bool result = false;

            if (ingredientName.Length >= 3 || ingredientName != null)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidIngredientType(this string ingredientType)
        {
            bool result = false;

            if (ingredientType.Length >= 2 || ingredientType != null)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidIngredientMeasurement(this string ingredientMeasurement)
        {
            bool result = false;

            if (ingredientMeasurement.Length >= 2 || ingredientMeasurement != null)
            {
                result = true;
            }

            return result;
        }


    }
}
