namespace BeComfy.CQRS
{
    // Marker
    public interface IQuery
    {
        
    }
    
    // Marker
    public interface IQuery<TResult> : IQuery
    {

    }

    public interface IPaginableQuery<TResult> : IQuery
    {

    }
}