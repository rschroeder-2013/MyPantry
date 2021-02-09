# MyPantry
Custom Grocery/Recipe Tracking Software

## Installation
As of 1/29/2021 this project is currently the MVP, and is not yet completed.

## Introduction
This project was created to assist me and my girlfriend to better plan what is bought during grocery runs. This eliminates handwriting a list and potentially omitting required ingredients. All recipes, ingredients, users, and roles are CRUD'd on a custom SQL DB. The softwares layer architecture (presentation, logic, data access, data transfer) was created from scratch (with some assistance from my college professor).

## Usage
The user has their own login that can be created by and Admin user, on initial login, they must change their password is the default password is used. The user can enter custom ingredients, their measurement type, ingredient type, and a description (Where they may buy these ingredients). From these ingredients, the user can then build a recipe by choosing an ingredient from the ingredient list they populated, and input the amount for that recipe. Once several recipes have been created, the user can then plan a week (or more) of which recipes they want to make on which day of the week. Once the user plans their week(s), the last function of this software consolidates each recipe and combines like ingredients and summing their amounts for every recipe chosen. This process is repeated for every ingredient in the users plan. The user can then print or save a text file that lists all the ingredients they will need to fulfill their recipe plan.

## Future Additions
1) Organize the grocery list alphabetically
2) Add a combobox to allow users to choose where they buy these ingredients from an easy-to-use dropdown. This list of stores can be appended by the user in a seperate window. This will allow users to group ingredients by store, rather than one long list.
3) Allow the user to track which ingredients they have in their personal kitchen and add/remove ingredients and their amounts based on which recipes were planned/made.
4) Implement user recipe limits. Standard users will only be allowed X number of recipes to save. Premium users will allow unlimited recipes.
