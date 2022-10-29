using EkaterinburgDesign.Guides.Api.ApplicationOptions;
using Npgsql;

namespace EkaterinburgDesign.Guides.Api.Database;

public class PostgresDb
{
    private readonly PostgresCredentials postgresCredentials;

    public PostgresDb(PostgresCredentials postgresCredentials)
    {
        this.postgresCredentials = postgresCredentials;
    }

    public void TestMethod()
    {
        //Кажется, такие вещи нужно делать через ORM. Для работы с БД есть отдельная задача
        
        using (var conn = new NpgsqlConnection(postgresCredentials.ConnectionString))
        {
            conn.Open();
            using (var command =
                   new NpgsqlCommand("CREATE TABLE test(id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER)",
                       conn))
            {
                command.ExecuteNonQuery();
                Console.Out.WriteLine("Finished creating table");
            }

            using (var command =
                   new NpgsqlCommand("INSERT INTO test (name, quantity) VALUES (@n1, @q1), (@n2, @q2), (@n3, @q3)",
                       conn))
            {
                command.Parameters.AddWithValue("n1", "banana");
                command.Parameters.AddWithValue("q1", 150);
                command.Parameters.AddWithValue("n2", "orange");
                command.Parameters.AddWithValue("q2", 154);
                command.Parameters.AddWithValue("n3", "apple");
                command.Parameters.AddWithValue("q3", 100);

                int nRows = command.ExecuteNonQuery();
                Console.Out.WriteLine(String.Format("Number of rows inserted={0}", nRows));
            }
        }
    }
}