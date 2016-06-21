using System.Linq;
using SudokuSolver.Service;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Process class used to solve a Sudoku puzzle.
    /// </summary>
    public class GridProcessor
    {
        /// <summary>
        ///     The method used to solve a Sudoku grid.
        /// </summary>
        /// <param name="filePath">
        ///     A string that represents the File Path to the test file.
        /// </param>
        public void Process(string filePath)
        {
            var grid = GridService.LoadGrid(filePath);

            var initilizationProcessor = StrategyProcessorFactory.GetInitilizationProcessor();
            initilizationProcessor.Process(grid);

            int changeCount;

            do
            {
                var processors = StrategyProcessorFactory.GetStrategyProcessors();
                changeCount = processors.Sum(processor => processor.Process(grid));
            } while (changeCount > 0);

            GridService.SaveGrid(GridService.GetSaveName(filePath), grid);
        }
       
    }
}
