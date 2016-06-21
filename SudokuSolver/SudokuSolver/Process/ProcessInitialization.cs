using System.Collections.Generic;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    public class ProcessInitialization : StrategyProcessor
    {
        public override int Process(Grid grid)
        {
            foreach (var square in grid)
            {
                if (square.Value.Value != 0)
                {
                    RemovePossibleValueFromNeighbors(grid, square);
                }
            }

            return 0;
        }

        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> @group)
        {
            throw new System.NotImplementedException();
        }
    }
}