namespace ApplicationServices
{
    public interface IMatrixOperationFactory
    {
        IMatrixOperation Create(string name);
    }
}