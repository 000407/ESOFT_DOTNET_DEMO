namespace demo_crud.Models
{
    public class Product
    {
        private ProductDataAccessContext context;

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}