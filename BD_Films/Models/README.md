[Begin]
Строка, которую ты спросил, - это метод, который переопределяет базовый метод OnConfiguring из класса DbContext. 
Этот метод вызывается при создании экземпляра класса DbFilmsContext и позволяет настроить параметры 
подключения к базе данных.
В этом методе ты используешь метод UseSqlite из класса DbContextOptionsBuilder, чтобы указать, что твоя база данных - это SQLite, и передать строку подключения, которая хранится в статическом поле Program.connectionString.
[End]


[Begin]
ВОПРОС: Скажи, в файле https://github.com/MMax314/BD_Films/blob/master/BD_Films/Program.cs есть строка: builder.Services.AddDbContext<DbFilmsContext>(options => options.UseSqlite(connectionString));
Тут тоже используется connectionString. Это не дублирование со строкой: OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(Program.connectionString); ?
ОТВЕТ: Хороший вопрос. Нет, это не дублирование, а разные способы передать строку подключения к базе данных. В файле Program.cs ты используешь метод AddDbContext из класса IServiceCollection, чтобы зарегистрировать твой класс DbFilmsContext в контейнере зависимостей. Это позволяет тебе внедрять этот класс в другие классы, например, в классы PageModel, которые обрабатывают запросы к твоим веб-страницам. В этом методе ты также можешь указать параметры подключения к базе данных с помощью лямбда-выражения, которое принимает объект DbContextOptionsBuilder и вызывает его метод UseSqlite с твоей строкой подключения.
В файле DbFilmsContext.cs ты переопределяешь метод OnConfiguring из класса DbContext, чтобы также указать параметры подключения к базе данных. Этот метод будет вызван, если ты создашь экземпляр класса DbFilmsContext без передачи объекта DbContextOptions<DbFilmsContext> в конструктор. Это может быть полезно, если ты хочешь использовать твой класс DbFilmsContext вне контейнера зависимостей, например, для тестирования или миграции базы данных.
Таким образом, эти две строки не конфликтуют друг с другом, а предоставляют разные способы настройки подключения к базе данных. Однако, если ты передашь объект DbContextOptions<DbFilmsContext> в конструктор класса DbFilmsContext, то метод OnConfiguring не будет вызван, и будут использованы параметры из этого объекта

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(Program.connectionString);
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//=> optionsBuilder.UseSqlite("Data Source=C:\\Work\\C_Sharp\\Bing\\BD_Films\\BD_Films\\BD_Films\\wwwroot\\DB_Films.sqlite3");
[End]