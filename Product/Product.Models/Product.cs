namespace Product.Models
{
    public class Product
    {
        public Product(string id, string desc, string model, string brand)
        {
            Id = id;
            Description = desc;
            Model = model;
            Brand = brand;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}