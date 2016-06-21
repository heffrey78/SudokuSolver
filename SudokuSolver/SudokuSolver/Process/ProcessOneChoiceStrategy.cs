using SudokuSolver.Business;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that implements the One Choice Strategy.
    /// </summary>
    public class ProcessOneChoiceStrategy : StrategyProcessor
    {
        /// <summary>
        ///     Method that contains the One Choice Strategy Algorithm
        /// </summary>
        ///     The Grid to solve.
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="dictionary">
        ///     The dictionary to be processed.
        /// </param>
        /// <returns>
        ///     A bool indicating whether a value has been set.
        /// </returns>
        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> dictionary)
        {
            var takenValues = dictionary.Values.Where(v => v.Value != 0).Select(v => v.Value).ToList();
            var hasValueBeenSet = false;
            int squaresSet;

            do
            {
                squaresSet = 0;

                foreach (var square in dictionary)
                {
                    foreach (var value in takenValues)
                    {
                        square.Value.RemovePossibleValue(value);
                    }

                    if (square.Value.PossibleValuesCount != 1) continue;
                    square.Value.Value = square.Value.PossibleValues[0];
                    RemovePossibleValueFromNeighbors(grid, square);
                    takenValues.Add(square.Value.Value);
                    hasValueBeenSet = true;
                    squaresSet++;
                }

            } while (squaresSet > 0);

            return hasValueBeenSet;
        }
    }
}
