using System;
using System.Data;
using System.Data.SqlClient;

namespace CoAPServer
{
    class Database
    {
        private SqlConnection connection;

        public Database(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Execute(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void AddProduct(string name, double price)
        {
            // Replace underscores with spaces
            name = name.Replace('_', ' ');

            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[products] ([name], [price]) VALUES (@name, @price)", connection);
            command.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar)).Value = name;
            command.Parameters.Add(new SqlParameter("@price", SqlDbType.Float)).Value = price;
            command.ExecuteNonQuery();

            Console.WriteLine("Added product '{0}' with price {1}", name, price);
        }

        public void GetProducts()
        {
            Console.WriteLine("Product database:");

            SqlCommand command = new SqlCommand("SELECT [id], [name], [price], [purchases] FROM [dbo].[products]", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("id"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    double price = reader.GetDouble(reader.GetOrdinal("price"));
                    int purchases = reader.GetInt32(reader.GetOrdinal("purchases"));

                    Console.WriteLine("- [ID: {0}] [Name: {1}] [Price: {2}] [Purchases: {3}]", id, name, price, purchases);
                }
            }
        }

        public void GetLastScans()
        {
            Console.WriteLine("Last 10 scans:");

            SqlCommand command = new SqlCommand("SELECT TOP 10 [tag], [scanned_at] FROM [dbo].[scans] ORDER BY [scanned_at] DESC", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tag = reader.GetString(reader.GetOrdinal("tag"));
                    DateTime scannedAt = reader.GetDateTime(reader.GetOrdinal("scanned_at"));

                    Console.WriteLine("- [Tag: {0}] [Scanned At: {1}]", tag, scannedAt);
                }
            }
        }

        public void AddTag(string tag, int productId)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[tags] ([tag], [product_id]) VALUES (@tag, @product_id)", connection);
            command.Parameters.Add(new SqlParameter("@tag", SqlDbType.VarChar)).Value = tag;
            command.Parameters.Add(new SqlParameter("@product_id", SqlDbType.Int)).Value = productId;
            command.ExecuteNonQuery();

            Console.WriteLine("Added tag {0} with product id: {1}", tag, productId);
        }

        public void ClearTags()
        {
            SqlCommand command = new SqlCommand("TRUNCATE TABLE [dbo].[tags]", connection);
            command.ExecuteNonQuery();
        }

        public void AddScan(string tag)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[scans] ([tag], [scanned_at]) VALUES (@tag, @scanned_at)", connection);
            command.Parameters.Add(new SqlParameter("@tag", SqlDbType.VarChar)).Value = tag;
            command.Parameters.Add(new SqlParameter("@scanned_at", SqlDbType.DateTime)).Value = DateTime.Now;
            command.ExecuteNonQuery();

            Console.WriteLine("Added scan for tag {0}", tag);

            int productId = GetProductId(tag);
            if (productId >= 0)
                IncrementPurchases(productId);
            else
                Console.WriteLine("No product found for tag: {0}", tag);
        }

        public void ClearScans()
        {
            SqlCommand command = new SqlCommand("TRUNCATE TABLE [dbo].[scans]", connection);
            command.ExecuteNonQuery();
        }

        private int GetProductId(string tag)
        {
            SqlCommand command = new SqlCommand("SELECT [product_id] FROM [dbo].[tags] WHERE [tag] = @tag", connection);
            command.Parameters.Add(new SqlParameter("@tag", SqlDbType.VarChar)).Value = tag;
            Console.WriteLine("Requested product id for tag {0}", tag);

            object result = command.ExecuteScalar();
            if (result != null)
            {
                return (int)result;
            }

            return -1;
        }

        private void IncrementPurchases(int productId)
        {
            SqlCommand command = new SqlCommand("UPDATE [dbo].[products] SET [purchases] = [purchases] + 1 WHERE [id] = @product_id", connection);
            command.Parameters.Add(new SqlParameter("@product_id", SqlDbType.Int)).Value = productId;
            command.ExecuteNonQuery();
            Console.WriteLine("Increased purchases for product id: {0}", productId);
        }
    }
}
