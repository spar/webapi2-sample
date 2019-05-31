using System.Collections.Generic;
using Product.Models;

namespace Product.Services
{
    public interface IProductService
    {
        PaginatedResult<List<Models.Product>> Get(int page = 1, int pageSize = 10, string searchText = "");

        Models.Product Get(string id);

        Result<Models.Product> Create(Models.Product product);

        Result<Models.Product> Update(Models.Product product);

        Result<Models.Product> Delete(string id);
    }
}