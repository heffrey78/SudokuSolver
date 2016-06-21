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
            string filePath;

            if (args.Length == 0)
                return;

            try
            {
                filePath = args[0];
                var grid = GridService.LoadGrid(filePath);

                var processor = GridProcessorFactory.GetGridProcessor();
                processor.Process(grid);

                GridService.SaveGrid(GridService.GetSaveName(filePath), grid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
