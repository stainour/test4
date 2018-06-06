using Domain;
using System.Collections.Generic;

namespace ApplicationServices.MatrixOperation
{
    internal class AddMatrixOperation : AbstactMatrixOperation
    {
        protected override int MinMatricesCount => 2;

        protected override IEnumerable<Matrix> Innner(Matrix[] matrices)
        {
            var matrix = matrices[0];
            for (var i = 1; i < matrices.Length; i++)
            {
                matrix += matrices[i];
            }

            yield return matrix;
        }
    }
}