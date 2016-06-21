using NUnit.Framework;
using SudokuSolver.Business;

namespace TestSudokuSolver.Business
{
    [TestFixture]
    public class TestCoordinate
    {
        [Test]
        public void Test()
        {
            var coordinate = new Coordinate(1, 5); 

            Assert.AreEqual(1, coordinate.RowCoordinate);
            Assert.AreEqual(5,coordinate.ColumnCoordinate);
        }
    }
}
