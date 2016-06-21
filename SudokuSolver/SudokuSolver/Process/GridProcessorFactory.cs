namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that instantiates the GridProcessor.
    /// </summary>
    public class GridProcessorFactory
    {
        /// <summary>
        ///     Method that instantiates and returns a GridProcessor
        /// </summary>
        /// <returns>
        ///     A GridProcessor
        /// </returns>
        public static GridProcessor GetGridProcessor()
        {
            return new GridProcessor();
        }
    }
}