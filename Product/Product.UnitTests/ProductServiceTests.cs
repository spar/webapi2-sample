using Moq;
using Product.Repositories;
using Product.Services;
using System.Collections.Generic;
using Xunit;

namespace Product.UnitTests
{
    public class ProductServiceTests
    {
        private static List<Models.Product> InitProducts()
        {
            return new List<Models.Product>
            {
                new Models.Product("Id1", "Desc1", "Model1", "Brand1")
                ,new Models.Product("Id2", "Desc2", "Model2", "Brand2")
                ,new Models.Product("Id3", "Desc3", "Model3", "Brand3")
                ,new Models.Product("Id4", "Desc4", "Model4", "Brand4")
                ,new Models.Product("Id5", "Desc5", "Model5", "Brand5")
            };
        }

        [Fact]
        public void GetTest()
        {
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(x => x.Get("Id1")).Returns(prod);

            var service = new ProductService(repoMock.Object);
            var returned = service.Get("Id1");
            Assert.Equal(prod.Id, returned.Id);
            Assert.Equal(prod.Brand, returned.Brand);
            Assert.Equal(prod.Description, returned.Description);
            Assert.Equal(prod.Model, returned.Model);
        }

        [Fact]
        public void GetPaginationTest()
        {
            var repoMock = new Mock<IProductRepository>();
            var products = InitProducts();
            repoMock.Setup(x => x.Get(1, 5, "")).Returns(products);

            var service = new ProductService(repoMock.Object);
            var returned = service.Get(1, 5, "");

            Assert.Equal(1, returned.CurrentPage);
            Assert.Equal(5, returned.PageSize);
            Assert.True(returned.Success);
            Assert.Equal(products, returned.Data);
        }

        [Fact]
        public void CreateTest_ProductDoesNotExists()
        {
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var service = new ProductService(repoMock.Object);
            var result = service.Create(prod);

            Assert.True(result.Success);
            Assert.Equal(prod, result.Data);
        }

        [Fact]
        public void CreateTest_ProductExists()
        {
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var service = new ProductService(repoMock.Object);
            var result = service.Create(prod);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Product already exists", result.Error);
        }

        [Fact]
        public void UpdateTest_ProductExists()
        {
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists("Id1"))
                .Returns(true);

            var service = new ProductService(repoMock.Object);
            var result = service.Update(prod);

            Assert.True(result.Success);
            Assert.Equal(prod, result.Data);
        }

        [Fact]
        public void UpdateTest_ProductDoesNotExists()
        {
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists("Id1"))
                .Returns(false);

            var service = new ProductService(repoMock.Object);
            var result = service.Update(prod);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal($"Product with Id 'Id1' doesn't exist", result.Error);
        }

        [Fact]
        public void DeleteTest_ProductExists()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists("Id1"))
                .Returns(true);

            var service = new ProductService(repoMock.Object);
            var result = service.Delete("Id1");

            Assert.True(result.Success);
        }

        [Fact]
        public void DeleteTest_ProductDoesNotExists()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(x => x.ProductExists("Id1"))
                .Returns(false);

            var service = new ProductService(repoMock.Object);
            var result = service.Delete("Id1");

            Assert.False(result.Success);
            Assert.Equal("Product with Id 'Id1' doesn't exist", result.Error);
        }
    }
}