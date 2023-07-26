/*
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools
Добавьте пакеты Microsoft.EntityFrameworkCore.Sqlite и Microsoft.EntityFrameworkCore.Tools 
в ваш проект через NuGet Package Manager или команду dotnet add package.
*/


/*
Классы могут сформироваться автоматически на основании структуры базы данных, 
если вы используете подход Database First, то есть генерируете модель из существующей базы данных. 
Для этого вам нужно использовать инструмент Scaffold-DbContext, 
который доступен через Package Manager Console или dotnet CLI. 
Например, если вы хотите сгенерировать модель из базы данных SQLite, то вы можете использовать следующую команду:
dotnet ef dbcontext scaffold "Data Source=MyDatabase.db" Microsoft.EntityFrameworkCore.Sqlite -o Models
Эта команда создаст класс контекста данных и классы сущностей в папке Models на основе таблиц в базе данных MyDatabase.db. Вы можете указать дополнительные параметры для настройки генерации модели, такие как имя контекста данных, пространство имен, схемы и т.д.

dotnet ef dbcontext scaffold "Data Source=C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\wwwroot\DB_Films.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -o Models --project C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\BD_Films.csproj 

 ВОПРОС: Если структура данных изменилась, то мне надо еще раз будет запустить эту команду и файлы в папке Models будут обновлены?
ОТВЕТ: Да, если структура данных изменилась, то вам нужно еще раз запустить эту команду, чтобы обновить файлы в папке Models. Однако, вы должны быть осторожны, так как это может перезаписать любые изменения, которые вы сделали в этих файлах. Поэтому, если вы хотите сохранить свои изменения, вам нужно использовать параметр --force, который создаст новые файлы и переименует старые файлы с суффиксом .old. Например:

dotnet ef dbcontext scaffold "Data Source=C:\Work\C_Sharp\Bing\BD_Films\BD_Films\BD_Films\wwwroot\DB_Films.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -o Models --project BD_Films.csproj --force

Это позволит вам сравнить новые и старые файлы и перенести свои изменения в новые файлы.

Надеюсь, это поможет вам. Если у вас есть еще вопросы, пожалуйста, спрашивайте. blush

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