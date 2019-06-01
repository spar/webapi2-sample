using System.Collections.Generic;

namespace Product.Repositories
{
    public interface IProductRepository
    {
        List<Models.Product> Get(int page = 1, int pageSize = 10, string searchText = "");

        List<Models.Product> Get(string desc, string model, string brand, int page = 1, int pageSize = 10);

        Models.Product Get(string id);

        void Create(Models.Product product);

        void Update(Models.Product product);

        void Delete(string id);

        bool ProductExists(string brand, string model, string description);

        bool ProductExists(string id);
    }
}