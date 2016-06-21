using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Business;
using SudokuSolver.Process;
using SudokuSolver.Service;
using TestSudokuSolver.Utility;

namespace TestSudokuSolver.Process
{
    [TestFixture]
    public class TestGridProcessor
    {
        [Test]
        public void TestProcessOneChoice()
        {
            const string inputFilePath = @"\puzzle1.txt";
            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);

            StrategyProcessor processor = new ProcessOneChoiceStrategy();
            var changeCount = processor.ProcessRows(grid);

            foreach (var square in grid)
            {
                if (square.Value.Value == 0)
                {
                    Assert.IsTrue(square.Value.PossibleValuesCount > 0);
                }
                else
                {
                    Assert.IsTrue(square.Value.PossibleValuesCount == 0);
                }
            }
        }

        [Test]
        public void TestProcessSinglePossibility()
        {
            const string inputFilePath = @"\puzzleOneChoice.txt";

            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);

            StrategyProcessor processor = new ProcessInitialization();

            processor.Process(grid);

            processor = new ProcessSinglePossibilityStrategy();
            processor.ProcessRows(grid);
            processor.ProcessColumns(grid);
            processor.ProcessRegions(grid);

            var square = grid[new Coordinate(3, 0)];

            Assert.IsTrue(square.Value == 7);
        }

        [Test]
        public void TestProcessSubGroupExclusion()
        {
            const string inputFilePath = @"\puzzleSubGroup.txt";

            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);
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
        }

        [Test]
        public void TestProcessChainTripleRow()
        {
            const string inputFilePath = @"\puzzle5.txt";

            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);

            IStrategyProcessor processor = new ProcessInitialization();
            processor.Process(grid);

            var changeCount = 0;

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

            var row = grid.GetRowByRowCoordinate(4);

            /// VALIDATE METHOD
        }

        [Test]
        public void TestProcess()
        {
            const string inputFilePath = @"\puzzle5.txt";

            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);
            var processor = new GridProcessor();

            processor.Process(grid);

            var gridString = grid.ToString();
            var possibleString = grid.PossibleValuesToString();
        }
    }
}
