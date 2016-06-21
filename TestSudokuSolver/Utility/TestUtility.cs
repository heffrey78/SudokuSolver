using System;
using System.IO;
using System.Reflection;

namespace TestSudokuSolver.Utility
{
    /// <summary>
    ///     Class that contains utility methods for testing.
    /// </summary>
    internal class TestUtility
    {
        /// <summary>
        ///     Method that retrieves the local execution path. 
        ///     This is useful to compensate for ReSharper's shadow copy.
        /// </summary>
        /// <returns>
        ///     A string that represents the execution path.
        /// </returns>
        internal static string GetLocalPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var codeBase = new Uri(assembly.CodeBase);
            var path = codeBase.LocalPath;
            return Path.GetDirectoryName(path);
        }
    }
}
