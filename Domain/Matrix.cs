using System;
using System.Text;

namespace Domain
{
    public class Matrix : IEquatable<Matrix>
    {
        private readonly int[,] _values;

        public Matrix(int[,] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            _values = (int[,])values.Clone();
        }

        private Matrix(int[,] values, bool dontCheckAndCopyArray)
        {
            _values = values;
        }

        public int ColumnCount => _values.GetLength(1);
        public int RowCount => _values.GetLength(0);

        public int this[int rowNumber, int columnNumber] => _values[rowNumber, columnNumber];

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="matrix1">The matrix1.</param>
        /// <param name="matrix2">The matrix2.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="MatrixDimensionMistmatchException">-</exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (!(matrix1.RowCount == matrix2.RowCount && matrix1.ColumnCount == matrix2.ColumnCount))
            {
                throw new MatrixDimensionMistmatchException(matrix1, matrix2, "-");
            }
            return matrix1 + (-1 * matrix2);
        }

        public static bool operator !=(Matrix c1, Matrix c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The result of the operator.</returns>
        public static Matrix operator *(int scalar, Matrix matrix)
        {
            var newMatrixValues = new int[matrix.RowCount, matrix.ColumnCount];
            for (var rowNumber = 0; rowNumber < matrix.RowCount; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < matrix.ColumnCount; columnNumber++)
                {
                    newMatrixValues[rowNumber, columnNumber] = matrix[rowNumber, columnNumber] * scalar;
                }
            }
            return new Matrix(newMatrixValues, true);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="matrix1">The matrix1.</param>
        /// <param name="matrix2">The matrix2.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="MatrixDimensionMistmatchException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.ColumnCount != matrix2.RowCount)
            {
                throw new MatrixDimensionMistmatchException(matrix1, matrix2, "multiplication");
            }
            var newMatrixValues = new int[matrix1.RowCount, matrix2.ColumnCount];

            for (var rowNumberFirstMatrix = 0; rowNumberFirstMatrix < matrix1.RowCount; rowNumberFirstMatrix++)
            {
                for (var columnNumberSecondMatrix = 0; columnNumberSecondMatrix < matrix2.ColumnCount; columnNumberSecondMatrix++)
                {
                    var value = 0;
                    for (var columnNumberFirstMatrix = 0; columnNumberFirstMatrix < matrix1.ColumnCount; columnNumberFirstMatrix++)
                    {
                        value += matrix1[rowNumberFirstMatrix, columnNumberFirstMatrix] * matrix2[columnNumberFirstMatrix, columnNumberSecondMatrix];
                    }
                    newMatrixValues[rowNumberFirstMatrix, columnNumberSecondMatrix] = value;
                }
            }
            return new Matrix(newMatrixValues, true);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="matrix1">The matrix1.</param>
        /// <param name="matrix2">The matrix2.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="MatrixDimensionMistmatchException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (!(matrix1.RowCount == matrix2.RowCount && matrix1.ColumnCount == matrix2.ColumnCount))
            {
                throw new MatrixDimensionMistmatchException(matrix1, matrix2, "summ");
            }
            var newMatrixValues = new int[matrix1.RowCount, matrix2.ColumnCount];

            for (var rowNumber = 0; rowNumber < matrix1.RowCount; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < matrix1.ColumnCount; columnNumber++)
                {
                    newMatrixValues[rowNumber, columnNumber] = matrix1[rowNumber, columnNumber] + matrix2[rowNumber, columnNumber];
                }
            }
            return new Matrix(newMatrixValues, true);
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            if (ReferenceEquals(matrix1, null) || ReferenceEquals(matrix2, null)) return false;

            return matrix1.Equals(matrix2);
        }

        public bool Equals(Matrix other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.ColumnCount != ColumnCount || other.RowCount != RowCount) return false;

            for (var rowNumber = 0; rowNumber < other.RowCount; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < other.ColumnCount; columnNumber++)
                {
                    if (this[rowNumber, columnNumber] != other[rowNumber, columnNumber])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Matrix)obj);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (var rowNumber = 0; rowNumber < RowCount; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < ColumnCount; columnNumber++)
                {
                    builder.Append(' ');
                    builder.Append(_values[rowNumber, columnNumber]);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public Matrix Transpose()
        {
            var values = new int[ColumnCount, RowCount];

            for (var rowNumber = 0; rowNumber < RowCount; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < ColumnCount; columnNumber++)
                {
                    values[columnNumber, rowNumber] = this[rowNumber, columnNumber];
                }
            }

            return new Matrix(values, true);
        }
    }
}