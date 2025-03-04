using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class CharacterData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public async Task<List<Character>> GetOrdersAsync()
        {
            string Query = "SELECT * FROM Characters;";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Character> orders = Connection.Query<Character>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<Character>> GetOrdersMaxAsync(string column)
        {
            string Query = $"SELECT * FROM Characters WHERE {column} = (SELECT MAX({column}) FROM Characters);";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Character> orders = Connection.Query<Character>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<Character>> GetOrdersMaxByIdAsync(string column, int userId)
        {
            string Query = $"SELECT * FROM Characters WHERE {column} = (SELECT MAX({column}) FROM Characters WHERE Users_Id = '{userId}');";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Character> orders = Connection.Query<Character>(Query).ToList();
                return orders;
            }
        }
        public async Task<List<Character>> GetOrdersByUserAsync(int userId)
        {
            string Query = "SELECT * FROM Characters WHERE Users_Id = '" + userId + "';";
            using (IDbConnection Connection = new MySqlConnection(ConnectionString))
            {
                Connection.Open();
                List<Character> orders = Connection.Query<Character>(Query).ToList();
                return orders;
            }
        }
        public async Task AddOrderAsync(Character Value)
        {
            string query = "INSERT INTO Characters(Name, Health, Score, HoursPlayed, Classes_Id, Servers_Id, Users_Id) VALUES(@Name, @Health, @Score, @HoursPlayed, @Classes_Id, @Servers_Id, @Users_Id)";
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

        public async Task UpdateOrderAsync(Character Value)
        {
            string query = "UPDATE Characters Set Name = @Name, Health=@Health, Score = @Score, HoursPlayed=@HoursPlayed, Classes_Id = @Classes_Id, Servers_Id = @Servers_Id, Users_Id = @Users_Id WHERE Id = @Id";
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
            string query = "DELETE FROM Characters WHERE Id = @Id";
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
    public class Character
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Health { get; set; }
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Classes_Id { get; set; }
        public int? Servers_Id { get; set; }
        public int? Users_Id { get; set; }
    }
}