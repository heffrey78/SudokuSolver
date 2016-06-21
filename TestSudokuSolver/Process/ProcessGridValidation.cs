using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Business;

namespace TestSudokuSolver.Process
{
    /// <summary>
    ///     Class that inherits from StrategyProcessor used to validate the contents
    ///     of a solved grid.
    /// </summary>
    public class ProcessGridValidation
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
            var incorrectCount = 0;
            incorrectCount += ProcessRows(grid);
            incorrectCount += ProcessColumns(grid);
            incorrectCount += ProcessRegions(grid);

            return incorrectCount;
        }

        /// <summary>
        ///     Method that counts incorrect Rows.
        /// </summary>
        /// <param name="grid">
        ///     The grid to validate.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of incorrect rows.
        /// </returns>
        public int ProcessRows(Grid grid)
        {
            var rowCount = Convert.ToInt32(Math.Sqrt(grid.Count));
            var incorrectRows = 0;

            for (var i = 0; i < rowCount; i++)
            {
                var isRowCorrect = Process(grid, grid.GetRowByRowCoordinate(i));

                if (!isRowCorrect)
                {
                    incorrectRows++;
                }
            }

            return incorrectRows;
        }

        /// <summary>
        ///     Method that counts incorrect Columns.
        /// </summary>
        /// <param name="grid">
        ///     The grid to validate.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of inccorect columns
        /// </returns>
        public virtual int ProcessColumns(Grid grid)
        {
            var columnCount = Convert.ToInt32(Math.Sqrt(grid.Count));
            var incorrectColumns = 0;

            for (var i = 0; i < columnCount; i++)
            {
                var isColumnCorrect = Process(grid, grid.GetColumnByColumnCoordinate(i));

                if (!isColumnCorrect)
                {
                    incorrectColumns++;
                }
            }

            return incorrectColumns;
        }

        /// <summary>
        ///     Method that counts incorrect Regions.
        /// </summary>
        /// <param name="grid">
        ///     The grid to validate.
        /// </param>
        /// <returns>
        ///     An int that indicates the number of incorrect regions.
        /// </returns>
        public int ProcessRegions(Grid grid)
        {
            var gridSize = Convert.ToInt32(Math.Sqrt(grid.Count));
            var incorrectRegions = 0;

            for (var i = 0; i < gridSize; i += 3)
            {
                for (var j = 0; j < gridSize; j += 3)
                {
                    var coordinate = new Coordinate(i, j);
                    var isRegionCorrect = Process(grid,
                        grid.GetRegionByCoordinate(coordinate).ToDictionary(kv => kv.Key, kv => kv.Value));

                    if (!isRegionCorrect)
                    {
                        incorrectRegions++;
                    }
                }
            }

            return incorrectRegions;
        }

        /// <summary>
        ///     Method that adds up the current group's values to assure they are correct.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to validate.
        /// </param>
        /// <param name="group">
        ///     The current group to validate for eg. Row, Column, or Region
        /// </param>
        /// <returns>
        ///     A bool that indicates the assertions are true.
        /// </returns>
        protected bool Process(Grid grid, Dictionary<Coordinate, Square> @group)
        {
            return group.Sum(kv => kv.Value.Value) == 45;
        }
    }
}