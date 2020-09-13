namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface ITransferObjectFor<T>  
    {
        int Id { get; set; }
        System.DateTime DateTimeCreated { get; set; }
        T ToModelEntity();
    }
}
