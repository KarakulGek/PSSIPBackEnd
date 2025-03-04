using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class EnemyData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public async Task<List<Enemy>> GetOrdersAsync()
        {
            string Query = "SELECT * FROM Enemies;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Enemy> orders = Connection.Query<Enemy>(Query).ToList();
                return orders;
            }
        }
        public async Task AddOrderAsync(Enemy Value)
        {
            string query = "INSERT INTO Enemies(Health, EnemyTypes_Id, Servers_Id) VALUES(@Health, @EnemyTypes_Id, @Servers_Id)";
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

        public async Task UpdateOrderAsync(Enemy Value)
        {
            string query = "UPDATE Enemies Set Health = @Health, EnemyTypes_Id = @EnemyTypes_Id, Servers_Id = @Servers_Id";
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
            string query = "DELETE FROM Enemies WHERE Id = @Id";
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
    public class Enemy
    {
        public int? Id { get; set; }
        public int? Health { get; set; }
        public int? EnemyTypes_Id { get; set; }
        public int? Servers_Id { get; set; }
    }
}