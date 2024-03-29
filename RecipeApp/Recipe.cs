using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecipeApp
{
    internal class Recipe
    {
        public RecipeIngredient[] Ingredients {  get; set; }
        public RecipeStep[] Instructions {  get; set; }
        public float ScaleFactor { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// -------------------------------------------------------------------------
        public Recipe() { }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="recipeName"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        /// -------------------------------------------------------------------------
        public Recipe(RecipeIngredient[] ingredients, RecipeStep[] steps, float scaleFactor)
        {
            this.Ingredients = ingredients;
            this.Instructions = steps;
            this.ScaleFactor = scaleFactor;
        }

        /// <summary>
        /// Returns the recipe object as a string.
        /// </summary>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public override string ToString()
        {
            if (Ingredients != null && Instructions != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("----------------------------");
                sb.AppendLine($"Number of ingredients: \t{Ingredients.Length}");
                sb.AppendLine($"Number of steps: \t{Instructions.Length}");
                sb.AppendLine();

                sb.AppendLine("Ingredients:");

                for (int i = 0; i < Ingredients.Length; i++)
                {
                    sb.AppendLine($"\t{Ingredients[i].ToString(ScaleFactor)}");
                }

                sb.AppendLine("----------------------------");
                sb.AppendLine();
                sb.AppendLine("Instructions (Steps):");

                for (int i = 0; i < Instructions.Length; i++)
                {
                    sb.AppendLine($"\tStep {i}: {Instructions[i].HelpText}");
                }

                sb.AppendLine("----------------------------");

                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Clears the recipe to its original state.
        /// </summary>
        public void Clear()
        {
            this.Ingredients = null;
            this.Instructions = null;
            this.ScaleFactor = 1.0f;
        }
    }
}
