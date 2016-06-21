using System.Linq;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Process class used to solve a Sudoku puzzle
    /// </summary>
    public class GridProcessor
    {
        /// <summary>
        ///     The method used to solve a Sudoku grid
        /// </summary>
        /// <param name="grid">
        ///     The Sudoku grid to solve
        /// </param>
        public void Process(Grid grid)
        {
            var initilizationProcessor = StrategyProcessorFactory.GetInitilizationProcessor();
            initilizationProcessor.Process(grid);

            int changeCount;

            do
            {
                var processors = StrategyProcessorFactory.GetStrategyProcessors();
                changeCount = processors.Sum(processor => processor.Process(grid));
            } while (changeCount > 0);
        }
       
    }
}
