using Domain;
using System.Collections.Generic;

namespace ApplicationServices
{
    public interface IMatrixOperation
    {
        /// <summary>
        /// Applies the specified matrices.
        /// </summary>
        /// <param name="matrices">The matrices.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">matrices</exception>
        /// <exception cref="System.ArgumentException">matrices count is not enough</exception>
        IEnumerable<Matrix> Apply(IEnumerable<Matrix> matrices);
    }
}