namespace AuthGuard.BLL.Domain.Entities.Common
{
    public interface IRowVersion
    {
        byte[] Ts { get; set; }
    }
}