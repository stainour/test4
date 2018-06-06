using Domain;
using System;
using System.Collections.Generic;

namespace ApplicationServices
{
    public class MatrixOperationCommand
    {
        public MatrixOperationCommand(string operation, IEnumerable<Matrix> matrices)
        {
            Operation = operation ?? throw new ArgumentNullException(nameof(operation));
            Matrices = matrices ?? throw new ArgumentNullException(nameof(matrices));
        }

        public IEnumerable<Matrix> Matrices { get; }
        public string Operation { get; }
    }
}