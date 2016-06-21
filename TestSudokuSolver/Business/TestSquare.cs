using NUnit.Framework;
using SudokuSolver.Business;

namespace TestSudokuSolver.Business
{
    /// <summary>
    ///     Class that tests the Square class.
    /// </summary>
    [TestFixture]
    public class TestSquare
    {
        /// <summary>
        ///     Tests that a Square without a Value has 
        ///     9 possible values.
        /// </summary>
        [Test]
        public void TestEmptySquare()
        {
            var square = new Square(0);

            Assert.IsTrue( square.PossibleValuesCount == 9);
        }

        /// <summary>
        ///     Tests that a Square with a Value has
        ///     no possible values left.
        /// </summary>
        [Test]
        public void TestFilledSquare()
        {
            var square = new Square(1);

            Assert.IsTrue(square.PossibleValuesCount == 0);
        }

        /// <summary>
        ///     Tests that setting a Value works and that
        ///     it removes possible values.
        /// </summary>
        [Test]
        public void TestValue()
        {
            var square = new Square(0);

            Assert.IsTrue(square.Value == 0);
            Assert.IsTrue(square.PossibleValuesCount == 9);

            square = new Square(8);

            Assert.IsTrue(square.Value == 8);
            Assert.IsTrue(square.PossibleValuesCount == 0);

            square.Value = 5;

            Assert.IsTrue(square.Value == 5);
            Assert.IsTrue(square.PossibleValuesCount == 0);
        }

        /// <summary>
        ///     Tests RemovePossibleValue.
        /// </summary>
        [Test]
        public void TestRemovePossibleValue()
        {
            var square = new Square(0);

            Assert.IsTrue(square.PossibleValuesCount == 9);

            square.RemovePossibleValue(5);

            Assert.IsTrue(!square.PossibleValues.Contains(5));
        }
    }
}
