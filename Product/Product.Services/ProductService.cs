using System;
using System.Collections.Generic;
using Product.Models;
using Product.Repositories;

namespace Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public PaginatedResult<List<Models.Product>> Get(int page = 1, int pageSize = 10, string searchText = "")
        {
            return new PaginatedResult<List<Models.Product>>
            {
                CurrentPage = page,
                PageSize = pageSize,
                Data = _productRepository.Get(page, pageSize, searchText),
                Success = true
            };
        }

        public Models.Product Get(string id)
        {
            return _productRepository.Get(id);
        }

        public Result<Models.Product> Create(Models.Product product)
        {
            if (_productRepository.ProductExists(product.Brand, product.Model, product.Description))
                return new Result<Models.Product>
                {
                    Success = false,
                    Error = "Product already exists"
                };
            product.Id = Guid.NewGuid().ToString();
            _productRepository.Create(product);
            return new Result<Models.Product>
            {
                Data = product,
                Success = true
            };
        }

        public Result<Models.Product> Update(Models.Product product)
        {
            if (!_productRepository.ProductExists(product.Id))
                return new Result<Models.Product>
                {
                    Success = false,
                    Error = $"Product with Id '{product.Id}' doesn't exist"
                };
            _productRepository.Update(product);
            return new Result<Models.Product>
            {
                Data = product,
                Success = true
            };
        }

        public Result<Models.Product> Delete(string id)
        {
            if (!_productRepository.ProductExists(id))
                return new Result<Models.Product>
                {
                    Success = false,
                    Error = $"Product with Id '{id}' doesn't exist"
                };
            _productRepository.Delete(id);
            return new Result<Models.Product>
            {
                Success = true
            };
        }
    }
}