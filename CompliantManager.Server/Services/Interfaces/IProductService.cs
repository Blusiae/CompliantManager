namespace CompliantManager.Server.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> DeleteAsync(int id);
    }
}
