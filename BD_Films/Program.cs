/*
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools
�������� ������ Microsoft.EntityFrameworkCore.Sqlite � Microsoft.EntityFrameworkCore.Tools 
� ��� ������ ����� NuGet Package Manager ��� ������� dotnet add package.
*/


/*
������ ����� �������������� ������������� �� ��������� ��������� ���� ������, 
���� �� ����������� ������ Database First, �� ���� ����������� ������ �� ������������ ���� ������. 
��� ����� ��� ����� ������������ ���������� Scaffold-DbContext, 
������� �������� ����� Package Manager Console ��� dotnet CLI. 
��������, ���� �� ������ ������������� ������ �� ���� ������ SQLite, �� �� ������ ������������ ��������� �������:
dotnet ef dbcontext scaffold "Data Source=MyDatabase.db" Microsoft.EntityFrameworkCore.Sqlite -o Models
��� ������� ������� ����� ��������� ������ � ������ ��������� � ����� Models �� ������ ������ � ���� ������ MyDatabase.db. �� ������ ������� �������������� ��������� ��� ��������� ��������� ������, ����� ��� ��� ��������� ������, ������������ ����, ����� � �.�.

dotnet ef dbcontext scaffold "Data Source=C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\wwwroot\DB_Films.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -o Models --project C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\BD_Films.csproj 

 ������: ���� ��������� ������ ����������, �� ��� ���� ��� ��� ����� ��������� ��� ������� � ����� � ����� Models ����� ���������?
�����: ��, ���� ��������� ������ ����������, �� ��� ����� ��� ��� ��������� ��� �������, ����� �������� ����� � ����� Models. ������, �� ������ ���� ���������, ��� ��� ��� ����� ������������ ����� ���������, ������� �� ������� � ���� ������. �������, ���� �� ������ ��������� ���� ���������, ��� ����� ������������ �������� --force, ������� ������� ����� ����� � ����������� ������ ����� � ��������� .old. ��������:

dotnet ef dbcontext scaffold "Data Source=C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\wwwroot\DB_Films.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -o Models --project BD_Films.csproj --force

��� �������� ��� �������� ����� � ������ ����� � ��������� ���� ��������� � ����� �����.

�������, ��� ������� ���. ���� � ��� ���� ��� �������, ����������, �����������. blush

: https://docs.microsoft.com/en-us/ef/core/cli/dotnet#dotnet-ef-dbcontext-scaffold
 */
using BD_Films.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            builder.Services.AddDbContext<DbFilmsContext>(options => options.UseSqlite(connectionString));

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