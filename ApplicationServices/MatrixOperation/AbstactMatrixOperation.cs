using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationServices.MatrixOperation
{
    internal abstract class AbstactMatrixOperation : IMatrixOperation
    {
        protected abstract int MinMatricesCount { get; }

        public IEnumerable<Matrix> Apply(IEnumerable<Matrix> matrices)
        {
            if (matrices == null) throw new ArgumentNullException(nameof(matrices));
            var array = matrices.ToArray();
            if (array.Length < MinMatricesCount)
            {
                throw new ArgumentException($"Should be at least {MinMatricesCount} matrices!", nameof(matrices));
            }

            return Innner(array);
        }

        protected abstract IEnumerable<Matrix> Innner(Matrix[] matrices);
    }
}