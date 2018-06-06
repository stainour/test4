using Domain;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    internal class MatrixTests
    {
        [Test]
        public void Constructor_CorrectArray_Ok()
        {
            var matrix = new Matrix(new[,]
            {
                { 1, 4, 6, 7 },
                { 1, 4, 6, 7 }
            });

            Assert.AreEqual(matrix.RowCount, 2);
            Assert.AreEqual(matrix.ColumnCount, 4);
        }

        [Test]
        public void MatrixMultiplication_CorrectMatrices_Ok()
        {
            var matrix1 = new Matrix(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            var matrix2 = new Matrix(new[,]
            {
                { 7, 8 },
                { 9, 10 },
                { 11, 12 }
            });

            var resultMatrix = new Matrix(new[,]
            {
                { 58, 64 },
                { 139, 154 }
            });

            Assert.IsTrue(matrix1 * matrix2 == resultMatrix);
        }

        [Test]
        public void Substract_CorrectMatrices_Ok()
        {
            var matrix1 = new Matrix(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            var matrix2 = new Matrix(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            var resultMatrix = new Matrix(new[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 }
            });

            Assert.IsTrue(matrix1 - matrix2 == resultMatrix);
        }

        [Test]
        public void SummAndScalarMultiplication_CorrectMatrices_Ok()
        {
            var matrix1 = new Matrix(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            var matrix2 = new Matrix(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            var resultMatrix = 2 * matrix2;

            Assert.IsTrue(matrix1 + matrix2 == resultMatrix);
        }

        [Test]
        public void Transpose_Ok()
        {
            var matrixToTranspose = new Matrix(new[,]
            {
                 { 1, 4, 6, 7 },
                 { 90, 40, 60, 70 }
            });

            var transposedMatrix = new Matrix(new[,]
            {
                { 1, 90 },
                { 4, 40 },
                { 6, 60 },
                { 7, 70 }
            });

            Assert.IsTrue(matrixToTranspose.Transpose() == transposedMatrix);
        }
    }
}