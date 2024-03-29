using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    internal class WorkerClass
    {
        // Data fields
        private Recipe recipe = new Recipe();
        private bool recipeCreated = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkerClass() { }

        /// <summary>
        /// This is the method that runs the worker class.
        /// </summary>
        /// -------------------------------------------------------------------------
        public void Run()
        {
            // Assign the console title.
            Console.Title = "RecipeApp";

            // Print out the author details and instructions.
            ConsoleIO.PrintColorText("Welcome to RecipeApp.", ConsoleColor.DarkCyan, true);
            Console.WriteLine("Made by: Matthew De Waal");
            Console.WriteLine("Student Number: ST10069070");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press M to launch the menu.");
            Console.WriteLine("Press any other key to exit the program.");

            ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);

            switch(keyInfo.Key)
            {
                case ConsoleKey.M:
                    LaunchMenu();
                    break;

                default:
                    // Do nothing: This will exit the console application.
                    break;
            }
        }

        /// <summary>
        /// This method will launch the menu for the program.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void LaunchMenu()
        {
            // Display the menu.
            ConsoleIO.PrintColorText("\nMenu:", ConsoleColor.Green, true);
            Console.WriteLine("\tA - Add a new recipe [Only able to add one recipe]");
            Console.WriteLine("\tB - Scale a recipe's ingredients");
            Console.WriteLine("\tC - View a recipe");
            Console.WriteLine("\tD - Reset recipe");
            Console.WriteLine("Press any other key to exit the program.");

            ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);
            bool exitApp = false;

            // Handle the input from the user.
            switch(keyInfo.Key)
            {
                case ConsoleKey.A:
                    Operation_NewRecipe();
                    break;

                case ConsoleKey.B:
                    Operation_ScaleRecipe();
                    break;

                case ConsoleKey.C:
                    Operation_ViewRecipe();
                    break;

                case ConsoleKey.D:
                    Operation_ResetRecipe();
                    break;

                default:
                    exitApp = true;
                    break;
            }

            if(!exitApp)
                LaunchMenu();
        }

        /// <summary>
        /// This method handles the operation of a new recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_NewRecipe()
        {
            if (!recipeCreated)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Add New Recipe", ConsoleColor.Blue, true);
                Console.WriteLine("-----------------------------------------");

                // Obtain the ingredients from the user.
                int nIngredients = Convert.ToInt32(ConsoleIO.GetValidInput("Enter the number of ingredients: ", ConsoleColor.Yellow, InputDataType.PositiveNumbers, false));
                RecipeIngredient[] ingredients = new RecipeIngredient[nIngredients];

                for (int i = 0; i < nIngredients; i++)
                {
                    Console.WriteLine($"Ingredient {i}:");

                    string ingredientName = ConsoleIO.GetValidInput("Name: ", ConsoleColor.Yellow, InputDataType.LettersAndNumbers, false);
                    int ingredientQuantity = Convert.ToInt32(ConsoleIO.GetValidInput("Quantity: ", ConsoleColor.Yellow, InputDataType.Numbers, false));

                    DisplayUnitOfMeasurement();
                    UnitMeasurement measurement = default;
                    bool success = false;

                    while (!success)
                    {
                        string sUnitOfMeasurement = ConsoleIO.GetValidInput("Enter the unit of measurement. Provide the abbreviation only: ", ConsoleColor.Yellow, InputDataType.Letters, false);
                        measurement = GetUnitMeasurement(sUnitOfMeasurement, ref success);

                        if (!success)
                        {
                            // Display the error to the user.
                            ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                            Console.WriteLine("Invalid input provided. Expected a value from the measurement list.");
                        }
                    }

                    ingredients[i] = new RecipeIngredient(ingredientName, ingredientQuantity, measurement);
                }

                // Obtain the steps from the user
                int nSteps = Convert.ToInt32(ConsoleIO.GetValidInput("Enter the number of steps: ", ConsoleColor.Yellow, InputDataType.PositiveNumbers, false));
                RecipeStep[] steps = new RecipeStep[nSteps];

                for (int i = 0; i < nSteps; i++)
                {
                    Console.WriteLine($"Step {i}:");

                    Console.WriteLine("Description: ");
                    string description = ConsoleIO.GetInput(ConsoleColor.Yellow);

                    steps[i] = new RecipeStep(description);
                }

                // Assign the instructions and steps to the recipe object.
                recipe.Ingredients = ingredients;
                recipe.Instructions = steps;
                recipe.ScaleFactor = 1.0f;
                recipeCreated = true;
                
                ConsoleIO.PrintColorText("\nSuccessfully added the new recipe.", ConsoleColor.DarkGreen, true);
            }
            else
            {
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to add a new recipe. Only allowed to add one recipe.");
            }
        }

        /// <summary>
        /// This method handles the operation of scaling an existing recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_ScaleRecipe()
        {
            if (recipeCreated)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Scale a recipe's ingredients", ConsoleColor.Blue, true);
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("\t1 - Scale by 0.5");
                Console.WriteLine("\t2 - Scale by 1 [This will reset the scale]");
                Console.WriteLine("\t3 - Scale by 2");
                Console.WriteLine("\t4 - Scale by 3");

                int selectedMenuItem = Convert.ToInt32(ConsoleIO.GetValidInput("Choose the scale factor. Provide the number only: ", ConsoleColor.Yellow, InputDataType.PositiveDecimals, false));
                float scaleFactor = 0.0f;
                bool foundError = false;

                switch (selectedMenuItem)
                {
                    case 1: scaleFactor = 0.5f; break;
                    case 2: scaleFactor = 1.0f; break;
                    case 3: scaleFactor = 2.0f; break;
                    case 4: scaleFactor = 3.0f; break;
                    // Force an error if the menu choice
                    // is out of range.
                    default: foundError = true; break;
                }

                if (!foundError)
                {
                    recipe.ScaleFactor = scaleFactor;
                    ConsoleIO.PrintColorText("\nSuccessfully updated the scale factor.", ConsoleColor.DarkGreen, true);
                }
                else
                {
                    // Display the error message to the user.
                    ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                    Console.WriteLine("Invalid option provided.");
                }
            }
            else
            {
                // Display the error message to the user.
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to scale the recipe. The recipe has not been created yet.");
            }
        }

        /// <summary>
        /// This method handles the operation of viewing a recipe.
        /// </summary>
        private void Operation_ViewRecipe()
        {
            if (recipeCreated)
            {
                ConsoleIO.PrintColorText("\nOPERATION: View a recipe", ConsoleColor.Blue, true);
                Console.WriteLine(recipe.ToString());
            }
            else
            {
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to view the recipe. The recipe has not been created yet.");
            }    
        }

        /// <summary>
        /// This method handles the operation of resetting a recipe.
        /// </summary>
        private void Operation_ResetRecipe()
        {
            if (recipeCreated)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Reset recipe", ConsoleColor.Blue, true);
                Console.WriteLine("-----------------------------------------");

                Console.WriteLine("Are you sure you want to reset the recipe. Enter (Y/N):");
                ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    recipe.Clear();
                    recipeCreated = false;

                    ConsoleIO.PrintColorText("\nSuccessfully cleared the recipe.", ConsoleColor.DarkGreen, true);
                }
            }
            else
            {
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to reset the recipe. The recipe has not been created yet.");
            }
        }

        /// <summary>
        /// Displays all the unit of measurements that are available.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void DisplayUnitOfMeasurement()
        {
            // Display the unit of measurements to help the user.
            Console.WriteLine("------------------------------");
            Console.WriteLine("List of measurements:");
            Console.WriteLine("ML - Millilitres");
            Console.WriteLine("L - Litres");
            Console.WriteLine("G - Grams");
            Console.WriteLine("KG - Kilograms");
            Console.WriteLine("t - Teaspoon");
            Console.WriteLine("T - Tablespoon");
            Console.WriteLine();
        }

        /// <summary>
        /// Converts a string to a UnitMeasurement.
        /// </summary>
        /// <param name="sMeasurement"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private UnitMeasurement GetUnitMeasurement(string sMeasurement, ref bool success)
        {
            UnitMeasurement measurement = default;

            switch(sMeasurement)
            {
                case "ML":
                    measurement = UnitMeasurement.Millilitres; 
                    success = true; 
                    break;

                case "L":
                    measurement = UnitMeasurement.Litres;
                    success = true;
                    break;

                case "G":
                    measurement = UnitMeasurement.Grams;
                    success = true;
                    break;
                case "KG":
                    measurement = UnitMeasurement.Kilograms;
                    success = true;
                    break;
                case "t":
                    measurement = UnitMeasurement.Teaspoon;
                    success = true;
                    break;
                case "T":
                    measurement = UnitMeasurement.Tablespoon;
                    success = true;
                    break;

                default: 
                    success = false; 
                    break;
            }

            return measurement;
        }
    }
}
