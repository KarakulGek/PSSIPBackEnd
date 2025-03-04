using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class EnemyTypeData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public async Task<List<EnemyType>> GetOrdersAsync()
        {
            string Query = "SELECT EnemyTypes.Id, EnemyTypes.Name, EnemyTypes.MaxHealth, COUNT(Enemies.id) AS count FROM EnemyTypes LEFT JOIN enemies ON EnemyTypes.id = enemies.EnemyTypes_id GROUP BY EnemyTypes.id, EnemyTypes.name, EnemyTypes.MaxHealth;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<EnemyType> orders = Connection.Query<EnemyType>(Query).ToList();
                return orders;
            }
        }
        public async Task AddOrderAsync(EnemyType Value)
        {
            string query = "INSERT INTO EnemyTypes(Name, MaxHealth) VALUES(@Name, @MaxHealth)";
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

        public async Task UpdateOrderAsync(EnemyType Value)
        {
            string query = "UPDATE EnemyTypes Set Name = @Name, MaxHealth = @MaxHealth";
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
            string query = "DELETE FROM EnemyTypes WHERE Id = @Id";
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
    public class EnemyType
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? MaxHealth{ get; set; }
        public int Count { get; set; }
    }
}