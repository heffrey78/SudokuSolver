using System.Collections.Generic;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that instantiates and returns StrategyProcessors
    /// </summary>
    public class StrategyProcessorFactory
    {
        /// <summary>
        ///     Method that returns a List of IStrategyProcessor.
        /// </summary>
        /// <returns>
        ///     A List of IStrategyProcessor.
        /// </returns>
        public static List<IStrategyProcessor> GetStrategyProcessors()
        {
            var processorList = new List<IStrategyProcessor>();
            processorList.Add(new ProcessOneChoiceStrategy());
            processorList.Add(new ProcessSinglePossibilityStrategy());
            processorList.Add(new ProcessSubGroupExclusionStrategy());
            processorList.Add(new ProcessChainTripleStrategy());

            return processorList;
        }

        /// <summary>
        ///     Method that returns an IStrategyProcessor implementation of type ProcessInitilization.
        /// </summary>
        /// <returns>
        ///     An IStrategyProcessor implementation of type ProcessInitilization.
        /// </returns>
        public static IStrategyProcessor GetInitilizationProcessor()
        {
            return new ProcessInitialization();
        }
    }
}
