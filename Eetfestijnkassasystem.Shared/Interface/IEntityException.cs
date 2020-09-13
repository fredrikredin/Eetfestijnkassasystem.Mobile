namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface IEntityException
    {
        string Model { get; set; }
        string Property { get; set; }
        string Type { get; }
        string StackTrace { get; }
        string Message { get; }
    }
}
