using System.Collections.Generic;

namespace SudokuSolver.Business
{
    /// <summary>
    ///     Square class used to represent a Sudoku square with its value or possible values.
    /// </summary>
    public class Square
    {
        private int value;

        /// <summary>
        ///     Constructor used to instantiate a Square
        /// </summary>
        /// <param name="value">
        ///     An int used to instantiate a Square
        /// </param>
        public Square(int value)
        {
            this.value = value;

            PossibleValues = value == 0 ? new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9} : new List<int>();
        }

        /// <summary>
        ///     An int reprsenting the value of a Sudoku Square.
        /// </summary>
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PossibleValues.Clear();
            }
        }

        /// <summary>
        ///     A List of type int used to store the possible values of a Square.
        /// </summary>
        public List<int> PossibleValues { get; private set; }

        /// <summary>
        ///     An int indicating the possible values of a Square.
        /// </summary>
        public int PossibleValuesCount => PossibleValues.Count;

        /// <summary>
        ///     Method that removes a possible value from a Square.
        /// </summary>
        /// <param name="possibleValue">
        ///     An int to be removed from the List of possible values.
        /// </param>
        public void RemovePossibleValue(int possibleValue)
        {
            PossibleValues.Remove(possibleValue);
        }
    }
}
