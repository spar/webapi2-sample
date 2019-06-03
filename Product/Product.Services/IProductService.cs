using Product.Models;
using System.Collections.Generic;

namespace Product.Services
{
    public interface IProductService
    {
        PaginatedResult<List<Models.Product>> Get(int page = 1, int pageSize = 10, string searchText = "");

        PaginatedResult<List<Models.Product>> Get(string desc, string model, string brand, int page = 1, int pageSize = 10);

        Models.Product Get(string id);

        Result<Models.Product> Create(Models.Product product);

        Result<Models.Product> Update(Models.Product product);

        Result<Models.Product> Delete(string id);
    }
}