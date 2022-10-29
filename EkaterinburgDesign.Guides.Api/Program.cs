using Npgsql;

var DB_HOST = Environment.GetEnvironmentVariable("DB_HOST");
var DB_USER = Environment.GetEnvironmentVariable("POSTGRES_USER");
var DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB");
var DB_PWD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connString = $"Host={DB_HOST};Username={DB_USER};Password={DB_PWD};Database={DB_NAME}";

using (var conn = new NpgsqlConnection(connString)) 
{
    conn.Open();
    using (var command = new NpgsqlCommand("CREATE TABLE test(id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER)", conn))
    {
        command.ExecuteNonQuery();
        Console.Out.WriteLine("Finished creating table");
    }

    using (var command = new NpgsqlCommand("INSERT INTO test (name, quantity) VALUES (@n1, @q1), (@n2, @q2), (@n3, @q3)", conn))
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
Console.WriteLine(connString);

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();