namespace ApplicationServices
{
    public interface IMatrixOperationCommandDeserializer<TSource>
    {
        MatrixOperationCommand Deserialize(TSource source);
    }
}