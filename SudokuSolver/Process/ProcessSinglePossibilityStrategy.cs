using System.Collections.Generic;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that implements the Single Possibility Strategy.
    /// </summary>
    public class ProcessSinglePossibilityStrategy : StrategyProcessor
    {
        /// <summary>
        ///     Method that executes the Single Possibility Strategy Algorithm.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to Process.
        /// </param>
        /// <param name="group">
        ///     The current group to solve for eg. Row, Column, or Region.
        /// </param>
        /// <returns>
        ///     An int indicating whether any changes were made.
        /// </returns>
        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> group )
        {
            int squaresSet;
            var hasValueBeenSet = false;

            do
            {
                squaresSet = 0;

                foreach (var square in group)
                {
                    var possibleValues = new List<int>(square.Value.PossibleValues);

                    foreach (var otherSquare in group)
                    {
                        if (otherSquare.Key.RowCoordinate == square.Key.RowCoordinate &&
                            otherSquare.Key.ColumnCoordinate == square.Key.ColumnCoordinate) continue;
                        foreach (var value in otherSquare.Value.PossibleValues)
                        {
                            if (possibleValues.Contains(value))
                            {
                                possibleValues.Remove(value);
                            }
                        }
                    }

                    if (possibleValues.Count != 1) continue;
                    square.Value.Value = possibleValues[0];
                    hasValueBeenSet = true;
                    RemovePossibleValueFromNeighbors(grid, square);

                    squaresSet++;
                }
            } while (squaresSet > 0);

            return hasValueBeenSet;
        }
    }
}