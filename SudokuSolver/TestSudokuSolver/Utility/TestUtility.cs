using System;
using System.IO;
using System.Reflection;

namespace TestSudokuSolver.Utility
{
    internal class TestUtility
    {
        internal static string GetLocalPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var codeBase = new Uri(assembly.CodeBase);
            var path = codeBase.LocalPath;
            return Path.GetDirectoryName(path);
        }
    }
}
