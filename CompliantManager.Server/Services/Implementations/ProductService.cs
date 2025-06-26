using CompliantManager.Server.Repositories.Implementations;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;

namespace CompliantManager.Server.Services.Implementations
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<bool> DeleteAsync(int id)
        {
            var claim = await _productRepository.GetByIdAsync(id);
            if (claim == null)
            {
                return false;
            }
            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
