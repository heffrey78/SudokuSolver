using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using SudokuSolver.Service;
using TestSudokuSolver.Utility;

namespace TestSudokuSolver.Service
{
    [TestFixture]
    public class TestGridService
    {
        [Test]
        public void TestLoadGrid()
        {
            const string inputFilePath = @"\puzzle1.txt";

            try
            {
                var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);

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
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestSaveGrid()
        {
            const string inputFilePath = @"\puzzle1.txt";
            const string outputFilePath = "output.txt";

            var grid = GridService.LoadGrid(TestUtility.GetLocalPath() + inputFilePath);
            GridService.SaveGrid(outputFilePath, grid);

            Assert.IsTrue(File.Exists(outputFilePath));

            var outputFileContents = File.ReadAllText(outputFilePath);
            Assert.IsTrue(outputFileContents.Length == 97);

            File.Delete(outputFilePath);
        }
    }
}
