﻿using Product.Models;
using Product.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace Product.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController()
        {
        }

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        public PaginatedResult<List<Models.Product>> Get(int page = 1, int pageSize = 10)
        {
            return _productService.Get(page, pageSize);
        }

        // GET api/products/id1
        public Models.Product Get(string id)
        {
            return _productService.Get(id);
        }

        // POST api/products
        public Result<Models.Product> Post([FromBody]Models.Product product)
        {
            return _productService.Create(product);
        }

        // PUT api/products/id1
        public Result<Models.Product> Put(string id, [FromBody]Models.Product product)
        {
            product.Id = id;
            return _productService.Update(product);
        }

        // DELETE api/products/id1
        public Result<Models.Product> Delete(string id)
        {
            return _productService.Delete(id);
        }
    }
}