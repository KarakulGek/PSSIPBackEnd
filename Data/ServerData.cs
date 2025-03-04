using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class ServerData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public List<Server> GetOrders()
        {
            string Query = "SELECT * FROM Servers;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Server> orders = Connection.Query<Server>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<Server>> GetOrdersAsync()
        {
            string Query = "SELECT Servers.Id, Servers.Name, Servers.MaxCapacity,  COUNT(characters.id) AS count FROM Servers LEFT JOIN characters ON Servers.id = characters.Servers_id GROUP BY Servers.Id, Servers.Name, Servers.MaxCapacity;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Server> orders = Connection.Query<Server>(Query).ToList();
                return orders;
            }
        }
        public async Task AddOrderAsync(Server Value)
        {
            string query = "INSERT INTO Servers(Name, MaxCapacity) VALUES(@Name, @MaxCapacity)";
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

        public async Task UpdateOrderAsync(Server Value)
        {
            string query = "UPDATE Servers Set Name = @Name, MaxCapacity=@MaxCapacity WHERE Id = @Id";
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
            string query = "DELETE FROM Servers WHERE Id = @Id";
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
    public class Server
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? MaxCapacity { get; set; }
        public int Count { get; set; }
    }
}