using System;
using System.IO;
using SudokuSolver.Business;

namespace SudokuSolver.Service
{
    /// <summary>
    ///     Class that handles file operations for a Grid
    /// </summary>
    public class GridService
    {
        /// <summary>
        ///     Method that loads a Grid into memory from disk
        /// </summary>
        /// <param name="filePath">
        ///     String that indicates the input file path
        /// </param>
        /// <returns>
        ///     A Grid object.
        /// </returns>
        public static Grid LoadGrid(string filePath)
        {
            try
            {
                var grid = new Grid();

                if (File.Exists(filePath))
                {
                    var fileRows = File.ReadAllLines(filePath);

                    if (fileRows.Length != 9)
                    {
                        throw new ValidationException("Grid must have nine rows.");
                    }

                    for (var i = 0; i < fileRows.Length; i++)
                    {
                        var records = fileRows[i].ToCharArray();

                        if (fileRows[i].Length != 9)
                        {
                            throw new ValidationException("Row must have nine columns.");
                        }

                        for (var j = 0; j < records.Length; j++)
                        {
                            var coordinate = new Coordinate(i, j);
                            var record = records[j].ToString();
                            var value = 0;

                            if (record != "X" && !int.TryParse(record, out value))
                            {
                                throw new ValidationException("Square contains invalid character.");
                            }

                            var square = new Square(value);
                            grid.Add(coordinate, square);
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException($"The file: {filePath} was not found.");
                }

                return grid;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Error Loading File");
            }
        }

        /// <summary>
        ///     Method that saves a solved Grid.
        /// </summary>
        /// <param name="filePath">
        ///     String that indicates the output file path.
        /// </param>
        /// <param name="grid">
        ///     The Grid to save.
        /// </param>
        public static void SaveGrid(string filePath, Grid grid)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.WriteAllText(filePath, grid.ToString());
            }
            catch (Exception)
            {
                throw new Exception("Error Saving File.");
            }
        }

        /// <summary>
        ///     Method that returns an output file path.
        /// </summary>
        /// <param name="inputFilePath">
        ///     String that indicates the input file path.
        /// </param>
        /// <returns>
        ///     String indicating the output file path.
        /// </returns>
        public static string GetSaveName(string inputFilePath)
        {
            return inputFilePath.Substring(0, inputFilePath.IndexOf('.') + 1) + "sln.txt";
        }
    }
}
