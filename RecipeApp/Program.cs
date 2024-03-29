using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    internal class Program
    {
        /// <summary>
        /// Entry point for this program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Declare and instantiate a WorkerClass object.
            WorkerClass worker = new WorkerClass();
            // Execute/Run the WorkerClass object.
            worker.Run();
        }
    }
}
