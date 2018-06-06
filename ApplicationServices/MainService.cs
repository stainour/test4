using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApplicationServices
{
    public class MainService
    {
        private readonly IMatrixOperationCommandDeserializer<FileInfo> _matrixOperationCommandDeserializer;
        private readonly IMatrixOperationFactory _matrixOperationFactory;
        private readonly IMatrixSerializer<FileInfo> _matrixSerializer;

        public MainService(IMatrixOperationCommandDeserializer<FileInfo> matrixOperationCommandDeserializer, IMatrixOperationFactory matrixOperationFactory, IMatrixSerializer<FileInfo> matrixSerializer)
        {
            _matrixOperationCommandDeserializer = matrixOperationCommandDeserializer ?? throw new ArgumentNullException(nameof(matrixOperationCommandDeserializer));
            _matrixOperationFactory = matrixOperationFactory ?? throw new ArgumentNullException(nameof(matrixOperationFactory));
            _matrixSerializer = matrixSerializer;
        }

        public event EventHandler OnFileProcessed = (sender, args) => { };

        /// <summary>
        /// Processes the folder.
        /// </summary>
        /// <param name="fileInfos">The file infos.</param>
        /// <exception cref="System.ApplicationException"></exception>
        public void ProcessFolder(IEnumerable<FileInfo> fileInfos)
        {
            foreach (var fileInfo in fileInfos)
            {
                string operation = default;

                try
                {
                    var matrixOperationCommand = _matrixOperationCommandDeserializer.Deserialize(fileInfo);

                    operation = matrixOperationCommand.Operation;
                    var matrixOperation = _matrixOperationFactory.Create(operation);

                    var matrices = matrixOperation.Apply(matrixOperationCommand.Matrices);
                    _matrixSerializer.Serialize(new FileInfo(Path.Combine(fileInfo.DirectoryName, $"{fileInfo.Name}_result{fileInfo.Extension}")), matrices);
                    OnFileProcessed(this, null);
                }
                catch (ArgumentException e)
                {
                    throw new ApplicationException($"Matrices count in file={fileInfo.FullName} is not enough for operation \"{operation}\"", e);
                }
                catch (ComponentNotFoundException e)
                {
                    throw new ApplicationException($"Operation \"{operation }\" is not implemented (Bad file={fileInfo.FullName})", e);
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Error while processing file={fileInfo.FullName}: {e.Message}", e);
                }
            }
        }
    }
}