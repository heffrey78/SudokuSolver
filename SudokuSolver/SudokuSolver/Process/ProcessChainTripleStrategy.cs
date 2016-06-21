using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Business;

namespace SudokuSolver.Process
{
    /// <summary>
    ///     Class that implements the Chain Triple Strategy
    /// </summary>
    public class ProcessChainTripleStrategy : StrategyProcessor
    {
        /// <summary>
        ///     Method that executes the Chain Triple Strategy Algorithm.
        /// </summary>
        /// <param name="grid">
        ///     The Grid to solve.
        /// </param>
        /// <param name="group">
        ///     The dictionary to be processed.
        /// </param>
        /// <returns>
        ///     A bool indicating whether a value has been set.
        /// </returns>
        protected override bool Process(Grid grid, Dictionary<Coordinate, Square> @group)
        {
            var changeCount = 0;
            var candidates = group.Where(pv => pv.Value.PossibleValuesCount == 2 || pv.Value.PossibleValuesCount == 3);
            var chain = new List<KeyValuePair<Coordinate, Square>>();
            var frequencyDictionary = new Dictionary<int, int>();

            // Create a dictionary to determine squares whose possible values
            // occur more than once, enabling a chain
            var keyValuePairs = candidates as IList<KeyValuePair<Coordinate, Square>> ?? candidates.ToList();
            foreach (var candidate in keyValuePairs)
            {
                foreach (var possibileValue in candidate.Value.PossibleValues)
                {
                    if (frequencyDictionary.ContainsKey(possibileValue))
                    {
                        frequencyDictionary[possibileValue]++;
                    }
                    else
                    {
                        frequencyDictionary.Add(possibileValue, 1);
                    }
                }
            }

            // Determine the possible values that only occur once, excluding them from
            // the possiblity of chain inclusion
            var exclusionValues = frequencyDictionary.Where(kv => kv.Value == 1).Select(kv => kv.Key);
            var multipleFrequencyCandidate = new List<KeyValuePair<Coordinate, Square>>();

            // Remove squares that have excluded possible values
            foreach (var exclusion in exclusionValues)
            {
                foreach (var candidate in keyValuePairs)
                {
                    if (!candidate.Value.PossibleValues.Contains(exclusion) &&
                        !multipleFrequencyCandidate.Contains(candidate))
                    {
                        multipleFrequencyCandidate.Add(candidate);
                    }
                }
            }

            // Only attempt to make a chain if there are more than three squares
            if (multipleFrequencyCandidate.Count < 3) return changeCount != 0;
            {
                // Loop over each candidate
                foreach (var candidate in multipleFrequencyCandidate)
                {
                    chain.Clear();
                    chain.Add(candidate);
                    var otherCandidates = multipleFrequencyCandidate.Where(kv => kv.Key != candidate.Key);

                    // Loop over other members of candidate list
                    foreach (var otherCandidate in otherCandidates)
                    {
                        // Determine the number of intersecting values
                        var intersectionCount = candidate.Value.PossibleValues.Intersect(otherCandidate.Value.PossibleValues).Count();

                        // If the squares intersect, add to the chain
                        if ((candidate.Value.PossibleValuesCount + otherCandidate.Value.PossibleValuesCount == 4 &&
                             intersectionCount >= 1) ||
                            (candidate.Value.PossibleValuesCount + otherCandidate.Value.PossibleValuesCount == 5 &&
                             intersectionCount >= 2))
                        {
                            chain.Add(otherCandidate);
                        }
                    }

                    var possibleValues = chain.SelectMany(kv => kv.Value.PossibleValues).Distinct();

                    // The chain's distinct possible values must match the number of squares in order to be valid
                    var enumerable = possibleValues as IList<int> ?? possibleValues.ToList();

                    if (chain.Count != enumerable.Count()) continue;
                    var nonChain = @group.Except(chain);

                    foreach (var square in nonChain)
                    {
                        foreach (var possibleValue in enumerable)
                        {
                            if (square.Value.PossibleValues.Contains(possibleValue))
                            {
                                square.Value.RemovePossibleValue(possibleValue);
                                changeCount++;
                            }
                        }
                    }
                }
            }

            return changeCount != 0;
        }
    }
}