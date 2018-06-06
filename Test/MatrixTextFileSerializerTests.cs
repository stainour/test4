using ApplicationServices.Infrastructure;
using Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    [TestFixture]
    public class MatrixTextFileSerializerTests
    {
        private readonly FileInfo _fileInfo;
        private readonly Matrix _matrix;
        private readonly MatrixTextFileSerializer _serializer;

        public MatrixTextFileSerializerTests()
        {
            _matrix = new Matrix(new[,]
            {
                { 1, 4, 6, 78 },
                { 1, 41, 6, 7 }
            });

            _fileInfo = new FileInfo("____.txt");
            _serializer = new MatrixTextFileSerializer();
        }

        [Test]
        public void Serialize_Ok()
        {
            _serializer.Serialize(_fileInfo, new List<Matrix> { _matrix, _matrix });
            string fileContent;

            using (var fileStream = _fileInfo.OpenRead())
            {
                using (TextReader textReader = new StreamReader(fileStream))
                {
                    fileContent = textReader.ReadToEnd();
                }
            }

            Assert.AreEqual(fileContent, "1 4 6 78\r\n1 41 6 7\r\n\r\n1 4 6 78\r\n1 41 6 7\r\n\r\n");
        }
    }
}