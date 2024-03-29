using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    internal class RecipeIngredient
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public UnitMeasurement Measurement { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// -------------------------------------------------------------------------
        public RecipeIngredient()
        {
            this.Name = "";
            this.Quantity = 0;
            this.Measurement = default;
        }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="measurement"></param>
        /// -------------------------------------------------------------------------
        public RecipeIngredient(string name, int quantity, UnitMeasurement measurement)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Measurement = measurement;
        }

        /// <summary>
        /// Returns the ingredient as a string.
        /// </summary>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public override string ToString()
        {
            string sMeasurement = "";

            switch(Measurement)
            {
                case UnitMeasurement.Millilitres: sMeasurement = "ML"; break;
                case UnitMeasurement.Litres: sMeasurement = "L"; break;
                case UnitMeasurement.Grams: sMeasurement = "G"; break;
                case UnitMeasurement.Kilograms: sMeasurement = "KG"; break;
                case UnitMeasurement.Teaspoon: sMeasurement = "Teaspoon"; break;
                case UnitMeasurement.Tablespoon: sMeasurement = "Tablespoon"; break;
            }

            return $"{Quantity} {sMeasurement} of {Name}";
        }

        /// <summary>
        /// Returns a string of the ingredient where the measurement is scaled.
        /// </summary>
        /// <param name="scaleValue"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public string ToString(float scaleFactor)
        {
            string sMeasurement = "";

            switch (Measurement)
            {
                case UnitMeasurement.Millilitres: sMeasurement = "ML"; break;
                case UnitMeasurement.Litres: sMeasurement = "L"; break;
                case UnitMeasurement.Grams: sMeasurement = "G"; break;
                case UnitMeasurement.Kilograms: sMeasurement = "KG"; break;
                case UnitMeasurement.Teaspoon: sMeasurement = "Teaspoon"; break;
                case UnitMeasurement.Tablespoon: sMeasurement = "Tablespoon"; break;
            }

            if (Quantity != 1)
                sMeasurement += "s";

            return $"{Quantity * scaleFactor} {sMeasurement} of {Name}";
        }
    }
}
