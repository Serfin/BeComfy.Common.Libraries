namespace BeComfy.Common.Mongo
{
    public interface IPaginable
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}