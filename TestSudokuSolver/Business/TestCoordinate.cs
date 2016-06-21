using NUnit.Framework;
using SudokuSolver.Business;

namespace TestSudokuSolver.Business
{
    /// <summary>
    ///     Class that tests the Coordinate class.
    /// </summary>
    [TestFixture]
    public class TestCoordinate
    {
        /// <summary>
        ///     Method that tests Coordinate instantiation and retrieval
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var coordinate = new Coordinate(1, 5); 

            Assert.AreEqual(1, coordinate.RowCoordinate);
            Assert.AreEqual(5,coordinate.ColumnCoordinate);
        }
    }
}
