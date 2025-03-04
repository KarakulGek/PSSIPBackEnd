using System.Data;
using Dapper;
using MySql.Data.MySqlClient;



namespace Grid_Dapper.Data
{
    public class ClassData
    {
        readonly string ConnectionString = $"Server=127.0.0.1;Port=3306;database=gamedb;user id=root;password=24862486Dan";
        public async Task<List<Class>> GetOrdersAsync()
        {
            try
            {
                //string Query = "SELECT * FROM Classes;";
                /*string Query = "SELECT Classes.Id, Classes.Name, Classes.Weapon, Count(Characters.Id) as 'Count' " +
                    "FROM Classes " +
                    "LEFT JOIN Characters ON Classes.id = Сharacters.Classes_id " +
                    "GROUP BY Classes.Id, Classes.Name, Classes.Weapon;";*/
                string Query = "SELECT classes.id, classes.name, classes.weapon, COUNT(characters.id) AS count FROM classes LEFT JOIN characters ON classes.id = characters.classes_id GROUP BY classes.id, classes.name, classes.weapon;";
                using (IDbConnection Connection = new MySqlConnection(ConnectionString))
                {
                    Connection.Open();
                    List<Class> orders = Connection.Query<Class>(Query).ToList();
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        public async Task AddOrderAsync(Class Value)
        {
            string query = "INSERT INTO Classes(Name, Weapon) VALUES(@Name, @Weapon)";
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

        public async Task UpdateOrderAsync(Class Value)
        {
            string query = "UPDATE Classes Set Name = @Name, Weapon=@Weapon";
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
            string query = "DELETE FROM Classes WHERE Id = @Id";
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
    public class Class
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Weapon { get; set; }
        public int Count { get; set; }
    }
}