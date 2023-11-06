using MySql.Data.MySqlClient;

namespace demo_crud.Models
{
    public class ProductDataAccessContext
    {
        public string ConnectionString { get; private set; }

        public ProductDataAccessContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Product> GetAllProducts()
        {
            List<Product> list = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new("SELECT * FROM products", conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Product() {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDouble(reader["Price"]),
                    });
                }
            }
            return list;
        }
    }
}