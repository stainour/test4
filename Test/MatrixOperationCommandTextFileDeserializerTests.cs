using ApplicationServices;
using Domain;
using NUnit.Framework;
using System.Linq;

namespace Test
{
    [TestFixture]
    internal class MatrixOperationCommandTextFileDeserializerTests
    {
        private MatrixOperationCommandTextFileDeserializer _matrixOperationCommandTextFileDeserializer;

        public MatrixOperationCommandTextFileDeserializerTests()
        {
            _matrixOperationCommandTextFileDeserializer = new MatrixOperationCommandTextFileDeserializer();
        }

        [Test]
        public void Deserialize_Multiply_Ok()
        {
            var result = _matrixOperationCommandTextFileDeserializer.Deserialize(TestFiles.MultiplyFileInfo);

            Assert.AreEqual(result.Matrices.Count(), 2);

            Assert.AreEqual(result.Operation, Operations.Multiply);

            Assert.AreEqual(result.Matrices.First(), new Matrix(new[,]
            {
                { 2, 5, 6 },
                { 8, 55, 6 },
                { 7, 8, 5 }
            }));
            Assert.AreEqual(result.Matrices.Last(), new Matrix(new[,]
            {
                { 59, 48, 65 },
                { 59, 141, 56 },
                { 5, 5, 6 }
            }));
        }

        [Test]
        public void Deserialize_Transpose_Ok()
        {
            var result = _matrixOperationCommandTextFileDeserializer.Deserialize(TestFiles.TransposeFileInfo);

            Assert.AreEqual(result.Matrices.Count(), 1);

            Assert.AreEqual(result.Operation, Operations.Transpose);

            Assert.AreEqual(result.Matrices.First(), new Matrix(new[,]
            {
                { 2, 5, 6, 99 },
                { 8, 55, 6, 9 },
                { 7, 8, 5, 56 }
            }));
        }
    }
}