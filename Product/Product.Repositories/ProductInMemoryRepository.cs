using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Repositories
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private readonly List<Models.Product> _products;

        public ProductInMemoryRepository()
        {
            _products = new List<Models.Product>();
        }

        public ProductInMemoryRepository(List<Models.Product> products)
        {
            _products = products;
        }

        public List<Models.Product> Get(int page = 1, int pageSize = 10, string searchText = "")
        {
            if (string.IsNullOrEmpty(searchText))
                return _products.Skip(page - 1 * pageSize).Take(pageSize).ToList();
            searchText = searchText.ToLower();
            return _products
                .Where(p => p.Brand.ToLower().Contains(searchText)
                            || p.Description.ToLower().Contains(searchText)
                            || p.Model.ToLower().Contains(searchText))
                .Skip(page - 1 * pageSize).Take(pageSize).ToList();
        }

        public List<Models.Product> Get(string desc, string model, string brand, int page = 1, int pageSize = 10)
        {
            return _products
                .Where(p => string.Equals(p.Brand, brand, StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(p.Description, desc, StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(p.Model, model, StringComparison.CurrentCultureIgnoreCase))
                .Skip(page - 1 * pageSize).Take(pageSize).ToList();
        }

        public Models.Product Get(string id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Models.Product product)
        {
            _products.Add(product);
        }

        public void Update(Models.Product product)
        {
            var p = Get(product.Id);
            p.Description = product.Description;
            p.Model = product.Model;
            p.Brand = product.Brand;
        }

        public void Delete(string id)
        {
            _products.Remove(Get(id));
        }

        public bool ProductExists(string brand, string model, string description)
        {
            return _products.Any(p => p.Brand == brand
                                      && p.Description == description
                                      && p.Model == model);
        }

        public bool ProductExists(string id)
        {
            return _products.Any(p => p.Id == id);
        }
    }
}