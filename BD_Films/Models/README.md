[Begin]
������, ������� �� �������, - ��� �����, ������� �������������� ������� ����� OnConfiguring �� ������ DbContext. 
���� ����� ���������� ��� �������� ���������� ������ DbFilmsContext � ��������� ��������� ��������� 
����������� � ���� ������.
� ���� ������ �� ����������� ����� UseSqlite �� ������ DbContextOptionsBuilder, ����� �������, ��� ���� ���� ������ - ��� SQLite, � �������� ������ �����������, ������� �������� � ����������� ���� Program.connectionString.
[End]


[Begin]
������: �����, � ����� https://github.com/MMax314/BD_Films/blob/master/BD_Films/Program.cs ���� ������: builder.Services.AddDbContext<DbFilmsContext>(options => options.UseSqlite(connectionString));
��� ���� ������������ connectionString. ��� �� ������������ �� �������: OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(Program.connectionString); ?
�����: ������� ������. ���, ��� �� ������������, � ������ ������� �������� ������ ����������� � ���� ������. � ����� Program.cs �� ����������� ����� AddDbContext �� ������ IServiceCollection, ����� ���������������� ���� ����� DbFilmsContext � ���������� ������������. ��� ��������� ���� �������� ���� ����� � ������ ������, ��������, � ������ PageModel, ������� ������������ ������� � ����� ���-���������. � ���� ������ �� ����� ������ ������� ��������� ����������� � ���� ������ � ������� ������-���������, ������� ��������� ������ DbContextOptionsBuilder � �������� ��� ����� UseSqlite � ����� ������� �����������.
� ����� DbFilmsContext.cs �� ��������������� ����� OnConfiguring �� ������ DbContext, ����� ����� ������� ��������� ����������� � ���� ������. ���� ����� ����� ������, ���� �� ������� ��������� ������ DbFilmsContext ��� �������� ������� DbContextOptions<DbFilmsContext> � �����������. ��� ����� ���� �������, ���� �� ������ ������������ ���� ����� DbFilmsContext ��� ���������� ������������, ��������, ��� ������������ ��� �������� ���� ������.
����� �������, ��� ��� ������ �� ����������� ���� � ������, � ������������� ������ ������� ��������� ����������� � ���� ������. ������, ���� �� �������� ������ DbContextOptions<DbFilmsContext> � ����������� ������ DbFilmsContext, �� ����� OnConfiguring �� ����� ������, � ����� ������������ ��������� �� ����� �������

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(Program.connectionString);
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//=> optionsBuilder.UseSqlite("Data Source=C:\\Work\\C_Sharp\\Bing\\BD_Films\\BD_Films\\BD_Films\\wwwroot\\DB_Films.sqlite3");
[End]