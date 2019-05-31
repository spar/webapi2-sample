using Product.Repositories;
using Xunit;

namespace Product.UnitTests
{
    public class ProductInMemoryRepositoryTests
    {
        private static void InitProducts(IProductRepository repository)
        {
            repository.Create(new Models.Product("Id1", "Desc1", "Model1", "Brand1"));
            repository.Create(new Models.Product("Id2", "Desc2", "Model2", "Brand2"));
            repository.Create(new Models.Product("Id3", "Desc3", "Model3", "Brand3"));
            repository.Create(new Models.Product("Id4", "Desc4", "Model4", "Brand4"));
            repository.Create(new Models.Product("Id5", "Desc5", "Model5", "Brand5"));
        }

        [Fact]
        public void GetTest()
        {
            var repo = new ProductInMemoryRepository();
            var product = repo.Get("Id1");
            Assert.Null(product);

            InitProducts(repo);
            product = repo.Get("Id1");
            Assert.Equal("Id1", product.Id);
        }

        [Fact]
        public void GetPaginationTest()
        {
            var repo = new ProductInMemoryRepository();
            InitProducts(repo);
            var products = repo.Get(1, 1);
            Assert.Single(products);
            Assert.Equal("Model1", products[0].Model);

            products = repo.Get(2, 1);
            Assert.Single(products);
            Assert.Equal("Model2", products[0].Model);
        }

        [Fact]
        public void GetSearchTextTest()
        {
            var repo = new ProductInMemoryRepository();
            InitProducts(repo);
            var products = repo.Get(1, 10, "Brand1");
            Assert.Single(products);
            Assert.Equal("Model1", products[0].Model);

            products = repo.Get(1, 5, "Model5");
            Assert.Single(products);
            Assert.Equal("Model5", products[0].Model);
        }

        [Fact]
        public void CreateTest()
        {
            var repo = new ProductInMemoryRepository();
            repo.Create(new Models.Product("Id1", "Desc1", "Model1", "Brand1"));
            var product = repo.Get(1, 1);
            Assert.Equal("Id1", product[0].Id);
        }

        [Fact]
        public void UpdateTest()
        {
            var repo = new ProductInMemoryRepository();
            var prod = new Models.Product("Id1", "Desc1", "Model1", "Brand1");
            repo.Create(prod);

            prod.Brand = "Brand2";
            prod.Description = "Desc2";
            prod.Model = "Model2";

            repo.Update(prod);

            var insertedProd = repo.Get(prod.Id);
            Assert.Equal("Desc2", insertedProd.Description);
            Assert.Equal("Model2", insertedProd.Model);
            Assert.Equal("Brand2", insertedProd.Brand);
        }

        [Fact]
        public void DeleteTest()
        {
            var repo = new ProductInMemoryRepository();
            InitProducts(repo);

            repo.Delete("Id1");
            Assert.Equal(4, repo.Get().Count);

            var deleted = repo.Get("Id1");
            Assert.Null(deleted);
        }

        [Fact]
        public void ProductExistsTest()
        {
            var repo = new ProductInMemoryRepository();
            InitProducts(repo);
            Assert.True(repo.ProductExists("Brand1", "Model1", "Desc1"));
            Assert.False(repo.ProductExists("Brand3", "Model1", "Desc1"));
        }

        [Fact]
        public void ProductExistsByIdTest()
        {
            var repo = new ProductInMemoryRepository();
            InitProducts(repo);
            Assert.True(repo.ProductExists("Id1"));
            Assert.False(repo.ProductExists("Id0"));
        }
    }
}