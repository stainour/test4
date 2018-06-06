using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationServices.Infrastructure
{
    public class MatrixTextFileSerializer : IMatrixSerializer<FileInfo>
    {
        /// <summary>
        /// Serializes the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="matrices">The matrices.</param>
        /// <exception cref="System.ArgumentNullException">destination or matrices</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The path specified when creating an instance of the <see
        /// cref="T:System.IO.FileInfo"></see> object is read-only or is a directory.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The path specified when creating an instance of the <see
        /// cref="T:System.IO.FileInfo"></see> object is invalid, such as being on an unmapped drive.
        /// </exception>
        public void Serialize(FileInfo destination, IEnumerable<Matrix> matrices)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (matrices == null) throw new ArgumentNullException(nameof(matrices));
            using (var fileStream = destination.OpenWrite())
            {
                using (TextWriter textWriter = new StreamWriter(fileStream))
                {
                    foreach (var matrix in matrices)
                    {
                        for (var rowNumber = 0; rowNumber < matrix.RowCount; rowNumber++)
                        {
                            var rowStringBuilder = new StringBuilder();

                            for (var columnNumber = 0; columnNumber < matrix.ColumnCount - 1; columnNumber++)
                            {
                                rowStringBuilder.Append(matrix[rowNumber, columnNumber]);
                                rowStringBuilder.Append(' ');
                            }

                            rowStringBuilder.Append(matrix[rowNumber, matrix.ColumnCount - 1]);

                            textWriter.WriteLine(rowStringBuilder);
                        }
                        textWriter.WriteLine();
                    }
                }
            }
        }
    }
}