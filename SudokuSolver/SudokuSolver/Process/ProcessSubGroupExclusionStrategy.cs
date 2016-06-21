using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that implements the SubGroup Exclusion Strategy.
    /// </summary>
    public sealed class ProcessSubGroupExclusionStrategy : StrategyProcessor
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
        public override int Process(Grid grid)
        {
            int changeCount;
            var gridSideLength = Convert.ToInt32(Math.Sqrt(grid.Count));

            do
            {
                changeCount = 0;

                for (var i = 0; i < gridSideLength; i += 3)
                {
                    for (var j = 0; j < gridSideLength; j += 3)
                    {
                        var region = grid.GetRegionByCoordinate(new Coordinate(i, j));

                        changeCount += ProcessRow(grid, region, i);
                        changeCount += ProcessColumn(grid, region, j);
                    }
                }
            } while (changeCount > 0);
            return changeCount;
        }

        /// <summary>
        ///     Method that executes the SubGroup Exclusion Strategy rows in a given region.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="region">
        ///     The current Region.
        /// </param>
        /// <param name="rowCoordinate">
        ///     The row's coordinate
        /// </param>
        /// <returns>
        ///     An int indicating whether the method changed any values.
        /// </returns>
        public int ProcessRow(Grid grid, Grid region, int rowCoordinate)
        {
            var changeCount = 0;

            for (var k = 0; k < 3; k++)
            {
                var rowSubGroup = region.GetRowByRowCoordinate(rowCoordinate + k);
                var hasChangedValues = Process(region, rowSubGroup, rowCoordinate + k);


                if (hasChangedValues)
                {
                    changeCount++;
                }
            }

            return changeCount;
        }

        /// <summary>
        ///     Method that executes the SubGroup Exclusion Strategy columns in a given region.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="region">
        ///     The current Region.
        /// </param>
        /// <param name="columnCoordinate">
        ///     The column's coordinate
        /// </param>
        /// <returns>
        ///     An int indicating whether the method changed any values.
        /// </returns>
        public int ProcessColumn(Grid grid, Grid region, int columnCoordinate)
        {
            var changeCount = 0;

            for (var k = 0; k < 3; k++)
            {
                var rowSubGroup = region.GetColumnByColumnCoordinate(columnCoordinate + k);
                var hasChangedValues = Process(region, rowSubGroup, columnCoordinate + k);


                if (hasChangedValues)
                {
                    changeCount++;
                }
            }

            return changeCount;
        }

        /// <summary>
        ///     Method that executes the SubGroup Exclusion Strategy Algorithm.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="group">
        ///     The current group to solve for eg. Row, Column, or Region
        /// </param>
        /// <param name="coordinate">
        ///     An int indicating the current coordinate.
        /// </param>
        /// <returns>
        ///     A bool that indicates whether the method changed any values.
        /// </returns>
        private bool Process(Grid grid, Dictionary<Coordinate, Square> group, int coordinate)
        {
            var changeCount = 0;
            var nonSubGroup = grid.Except(group);
            var possibleValues = new List<int>(group.Values.SelectMany(rv => rv.PossibleValues).Distinct());

            foreach (var square in nonSubGroup)
            {
                foreach (var value in square.Value.PossibleValues)
                {
                    if (possibleValues.Contains(value))
                    {
                        possibleValues.Remove(value);
                    }
                }
            }

            if (possibleValues.Count > 0)
            {
                var row = grid.GetRowByRowCoordinate(coordinate).Except(group);

                var keyValuePairs = row as IList<KeyValuePair<Coordinate, Square>> ?? row.ToList();
                foreach (var value in possibleValues)
                {
                    foreach (var square in keyValuePairs)
                    {
                        if (!square.Value.PossibleValues.Contains(value)) continue;
                        square.Value.RemovePossibleValue(value);
                        changeCount++;
                    }

                    if (group.Count(pv => pv.Value.PossibleValues.Contains(value)) == 1)
                    {
                        var square =
                            group.SingleOrDefault(pv => pv.Value.PossibleValues.Contains(value));

                        square.Value.Value = value;
                        RemovePossibleValueFromNeighbors(grid, square);
                    }
                }
            }

            return changeCount != 0;
        }

        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> @group)
        {
            throw new NotImplementedException();
        }
    }
}