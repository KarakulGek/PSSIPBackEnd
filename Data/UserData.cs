using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class UserData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public List<User> GetOrders()
        {
            string Query = "SELECT * FROM Users;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<User> orders = Connection.Query<User>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<User>> GetOrdersAsync()
        {
            string Query = "SELECT * FROM Users;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<User> orders = Connection.Query<User>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<User>> GetUserByIdAsync(int id)
        {
            string Query = "SELECT * FROM Users WHERE Id = '" + id + "';";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<User> orders = Connection.Query<User>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<User>> GetUserByNameAsync(string name)
        {
            string Query = "SELECT * FROM Users WHERE Name = '" + name + "';";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<User> orders = Connection.Query<User>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<User>> GetUserByEmailAsync(string email)
        {
            string Query = "SELECT * FROM Users WHERE Email = '" + email + "';";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<User> orders = Connection.Query<User>(Query).ToList();
                return orders;
            }
        }
        public async Task AddOrderAsync(User Value)
        {
            string query = "INSERT INTO Users(Name, Email, Password, IsAdmin) VALUES(@Name, @Email, @Password, @IsAdmin)";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    Connection.Open();
                    await Connection.ExecuteAsync(query, Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public async Task UpdateOrderAsync(User Value)
        {
            string query = "UPDATE Users Set Name = @Name, Email=@Email, Password=@Password, IsAdmin=@IsAdmin WHERE Id = @Id";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    await Connection.ExecuteAsync(query, Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                Connection.Open();

                Console.WriteLine("Обновленная строка: " + 1);
            }
        }

        public async Task RemoveOrderAsync(int? Key)
        {
            string query = "DELETE FROM Users WHERE Id = @Id";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    Console.WriteLine("Remove step 1");
                    Connection.Open();
                    Console.WriteLine("Remove step 2");
                    await Connection.ExecuteAsync(query, new { Id = Key });
                    Console.WriteLine("Remove step 3");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }
    }
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte? IsAdmin { get; set; }
    }
}