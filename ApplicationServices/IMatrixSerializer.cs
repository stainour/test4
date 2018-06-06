using Domain;
using System.Collections.Generic;

namespace ApplicationServices
{
    public interface IMatrixSerializer<TDestination>
    {
        void Serialize(TDestination destination, IEnumerable<Matrix> matrices);
    }
}