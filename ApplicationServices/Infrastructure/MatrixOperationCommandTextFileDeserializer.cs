using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApplicationServices
{
    public class MatrixOperationCommandTextFileDeserializer : IMatrixOperationCommandDeserializer<FileInfo>
    {
        /// <summary>
        /// Deserializes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public MatrixOperationCommand Deserialize(FileInfo source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            string operation;
            var matrices = new List<Matrix>();

            using (var fileStream = source.OpenRead())
            {
                using (TextReader textReader = new StreamReader(fileStream))
                {
                    operation = textReader.ReadLine().Trim();
                    textReader.ReadLine();
                    var currentLine = " ";
                    while (currentLine != null)
                    {
                        var rows = new List<int[]>();
                        while (true)
                        {
                            currentLine = textReader.ReadLine();
                            if (string.IsNullOrWhiteSpace(currentLine))
                            {
                                break;
                            }

                            var matrixRow = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                            rows.Add(matrixRow);
                        }

                        if (rows.Any())
                        {
                            var columnCount = rows.First().Length;
                            var values = new int[rows.Count, columnCount];

                            for (int rowNumber = 0; rowNumber < rows.Count; rowNumber++)
                            {
                                for (int columnNumber = 0; columnNumber < columnCount; columnNumber++)
                                {
                                    values[rowNumber, columnNumber] = rows[rowNumber][columnNumber];
                                }
                            }

                            matrices.Add(new Matrix(values));
                        }
                    }

                    return new MatrixOperationCommand(operation, matrices);
                }
            }
        }
    }
}