namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface IModelFor<T> 
    {
        int Id { get; set; }
        System.DateTime DateTimeCreated { get; set; }
        T ToTransferObject();
    }
}
