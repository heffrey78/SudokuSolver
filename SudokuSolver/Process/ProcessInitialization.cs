using System.Collections.Generic;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that runs the initlization process, removing
    ///     possible values that have already been taken by a neighbor square.
    /// </summary>
    public class ProcessInitialization : StrategyProcessor
    {
        public override int Process(Grid grid)
        {
            foreach (var square in grid)
            {
                if (square.Value.Value != 0)
                {
                    RemovePossibleValueFromNeighbors(grid, square);
                }
            }

            return 0;
        }

        /// <summary>
        ///     Non implemented method.
        /// </summary>
        /// <param name="grid">
        ///     A Grid.
        /// </param>
        /// <param name="group">
        ///     A KeyValuePair of type Coordinate, Square.
        /// </param>
        /// <returns>
        ///     A bool.
        /// </returns>
        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> @group)
        {
            throw new System.NotImplementedException();
        }
    }
}