using System.Collections.Generic;

namespace SudokuSolver.Business
{
    /// <summary>
    ///     Coordinate class used to represent an object's location
    /// </summary>
    public class Coordinate
    {
        private KeyValuePair<int, int> coordinate;

        /// <summary>
        ///     Constructor used to instantiate the Coordinate object
        /// </summary>
        /// <param name="rowCoordinate">
        ///     An integer representing the row coordinate
        /// </param>
        /// <param name="columnCoordinate">
        ///     An integer representing the column coordinate
        /// </param>
        public Coordinate(int rowCoordinate, int columnCoordinate)
        {
            coordinate = new KeyValuePair<int, int>(rowCoordinate, columnCoordinate);
        }

        /// <summary>
        ///     Returns an integer that represents the row number
        /// </summary>
        public int RowCoordinate => coordinate.Key;

        /// <summary>
        ///     Returns an integer that represents the column number
        /// </summary>
        public int ColumnCoordinate => coordinate.Value;
    }
}
