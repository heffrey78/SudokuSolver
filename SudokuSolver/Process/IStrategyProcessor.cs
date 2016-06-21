using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Interface that serves as a contract for Strategy Processing
    /// </summary>
    public interface IStrategyProcessor
    {
        /// <summary>
        ///     Method used to execute a Strategy.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <returns>
        ///     An int indicating whether changes were made to a Square
        ///     during the execution of a Strategy.
        /// </returns>
        int Process(Grid grid);
    }
}