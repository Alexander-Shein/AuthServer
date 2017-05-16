namespace AuthGuard.BLL.Domain.Entities.Common
{
    public interface IReadOnly
    {
        bool IsReadOnly { get; set; }
    }
}