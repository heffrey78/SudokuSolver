using System;

namespace SudokuSolver
{
    /// <summary>
    ///     Custom Exception Class
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        ///     Constructor for ValidationException
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        ///     Constructor for ValidationException
        /// </summary>
        /// <param name="message">
        ///     string that represents an Exception message
        /// </param>
        public ValidationException(string message) : base(message)
        {
        }
    }
}