using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Business
{
    /// <summary>
    ///     Grid class that implements IDictionary of type Coordinate, Square used to represent
    ///     a Sudoku grid.
    /// </summary>
    public class Grid : IDictionary<Coordinate, Square>
    {
        private readonly Dictionary<Coordinate, Square> backingDictionary;

        /// <summary>
        ///     Returns the Dictionary's Collection of Keys.
        /// </summary>
        public ICollection<Coordinate> Keys => backingDictionary.Keys;

        /// <summary>
        ///     Returns the Dictionary's Collection of Values.
        /// </summary>
        public ICollection<Square> Values => backingDictionary.Values;

        /// <summary>
        ///     Returns the Dictionary's Count
        /// </summary>
        public int Count => backingDictionary.Count;

        /// <summary>
        ///     Returns IsReadonly = false.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        ///     Indexer used to return a Square object at the given location in the Dictionary
        /// </summary>
        /// <param name="key">
        ///     A Coordinate object used to return a Square.
        /// </param>
        /// <returns>
        ///     A Square.
        /// </returns>
        public Square this[Coordinate key]
        {
            get
            {
                return
                    backingDictionary.Where(
                        k => k.Key.RowCoordinate == key.RowCoordinate && k.Key.ColumnCoordinate == key.ColumnCoordinate)
                        .Select(v => v.Value)
                        .SingleOrDefault();
            }

            set
            {
                var coordinate = backingDictionary.SingleOrDefault(
                    b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate).Key;

                backingDictionary[coordinate] = value;
            }
        }

        /// <summary>
        ///     Constructor used to instantiate the Grid.
        /// </summary>
        public Grid()
        {
            backingDictionary = new Dictionary<Coordinate, Square>();
        }

        /// <summary>
        ///     Method that indicates whether the Dictionary contains a given Key.
        /// </summary>
        /// <param name="key">
        ///     A Coordinate object passed in as a possible key.
        /// </param>
        /// <returns>
        ///     A bool indicating whether the Dictionary contains the given Key.
        /// </returns>
        public bool ContainsKey(Coordinate key)
        {
            return
                backingDictionary
                    .Any(b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate);
        }

        /// <summary>
        ///     Method that tries to get a value from the Dictionary and returns a bool
        ///     indicating success.
        /// </summary>
        /// <param name="key">
        ///     A Coordinate object used as a Key.
        /// </param>
        /// <param name="value">
        ///     A Square object used as a Value.
        /// </param>
        /// <returns>
        ///     A bool indicating whether the get was successful.
        /// </returns>
        public bool TryGetValue(Coordinate key, out Square value)
        {
            value =
                backingDictionary.SingleOrDefault(
                    b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate).Value;

            return
                backingDictionary
                    .Any(b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate);
        }

        /// <summary>
        ///     Method that adds a new record to the Dictionary.
        /// </summary>
        /// <param name="key">
        ///     A Coordinate object used as a Key.
        /// </param>
        /// <param name="value">
        ///     A Square object used as a Value.
        /// </param>
        public void Add(Coordinate key, Square value)
        {
            backingDictionary.Add(key, value);
        }

        /// <summary>
        ///     Method that adds a new record to the Dictionary.
        /// </summary>
        /// <param name="item">
        ///     A KeyValuePair of type Coordinate, Square to be added to the Dictionary.
        /// </param>
        public void Add(KeyValuePair<Coordinate, Square> item)
        {
            backingDictionary.Add(item.Key, item.Value);
        }

        /// <summary>
        ///     Method that clears the contents of the Dictionary.
        /// </summary>
        public void Clear()
        {
            backingDictionary.Clear();
        }

        /// <summary>
        ///     Method that indicates whether the Dictionary contains a KeyValuePair
        /// </summary>
        /// <param name="item">
        ///     A KeyValuePair of type Coordinate, Square passed in to check for it's 
        ///     existence in the Dictionary.
        /// </param>
        /// <returns>
        ///     A bool indicating the existence of the KeyValuePair in the Dictionary.
        /// </returns>
        public bool Contains(KeyValuePair<Coordinate, Square> item)
        {
            return backingDictionary.Contains(item);
        }

        /// <summary>
        ///     CopyToMethod not implemented
        /// </summary>
        /// <param name="array">
        ///     An array of KeyValuePairs containing the contents of the Dictionary.
        /// </param>
        /// <param name="arrayIndex">
        ///     An int indicating an index in the array.
        /// </param>
        public void CopyTo(KeyValuePair<Coordinate, Square>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Method that removes a KeyValuePair at the Key passed in.
        /// </summary>
        /// <param name="key">
        ///     A Coordinate object used as a Key.
        /// </param>
        /// <returns>
        ///     A bool indicating the success of removal.
        /// </returns>
        public bool Remove(Coordinate key)
        {
            return backingDictionary.Remove(key);
        }

        /// <summary>
        ///     Method that removes a KeyValuePair at the Key passed in.
        /// </summary>
        /// <param name="item">
        ///     KeyValuePair containing the Coordinate used as Key.
        /// </param>
        /// <returns>
        ///     A bool indicating the success of removal.
        /// </returns>
        public bool Remove(KeyValuePair<Coordinate, Square> item)
        {
            return backingDictionary.Remove(item.Key);
        }

        /// <summary>
        ///     Method that returns an IEnumerator object of type KeyValuePair of type Coordinate, Square.
        /// </summary>
        /// <returns>
        ///     IEnumerator object.
        /// </returns>
        public IEnumerator<KeyValuePair<Coordinate, Square>> GetEnumerator()
        {
            return backingDictionary.GetEnumerator();
        }

        /// <summary>
        ///     Method that returns an IEnumerator.
        /// </summary>
        /// <returns>
        ///     IEnumerator object.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     A method that returns a Dictionary of Coordinate, Square representing a Row
        ///     in a Sudoku Grid.
        /// </summary>
        /// <param name="rowCoordinate">
        ///     An int that reprsents a Row Coordinate.
        /// </param>
        /// <returns>
        ///     A Dictionary representing a Grid Row
        /// </returns>
        public Dictionary<Coordinate, Square> GetRowByRowCoordinate(int rowCoordinate)
        {
            return backingDictionary.Where(v => v.Key.RowCoordinate == rowCoordinate)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        ///     Method that returns a Dictionary of Coordinate, Square representing a Column
        ///     in the Sudoku Grid.
        /// </summary>
        /// <param name="columnCoordinate">
        ///     An int that represents a Column Coordinate.
        /// </param>
        /// <returns>
        ///     A Dictionary representing a Grid Column.
        /// </returns>
        public Dictionary<Coordinate, Square> GetColumnByColumnCoordinate(int columnCoordinate)
        {
            return backingDictionary.Where(v => v.Key.ColumnCoordinate == columnCoordinate)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        ///     Method that returns a Grid object representing a region of the Sudoku Grid.
        /// </summary>
        /// <param name="coordiante">
        ///     A Coordinate object representing a location in the region.
        /// </param>
        /// <returns>
        ///     A Grid object representing a Grid region.
        /// </returns>
        public Grid GetRegionByCoordinate(Coordinate coordiante)
        {
            var region = new Grid();
            // Determine the size of the Grid
            var sideLength = Convert.ToInt32(Math.Sqrt(backingDictionary.Count));

            // Find the origin Coordinate of the region based on a Coordinate within that region.
            var regionRowCoordinateOrigin = Convert.ToInt32(Math.Abs(3 - Math.Ceiling((sideLength - coordiante.RowCoordinate) / 3f)));
            var regionColumnCoordinateOrigin = Convert.ToInt32(Math.Abs(3 - Math.Ceiling((sideLength - coordiante.ColumnCoordinate) / 3f)));

            // Loop over the Squares within the boundaries of the region and add them to the new Grid object.
            for (var i = (regionRowCoordinateOrigin * 3); i < ((regionRowCoordinateOrigin + 1) * 3); i++)
            {
                for (var j = (regionColumnCoordinateOrigin * 3); j < ((regionColumnCoordinateOrigin + 1) * 3); j++)
                {
                    var keyvalue = this.SingleOrDefault(v => v.Key.RowCoordinate == i && v.Key.ColumnCoordinate == j);
                    region.Add(keyvalue.Key, keyvalue.Value);
                }
            }

            return region;
        }

        /// <summary>
        ///     Overridden method that returns a string representation of the Grid.
        /// </summary>
        /// <returns>
        ///     A string representing the Grid.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            var sideLength = Convert.ToInt32(Math.Sqrt(backingDictionary.Count));

            for (var i = 0; i < sideLength; i++)
            {
                var row = new StringBuilder();

                for (var j = 0; j < sideLength; j++)
                {
                    row.Append(backingDictionary.SingleOrDefault(
                        b => b.Key.RowCoordinate == i && b.Key.ColumnCoordinate == j).Value.Value);
                }

                // if not the last row use AppendLine
                if (i != sideLength - 1)
                {
                    builder.AppendLine(row.ToString());
                }
                else
                {
                    builder.Append(row);
                }
            }

            return builder.ToString();
        }
    }
}
