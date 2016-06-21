using NUnit.Framework;
using SudokuSolver.Business;

namespace TestSudokuSolver.Business
{
    [TestFixture]
    public class TestGrid
    {
        [Test]
        public void TestGetRowByRowCoordinate()
        {
            var grid = new Grid();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var square = new Square(i+1);
                    grid.Add(coordinate,square);
                }
            }

            var row = grid.GetRowByRowCoordinate(0);

            foreach (var unit in row)
            {
                Assert.IsTrue(unit.Value.Value == 1);
                Assert.IsTrue(unit.Key.RowCoordinate == 0);
            }
        }

        [Test]
        public void TestGetColumnByRowCoordinate()
        {
            var grid = new Grid();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var square = new Square(i + 1);
                    grid.Add(coordinate, square);
                }
            }

            var column = grid.GetColumnByColumnCoordinate(1);
            var counter = 1;

            foreach (var unit in column)
            {
                Assert.IsTrue(unit.Value.Value == counter);
                Assert.IsTrue(unit.Key.ColumnCoordinate == 1);
                counter++;
            }
        }

        [Test]
        public void TestGetRegionByCoordinate()
        {
            var grid = new Grid();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var square = new Square(i + 1);
                    grid.Add(coordinate, square);
                }
            }

            var region = grid.GetRegionByCoordinate(new Coordinate(3, 2));

            for (var i = 3; i < 6; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Assert.IsTrue(region.ContainsKey(new Coordinate(i, j)));
                }
            }
        }

        [Test]
        public void TestToString()
        {
            var grid = new Grid();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var square = new Square(i + 1);
                    grid.Add(coordinate, square);
                }
            }

            var gridString = grid.ToString();

            // 81 characters for the grid and 16 characters for new lines
            Assert.IsTrue(gridString.Length == 97);
        }
    }
}
