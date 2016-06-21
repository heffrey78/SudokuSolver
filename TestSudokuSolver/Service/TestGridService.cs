using System;
using System.IO;
using NUnit.Framework;
using SudokuSolver;
using SudokuSolver.Service;
using TestSudokuSolver.Utility;

namespace TestSudokuSolver.Service
{
    /// <summary>
    ///     Class that tests the GridService
    /// </summary>
    [TestFixture]
    public class TestGridService
    {
        /// <summary>
        ///     Method that tests the LoadGrid method.
        /// </summary>
        [Test]
        public void TestLoadGrid()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzle1.txt";

            var grid = GridService.LoadGrid(inputFilePath);

            foreach (var unit in grid)
            {
                if (unit.Value.Value == 0)
                {
                    Assert.IsTrue(unit.Value.PossibleValuesCount == 9);
                }
                else
                {
                    Assert.IsTrue(unit.Value.PossibleValuesCount == 0);
                }
            }
        }

        /// <summary>
        ///     Method that tests FileNotFoundException handling for LoadGrid
        /// </summary>
        [Test]
        public void TestLoadGridException()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzle100.txt";

            var ex = Assert.Throws<FileNotFoundException>(() => GridService.LoadGrid(inputFilePath));

            Assert.IsTrue(ex.Message.Contains("The file:"));
        }

        /// <summary>
        ///     Method that tests ValidationException when a file with an incorrect
        ///     number of rows is loaded.
        /// </summary>
        [Test]
        public void TestLoadGridRowCountException()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleEightRows.txt";

            var ex = Assert.Throws<ValidationException>(() => GridService.LoadGrid(inputFilePath));

            Assert.IsTrue(ex.Message.Contains("Grid must have nine rows."));
        }

        /// <summary>
        ///     Method that tests ValidationException when a file with an incorrect
        ///     number of columns is loaded.
        /// </summary>
        [Test]
        public void TestLoadGridBadColumn()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleBadColumn.txt";

            var ex = Assert.Throws<ValidationException>(() => GridService.LoadGrid(inputFilePath));

            Assert.IsTrue(ex.Message.Contains("Row must have nine columns."));
        }

        /// <summary>
        ///     Method that tests ValidationException when a file with an incorrect
        ///     character is loaded.
        /// </summary>
        [Test]
        public void TestLoadGridBadSquare()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleBadSquare.txt";

            var ex = Assert.Throws<ValidationException>(() => GridService.LoadGrid(inputFilePath));

            Assert.IsTrue(ex.Message.Contains("Square contains invalid character."));
        }

        /// <summary>
        ///     Method that tests the SaveGrid method.
        /// </summary>
        [Test]
        public void TestSaveGrid()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzle1.txt";
            var outputFilePath = TestUtility.GetLocalPath() + @"\output.txt";

            var grid = GridService.LoadGrid(inputFilePath);
            GridService.SaveGrid(outputFilePath, grid);

            Assert.IsTrue(File.Exists(outputFilePath));

            var outputFileContents = File.ReadAllText(outputFilePath);
            Assert.IsTrue(outputFileContents.Length == 97);

            File.Delete(outputFilePath);
        }

        /// <summary>
        ///     Method that tests exception handling for SaveGrid.
        /// </summary>
        [Test]
        public void TestSaveGridException()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzle1.txt";
            var outputFilePath = TestUtility.GetLocalPath() + @"\out*put.txt";

            var grid = GridService.LoadGrid(inputFilePath);
            var ex = Assert.Throws<Exception>(() => GridService.SaveGrid(outputFilePath, grid));

            Assert.IsTrue(ex.Message.Contains("Error Saving File."));
        }
    }
}
