using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Business;
using System;

namespace SudokuSolver.Process
{
    public abstract class StrategyProcessor : IStrategyProcessor
    {
        /// <summary>
        ///     Method used to execute a Strategy.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to Process.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of changes.
        /// </returns>
        public virtual int Process(Grid grid)
        {
            int changeCount;
            var strategyUses = 0;

            do
            {
                changeCount = 0;
                changeCount += ProcessRows(grid);
                changeCount += ProcessColumns(grid);
                changeCount += ProcessRegions(grid);
                strategyUses += changeCount;
            } while (changeCount > 0);

            return strategyUses;
        }

        /// <summary>
        ///     Method that executes a Strategy for rows.
        /// </summary>
        /// <param name="grid">
        ///     The grid to solve.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of changed rows.
        /// </returns>
        public int ProcessRows(Grid grid)
        {
            var rowCount = Convert.ToInt32(Math.Sqrt(grid.Count));
            var changedRows = 0;

            for (var i = 0; i < rowCount; i++)
            {
                var hasChangedValues = Process(grid, grid.GetRowByRowCoordinate(i));

                if (hasChangedValues)
                {
                    changedRows++;
                }
            }

            return changedRows;
        }

        /// <summary>
        ///     Method that executes a Strategy for columns.
        /// </summary>
        /// <param name="grid">
        ///     The grid to solve
        /// </param>
        /// <returns>
        ///     An int that indicates the number of changed columns
        /// </returns>
        public virtual int ProcessColumns(Grid grid)
        {
            var rowCount = Convert.ToInt32(Math.Sqrt(grid.Count));
            var changedRows = 0;

            for (var i = 0; i < rowCount; i++)
            {
                var hasChangedValues = Process(grid, grid.GetColumnByColumnCoordinate(i));

                if (hasChangedValues)
                {
                    changedRows++;
                }
            }

            return changedRows;
        }

        /// <summary>
        ///     Method that executes a Strategy for columns.
        /// </summary>
        /// <param name="grid">
        ///     The grid to solve.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of changed regions.
        /// </returns>
        public int ProcessRegions(Grid grid)
        {
            var gridSize = Convert.ToInt32(Math.Sqrt(grid.Count));
            var changedRegions = 0;

            for (var i = 0; i < gridSize; i += 3)
            {
                for (var j = 0; j < gridSize; j += 3)
                {
                    var coordinate = new Coordinate(i, j);
                    var hasChangedValues = Process(grid,
                        grid.GetRegionByCoordinate(coordinate).ToDictionary(kv => kv.Key, kv => kv.Value));

                    if (hasChangedValues)
                    {
                        changedRegions++;
                    }
                }
            }

            return changedRegions;
        }

        /// <summary>
        ///     Utility method used to remove a value from the PossibleValues collection of all the given square's neighbors
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="square">
        ///     The Square whose value should be removed from its neighbors' PossibleValues
        /// </param>
        public void RemovePossibleValueFromNeighbors(Grid grid, KeyValuePair<Coordinate, Square> square)
        {
            var squares = grid.GetRowByRowCoordinate(square.Key.RowCoordinate);

            foreach (var otherSquare in squares)
            {
                otherSquare.Value.RemovePossibleValue(square.Value.Value);
            }

            squares = grid.GetColumnByColumnCoordinate(square.Key.ColumnCoordinate);

            foreach (var otherSquare in squares)
            {
                otherSquare.Value.RemovePossibleValue(square.Value.Value);
            }

            squares = grid.GetRegionByCoordinate(new Coordinate(square.Key.RowCoordinate, square.Key.ColumnCoordinate)).ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var otherSquare in squares)
            {
                otherSquare.Value.RemovePossibleValue(square.Value.Value);
            }
        }

        /// <summary>
        ///     Abstract method used to execute a Strategy Alogrithm.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="group">
        ///     The current group to solve for eg. Row, Column, or Region.
        /// </param>
        /// <returns>
        ///     An int indicating whether the method changed a value.
        /// </returns>
        protected abstract bool Process(Grid grid, Dictionary<Coordinate, Square> group);
    }
}