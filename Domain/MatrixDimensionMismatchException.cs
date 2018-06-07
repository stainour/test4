using System;

namespace Domain
{
    public class MatrixDimensionMismatchException : ApplicationException
    {
        public MatrixDimensionMismatchException(Matrix matrix1, Matrix matrix2, string operation) : base($"Matrices{Environment.NewLine}{matrix1} and {Environment.NewLine}{matrix2} have incompatible dimensions in context of \"{operation}\" operation!")
        {
        }
    }
}