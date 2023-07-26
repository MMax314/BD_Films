using Microsoft.Data.Sqlite;
using System.Runtime.CompilerServices;

namespace BD_Films
{
    public class Program
    {
        public static string? connectionString { get; set; }
        public static void readConfig()
        {
            // Read config file appsettings.json
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

            //get value DefaultConnection  
            var config = builder.Build();

            //Save config.GetConnectionString("DefaultConnection"); to connectionString
            connectionString = config.GetConnectionString("DefaultConnection");

            //Get path to database file SQLite3 from connectionString
            //string Path = connectionString.Split(";")[0].Split("=")[1];
            //string absolutePath = Path.GetFullPath(relativePath)
            //string currentDir = Directory.GetCurrentDirectory();

            //Create connection to database file SQLite3 from connectionString
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Films LIMIT 3;";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
                }
            }
        }
        public static void Main(string[] args)
        {
            readConfig();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}