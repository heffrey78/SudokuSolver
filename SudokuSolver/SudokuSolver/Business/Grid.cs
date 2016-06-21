using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Business
{
    /// <summary>
    ///     Grid class that is a Dictionary of Coordinate, Square used to represent
    ///     a Sudoku grid
    /// </summary>
    public class Grid : IDictionary<Coordinate, Square>
    {
        private Dictionary<Coordinate, Square> backingDictionary;

        public ICollection<Coordinate> Keys => backingDictionary.Keys;

        public ICollection<Square> Values => backingDictionary.Values;

        public int Count => backingDictionary.Count;

        public bool IsReadOnly => false;

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

        public Grid()
        {
            backingDictionary = new Dictionary<Coordinate, Square>();
        }

        public bool ContainsKey(Coordinate key)
        {
            return
                backingDictionary
                    .Any(b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate);
        }

        public void Add(Coordinate key, Square value)
        {
            backingDictionary.Add(key, value);
        }

        public bool Remove(Coordinate key)
        {
            return backingDictionary.Remove(key);
        }

        public bool TryGetValue(Coordinate key, out Square value)
        {
            value =
                backingDictionary.SingleOrDefault(
                    b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate).Value;

            return
                backingDictionary
                    .Any(b => b.Key.RowCoordinate == key.RowCoordinate && b.Key.ColumnCoordinate == key.ColumnCoordinate);
        }

        public void Add(KeyValuePair<Coordinate, Square> item)
        {
            backingDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            backingDictionary.Clear();
        }

        public bool Contains(KeyValuePair<Coordinate, Square> item)
        {
            return backingDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<Coordinate, Square>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Coordinate, Square> item)
        {
            return backingDictionary.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Coordinate, Square>> GetEnumerator()
        {
            return backingDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Returns a Dictionary of Coordinate, Square representing a Row
        ///     in a Sudoku grid
        /// </summary>
        /// <param name="rowCoordinate">
        ///     Integer representing the Row Coordinate
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
        ///     Returns a Dictionary of Coordinate, Square representing a Column
        ///     in a Sudoku grid
        /// </summary>
        /// <param name="columnCoordinate">
        ///     Integer representing the Column Coordinate
        /// </param>
        /// <returns>
        ///     A Dictionary representing a Grid Column
        /// </returns>
        public Dictionary<Coordinate, Square> GetColumnByColumnCoordinate(int columnCoordinate)
        {
            return backingDictionary.Where(v => v.Key.ColumnCoordinate == columnCoordinate)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public Grid GetRegionByCoordinate(Coordinate coordiante)
        {
            var region = new Grid();
            var sideLength = Convert.ToInt32(Math.Sqrt(backingDictionary.Count));
            var regionRowCoordinateOrigin = Convert.ToInt32(Math.Abs(3 - Math.Ceiling((sideLength - coordiante.RowCoordinate) / 3f)));
            var regionColumnCoordinateOrigin = Convert.ToInt32(Math.Abs(3 - Math.Ceiling((sideLength - coordiante.ColumnCoordinate) / 3f)));

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
        ///     Overridden method that returns a Grid
        /// </summary>
        /// <returns>
        ///     A string representing the Grid
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
                    builder.Append(row.ToString());
                }
            }

            return builder.ToString();
        }

        public string PossibleValuesToString()
        {
            var builder = new StringBuilder();
            var sideLength = Convert.ToInt32(Math.Sqrt(backingDictionary.Count));

            for (var i = 0; i < sideLength; i++)
            {
                var row = new StringBuilder();

                for (var j = 0; j < sideLength; j++)
                {
                    row.Append(string.Join(",", backingDictionary.SingleOrDefault(
                        b => b.Key.RowCoordinate == i && b.Key.ColumnCoordinate == j).Value.PossibleValues.ToArray()) + @"|");
                }

                // if not the last row use AppendLine
                if (i != sideLength - 1)
                {
                    builder.AppendLine(row.ToString());
                }
                else
                {
                    builder.Append(row.ToString());
                }
            }

            return builder.ToString();
        }
    }
}
