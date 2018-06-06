using Domain;
using System.Collections.Generic;

namespace ApplicationServices.MatrixOperation
{
    internal class TransposeMatrixOperation : AbstactMatrixOperation
    {
        protected override int MinMatricesCount => 1;

        protected override IEnumerable<Matrix> Innner(Matrix[] matrices)
        {
            foreach (var matrix in matrices)
            {
                yield return matrix.Transpose();
            }
        }
    }
}