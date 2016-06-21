using System.Collections.Generic;

namespace SudokuSolver.Business
{
    public class Square
    {
        private int value;

        public Square(int value)
        {
            this.value = value;

            PossibleValues = value == 0 ? new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9} : new List<int>();
        }

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PossibleValues.Clear();
            }
        }

        public List<int> PossibleValues { get; private set; }

        public int PossibleValuesCount => PossibleValues.Count;

        public void RemovePossibleValue(int possibleValue)
        {
            PossibleValues.Remove(possibleValue);
        }
    }
}
