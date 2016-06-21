using System;
using System.Collections.Generic;

namespace SudokuSolver.Business
{
    /// <summary>
    ///     Coordinate class used to represent an object's location in a Grid.
    /// </summary>
    public class Coordinate
    {
        private readonly Tuple<int, int> coordinate;

        /// <summary>
        ///     Constructor used to instantiate the Coordinate object.
        /// </summary>
        /// <param name="rowCoordinate">
        ///     An int that represents the row coordinate.
        /// </param>
        /// <param name="columnCoordinate">
        ///     An int that represents the column coordinate.
        /// </param>
        public Coordinate(int rowCoordinate, int columnCoordinate)
        {
            coordinate = new Tuple<int, int>(rowCoordinate, columnCoordinate);
        }

        /// <summary>
        ///     Returns an integer that represents the row number
        /// </summary>
        public int RowCoordinate => coordinate.Item1;

        /// <summary>
        ///     Returns an integer that represents the column number
        /// </summary>
        public int ColumnCoordinate => coordinate.Item2;
    }
}
