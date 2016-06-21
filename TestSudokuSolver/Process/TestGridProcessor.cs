using NUnit.Framework;
using System.IO;
using System.Linq;
using SudokuSolver.Business;
using SudokuSolver.Process;
using SudokuSolver.Service;
using TestSudokuSolver.Utility;

namespace TestSudokuSolver.Process
{
    /// <summary>
    ///     Class that tests the Sudoku strategies.
    /// </summary>
    [TestFixture]
    public class TestGridProcessor
    {
        /// <summary>
        ///     Method that tests the OneChoice Strategy.
        /// </summary>
        [Test]
        public void TestProcessOneChoice()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleOneChoice.txt";
            var grid = GridService.LoadGrid(inputFilePath);

            StrategyProcessor preProcessor = new ProcessInitialization();
            preProcessor.Process(grid);

            StrategyProcessor processor = new ProcessOneChoiceStrategy();
            processor.ProcessRows(grid);

            var changedCoordinated = new Coordinate(1,0);
            var changedSquare = grid[changedCoordinated];
            Assert.IsTrue(changedSquare.Value == 4);

            // One pass of this strategy does not completely solve the puzzle
            var validationProcessor = new ProcessGridValidation();
            Assert.IsTrue(validationProcessor.Process(grid) != 0);
        }

        /// <summary>
        ///     Method that tests the Single Possibility Strategy.
        /// </summary>
        [Test]
        public void TestProcessSinglePossibility()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleSinglePossibility.txt";
            var grid = GridService.LoadGrid(inputFilePath);

            StrategyProcessor processor = new ProcessInitialization();
            processor.Process(grid);

            processor = new ProcessSinglePossibilityStrategy();
            processor.ProcessRows(grid);
            processor.ProcessColumns(grid);
            processor.ProcessRegions(grid);

            var square = grid[new Coordinate(3, 0)];

            Assert.IsTrue(square.Value == 7);

            var validationProcessor = new ProcessGridValidation();
            Assert.IsTrue(validationProcessor.Process(grid) == 0);
        }

        /// <summary>
        ///     Method that tets the SubGroup Exclusion Strategy.
        /// </summary>
        [Test]
        public void TestProcessSubGroupExclusion()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzleSubGroup.txt";
            var grid = GridService.LoadGrid(inputFilePath);

            StrategyProcessor processor = new ProcessSubGroupExclusionStrategy();
            processor.Process(grid);

            var row = grid.GetRowByRowCoordinate(5).Skip(3);

            foreach (var square in row)
            {
                if (square.Key.ColumnCoordinate != 3 && square.Key.ColumnCoordinate != 4 &&
                    square.Key.RowCoordinate != 5)
                {
                    Assert.IsTrue(!square.Value.PossibleValues.Contains(5));
                }
            }

            // One pass of this strategy does not completely solve the puzzle,
            // but instead removes possibilities.
            var validationProcessor = new ProcessGridValidation();
            Assert.IsTrue(validationProcessor.Process(grid) != 0);
        }

        /// <summary>
        ///     Method that tests the Chain Triple Strategy for a Row.
        /// </summary>
        [Test]
        public void TestProcessChainTripleRow()
        {
            var inputFilePath = TestUtility.GetLocalPath() + @"\puzzle5.txt";

            var grid = GridService.LoadGrid(inputFilePath);

            IStrategyProcessor processor = new ProcessInitialization();
            processor.Process(grid);

            int changeCount;

            do
            {
                changeCount = 0;
                processor = new ProcessOneChoiceStrategy();
                changeCount += processor.Process(grid);
                processor = new ProcessSinglePossibilityStrategy();
                changeCount += processor.Process(grid);
                processor = new ProcessSubGroupExclusionStrategy();
                changeCount += processor.Process(grid);
                processor = new ProcessChainTripleStrategy();
                changeCount += processor.Process(grid);

            } while (changeCount > 0);

            var validationProcessor = new ProcessGridValidation();

            Assert.IsTrue(validationProcessor.Process(grid) == 0);
        }

        /// <summary>
        ///     Method that tests the GridProcessor Process method.
        /// </summary>
        [Test]
        public void TestProcess()
        {
            var inputFilePath = TestUtility.GetLocalPath()  + @"\puzzle5.txt";
            var outputFilePath = TestUtility.GetLocalPath() + @"\puzzle5.sln.txt";

            var gridProcessor = GridProcessorFactory.GetGridProcessor();
            gridProcessor.Process(inputFilePath);

            var grid = GridService.LoadGrid(outputFilePath);

            var validationProcessor = new ProcessGridValidation();

            Assert.IsTrue(validationProcessor.Process(grid) == 0);

            File.Delete(outputFilePath);
        }
    }
}
