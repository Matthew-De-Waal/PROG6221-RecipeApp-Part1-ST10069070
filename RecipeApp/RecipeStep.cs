using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    internal class RecipeStep
    {
        public string HelpText { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RecipeStep()
        {
            this.HelpText = "";
        }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="helpText"></param>
        /// -------------------------------------------------------------------------
        public RecipeStep(string helpText)
        {
            this.HelpText = helpText;
        }
    }
}
