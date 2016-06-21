using System;
using SudokuSolver.Process;
using SudokuSolver.Service;

namespace SudokuSolver
{
    public class Program
    {
        /// <summary>
        ///     Main method for SudokuSolver.
        /// </summary>
        /// <param name="args">
        ///     Array of type string used for input parameters
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            try
            {
                var filePath = args[0];
                var processor = GridProcessorFactory.GetGridProcessor();
                processor.Process(filePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }
}
