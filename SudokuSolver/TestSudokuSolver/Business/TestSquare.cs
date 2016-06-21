using NUnit.Framework;
using SudokuSolver.Business;

namespace TestSudokuSolver.Business
{
    [TestFixture]
    public class TestSquare
    {
        [Test]
        public void TestEmptySquare()
        {
            var square = new Square(0);

            Assert.IsTrue( square.PossibleValuesCount == 9);
        }

        [Test]
        public void TestFilledSquare()
        {
            var square = new Square(1);

            Assert.IsTrue(square.PossibleValuesCount == 0);
        }

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
