using Microsoft.Data.SqlClient;
namespace MidtermExam.Models
{
    public class DBmanager
    {
        private readonly string ConnStr = "Data Source=127.0.0.1;Initial Catalog=shop;Integrated Security=True;Trust Server Certificate=True";

        public List<Shop> GetShop()
        {
            List<Shop> shops = new List<Shop>();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM shop");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Shop shop = new Shop
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        product_name = reader.GetString(reader.GetOrdinal("product_name")),
                        price = reader.GetString(reader.GetOrdinal("price")),
                        category = reader.GetString(reader.GetOrdinal("category")),
                    };
                    shops.Add(shop);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return shops;
        }
        public void NewShop(Shop shop)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
            @"INSERT INTO shop (product_name, price, category)
            VALUES(@product_name,@price, @category)", sqlConnection);
            //sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@product_name", shop.product_name));
            sqlCommand.Parameters.Add(new SqlParameter("@price", shop.price));
            sqlCommand.Parameters.Add(new SqlParameter("@category", shop.category));

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

        }
        public Shop GetShopByID(int id)
        {
            Shop shop = new Shop();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM shop WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    shop = new Shop
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        product_name = reader.GetString(reader.GetOrdinal("product_name")),
                        price = reader.GetString(reader.GetOrdinal("price")),
                        category = reader.GetString(reader.GetOrdinal("category")),
                    };
                }
            }
            else
            {
                shop.product_name = "未找到該筆資料";
            }
            sqlConnection.Close();
            return shop;
        }

        public void UpdateShop(Shop shop)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
            @"UPDATE shop SET product_name = @product_name, price = @price, category =
@category WHERE id = @id", sqlConnection);
            //sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@product_name"
            , shop.product_name));
            sqlCommand.Parameters.Add(new SqlParameter("@price"
            , shop.price));
            sqlCommand.Parameters.Add(new SqlParameter("@category"
            , shop.category));
            sqlCommand.Parameters.Add(new SqlParameter("@id", shop.ID));
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void DeleteShopById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM shop WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}


